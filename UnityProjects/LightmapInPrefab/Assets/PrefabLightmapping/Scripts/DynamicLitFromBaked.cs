using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Get color from the lightmap below a dynamic object.
/// Lightmap must be read enabled!
/// Only works on objects with mesh colliders
/// Setup to work with the DynamicLit shader.
/// </summary>
public class DynamicLitFromBaked : MonoBehaviour {

    public Transform rayStartTransform; //defaults to this transform
    public Vector3 rayStartOffset;
    public float rayRange = 1f; //make sure to consider rayStartOffset;
    public LayerMask checkLayers;
    public QueryTriggerInteraction testTriggers = QueryTriggerInteraction.Ignore;

    public Color sampledColorFromLightmap; //color result from checking lightmap

    private Renderer rend;

    // Use this for initialization
    void Start () {
		if (rayStartTransform == null)
        {
            rayStartTransform = transform;
        }

        rend = GetComponentInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        SampleLightmap();
        ApplyColor();
	}

    void SampleLightmap()
    {
        Vector3 rayStart = rayStartTransform.position + rayStartOffset;
        RaycastHit hit;
        if (Physics.Raycast(rayStart, Vector3.down, out hit, rayRange, checkLayers, testTriggers))
        {
            Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
            if (hitRenderer == null || hitRenderer.lightmapIndex > 253)
            {
                //no lightmap can be found
                return;
            }
            LightmapData lightmapData = LightmapSettings.lightmaps[hitRenderer.lightmapIndex];
            Texture2D lightmapColor = lightmapData.lightmapColor;
            Vector2 hitUV = hit.lightmapCoord;
            Color surfaceColor = lightmapColor.GetPixelBilinear(hitUV.x, hitUV.y);
            sampledColorFromLightmap = surfaceColor;
        }
    }

    public float valueMultiplier = 3f;
    public float saturationMultiplier = 0.5f;

    void ApplyColor()
    {
        rend.material.SetColor("_LightColor", sampledColorFromLightmap);
        return;
        /*//new stuff

        float h;
        float s;
        float v;
        Color.RGBToHSV(sampledColorFromLightmap, out h, out s, out v);
        //v = Mathf.Clamp01(Mathf.Pow(v, 1f - v));
        s *= saturationMultiplier;
        v *= valueMultiplier;
        v = Mathf.Clamp01(v);
        //v = Mathf.Max(v, 0.25f);

        Color revised = Color.HSVToRGB(h, s, v); //max value
        //rend.material.SetColor("_LightColor", revised);
        rend.material.SetColor("_MainColor", revised);
        */
    }
}


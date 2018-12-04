# UnityLightmapPrefabs
Lightmapping Prefabs in Unity 2018

# What is this?
My fork of Joachim Ante's solution for attaching lightmaps to runtime instantiated prefabs in Unity 5+, posted on the Unity forums here: https://forum.unity.com/threads/problems-with-instantiating-baked-prefabs.324514/

The goal is to make something more organized and robust out of it.

# What is it for?
Baking lightmaps into prefabs can be a major optimization for mobile and VR runtime procedural environments.

# How to use it
- Tested with Unity 2018.2.6f1
  - Unknown if it will work in other versions.
  

- Add PrefabLightmapData.cs to the top level of a prefab.
  - Prefab must be have all Static flags except Batching Static.
- Put the prefab in a scene and bake a non-directional lightmap.
  - Bake scene should have the same Lighting settings as the scene you intent to instantiate the prefab into.
  - Bake scene must have auto baking off.
  - Bake scene must be included in the build scenes list.
- Still in the bake scene, run _Assets > Bake Prefab Lightmaps_
- Instantiate the prefab at runtime in any scene you want.

## Lighting Dynamic objects like characters
New feature not in original package.

This system can preserve baked lighmaps, but does not yet preserve light probes for dynamic objects. Instead, the functionality of light probes is approximated with a component and a shader.

- DynamicLitFromBaked.cs can sample lightmapped objects underneath an object to determine light color and intensity.
  - Requires lightmaps be read/write enabled
  - Requires sampled objects to have meshcolliders
  - Only recommended for smallish moving objects like humans.
  - Feeds light color/intensity to DynamicLit shader.
- DynamicLit shader
  - Simplified PBR
  - Takes a color with alpha intensity
  - Approximates light probe functionality
- Scene setup
  - Add a white, 1 intensity directional light
  - Culling masked to only affect DynamicLit GameObjects.
  - Not 100% necessary, but important for shading quality

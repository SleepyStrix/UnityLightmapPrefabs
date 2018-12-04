# UnityLightmapPrefabs
Lightmapping Prefabs in Unity

# What is this?
My fork of Joachim Ante's solution for attaching lightmaps to runtime instantiated prefabs in Unity 5+, posted on the Unity forums here: https://forum.unity.com/threads/problems-with-instantiating-baked-prefabs.324514/

The goal is to make something more organized and robust out of it.

# What is it for?
Baking lightmaps into prefabs can be a major optimization for mobile and VR runtime procedural environments.

# How to use it
- Add PrefabLightmapData.cs to the top level of a prefab.
 - Prefab must be have all Static flags except Batching Static.
- Put the prefab in a scene and bake a non-directional lightmap.
 - Bake scene should have the same Lighting settings as the scene you intent to instantiate the prefab into.
 - Bake scene must have auto baking off.
 - Bake scene must be included in the build scenes list.
- Still in the bake scene, run _Assets > Bake Prefab Lightmaps_
- Instantiate the prefab at runtime in any scene you want.

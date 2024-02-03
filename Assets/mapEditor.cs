#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MapGenerator))]
public class mapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = (MapGenerator)target;


        DrawDefaultInspector();

        if (GUILayout.Button("Generate valid map"))
        {
            mapGenerator.GenerateValidMap();
        }

        if (GUILayout.Button("Generate map"))
        {
            mapGenerator.GenerateMap();
        }

        if (GUILayout.Button("Generate noise map"))
        {
            mapGenerator.GenerateNoiseMap();
        }

    }
}
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class BoardEdit : Editor {
    SerializedProperty HorizontalWallsProp;
    SerializedProperty VerticalWallsProp;

    void OnEnable()
    {
        HorizontalWallsProp = serializedObject.FindProperty("HorizontalWalls");
        VerticalWallsProp = serializedObject.FindProperty("VerticalWalls");
    }
    public override void OnInspectorGUI()
    {
        int Width = serializedObject.FindProperty("Width").intValue;
        int Height = serializedObject.FindProperty("Height").intValue;

        serializedObject.Update();

        //int size;
        //size = HorizontalWallsProp.FindPropertyRelative("Array.size").intValue;
        EditorGUILayout.BeginVertical();
        
        EditorGUILayout.LabelField(new GUIContent("Vertical Walls"));
        for (int y = Height; y >= 0; y--)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < Width; x++)
            {
                int i = (y * (Width + 1)) + x;
                SerializedProperty prop = HorizontalWallsProp.GetArrayElementAtIndex(i);
                prop.boolValue = EditorGUILayout.Toggle(prop.boolValue);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.LabelField(new GUIContent("Horizontal Walls"));
        for (int y = Height - 1; y >= 0; y--)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < Width + 1; x++)
            {
                int i = (y * (Width + 1)) + x;
                SerializedProperty prop = VerticalWallsProp.GetArrayElementAtIndex(i);
                prop.boolValue = EditorGUILayout.Toggle(prop.boolValue);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

}

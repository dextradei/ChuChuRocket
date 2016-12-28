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

        EditorGUILayout.BeginVertical();
        for (int y = 0; y < Height + 1; y++)
        {
            //Horizontal Row
            //Make the width slightly smaller
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            for (int x = 0; x < Width; x++)
            {
                int i = ((Height - y) * (Width + 1)) + x;
                //Debug.Log("x: " + x + ", y: " + y + ", i: " + i);
                SerializedProperty prop = HorizontalWallsProp.GetArrayElementAtIndex(i);
                prop.boolValue = EditorGUILayout.Toggle(prop.boolValue);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();

            //There is one extra horizontal row
            //skip the vertical row on the last loop
            if (y > Height - 1) continue;

            //Vertical Row
            EditorGUILayout.BeginHorizontal();
            //Vertical rows are wider by one
            for (int x = 0; x < Width + 1; x++)
            {
                int i = ((Height - y - 1) * (Width + 1)) + x;
                SerializedProperty prop = VerticalWallsProp.GetArrayElementAtIndex(i);
                prop.boolValue = EditorGUILayout.Toggle(prop.boolValue);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        //int size;
        //size = HorizontalWallsProp.FindPropertyRelative("Array.size").intValue;
        /*
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
        */

        serializedObject.ApplyModifiedProperties();
    }

}

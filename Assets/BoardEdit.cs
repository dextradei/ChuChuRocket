using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class BoardEdit : Editor {
    SerializedProperty HeightProp;
    SerializedProperty WidthProp;
    SerializedProperty HorizontalWallsProp;
    SerializedProperty VerticalWallsProp;

    void OnEnable()
    {
        WidthProp = serializedObject.FindProperty("Width");
        HeightProp = serializedObject.FindProperty("Height");
        HorizontalWallsProp = serializedObject.FindProperty("HorizontalWalls");
        VerticalWallsProp = serializedObject.FindProperty("VerticalWalls");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        EditorGUILayout.BeginVertical();

        int Width = WidthProp.intValue;
        int Height = HeightProp.intValue;

        WidthProp.intValue = EditorGUILayout.IntField("Width", Width);
        HeightProp.intValue = EditorGUILayout.IntField("Height", Height);

        EditorGUILayout.EndVertical();

        if (((WidthProp.intValue != Width) || (HeightProp.intValue != Height)) && (WidthProp.intValue > 0) && (HeightProp.intValue > 0))
        {
            //resize HorizontalWalls and VerticalWalls arrays
            Debug.Log("Resizing array");
            Width = WidthProp.intValue;
            Height = HeightProp.intValue;
            HorizontalWallsProp.arraySize = Width * (Height + 1);
            VerticalWallsProp.arraySize = (Width + 1) * Height;

        }

        if ((Height < 1) || (Width < 1)) return;

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
                int i = ((Height - y) * Width) + x;
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

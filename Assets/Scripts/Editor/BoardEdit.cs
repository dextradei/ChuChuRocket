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

        //Draw exposed properties
        DrawDefaultInspector();

        //Draw custom properties

        //Allow resizing of grid height/width
        //TODO: auto resizing of board graphics, currently static image of 16x16 squares
        EditorGUILayout.BeginVertical();

        int Width = WidthProp.intValue;
        int Height = HeightProp.intValue;

        WidthProp.intValue = EditorGUILayout.IntField("Width", Width);
        HeightProp.intValue = EditorGUILayout.IntField("Height", Height);

        EditorGUILayout.EndVertical();

        //if the size changed and the new size is valid, resize the arrays
        if (((WidthProp.intValue != Width) || (HeightProp.intValue != Height)) && (WidthProp.intValue > 0) && (HeightProp.intValue > 0))
        {
            //resize HorizontalWalls and VerticalWalls arrays
            Debug.Log("Resizing array");
            Width = WidthProp.intValue;
            Height = HeightProp.intValue;
            //HorizontalWalls array is taller than the board by 1.  If we're keeping track of the wall below each square, need an extra row for the top row of walls
            HorizontalWallsProp.arraySize = Width * (Height + 1);
            //VerticalWalls array is wider than the board by 1.
            VerticalWallsProp.arraySize = (Width + 1) * Height;

        }

        //Don't draw the grid of toggles if the size isn't valid
        if ((Height < 1) || (Width < 1)) return;

        //Draw grid of toggles
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

        serializedObject.ApplyModifiedProperties();
    }

}

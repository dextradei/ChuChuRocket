using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class BoardEdit : Editor {
	SerializedProperty heightProp;
	SerializedProperty widthProp;
	SerializedProperty horizontalWallsProp;
	SerializedProperty verticalWallsProp;

	void OnEnable()
	{
		widthProp = serializedObject.FindProperty("Width");
		heightProp = serializedObject.FindProperty("Height");
		horizontalWallsProp = serializedObject.FindProperty("HorizontalWalls");
		verticalWallsProp = serializedObject.FindProperty("VerticalWalls");
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

		int Width = widthProp.intValue;
		int Height = heightProp.intValue;

		widthProp.intValue = EditorGUILayout.IntField("Width", Width);
		heightProp.intValue = EditorGUILayout.IntField("Height", Height);

		EditorGUILayout.EndVertical();

		//if the size changed and the new size is valid, resize the arrays
		if (((widthProp.intValue != Width) || (heightProp.intValue != Height)) && (widthProp.intValue > 0) && (heightProp.intValue > 0))
		{
			//resize HorizontalWalls and VerticalWalls arrays
			Debug.Log("Resizing array");
			Width = widthProp.intValue;
			Height = heightProp.intValue;
			//HorizontalWalls array is taller than the board by 1.  If we're keeping track of the wall below each square, need an extra row for the top row of walls
			horizontalWallsProp.arraySize = Width * (Height + 1);
			//VerticalWalls array is wider than the board by 1.
			verticalWallsProp.arraySize = (Width + 1) * Height;

		}

		//Don't draw the grid of toggles if the size isn't valid
		if ((Height < 1) || (Width < 1)) return;

		//Draw grid of toggles
		EditorGUILayout.BeginVertical();
		for (int y = 0; y < Height + 1; y++)
		{
			//Horizontal Walls Row
			//Make the width slightly smaller
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			for (int x = 0; x < Width; x++)
			{
				int i = ((Height - y) * Width) + x;
				//Debug.Log("x: " + x + ", y: " + y + ", i: " + i);
				SerializedProperty prop = horizontalWallsProp.GetArrayElementAtIndex(i);
				prop.boolValue = EditorGUILayout.Toggle(prop.boolValue);
			}
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();

			//There is one extra horizontal row
			//skip the vertical row on the last loop
			if (y > Height - 1) continue;

			//Vertical Walls Row
			EditorGUILayout.BeginHorizontal();
			//Vertical rows are wider by one
			for (int x = 0; x < Width + 1; x++)
			{
				int i = ((Height - y - 1) * (Width + 1)) + x;
				SerializedProperty prop = verticalWallsProp.GetArrayElementAtIndex(i);
				prop.boolValue = EditorGUILayout.Toggle(prop.boolValue);
			}
			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndVertical();

		serializedObject.ApplyModifiedProperties();
	}

}

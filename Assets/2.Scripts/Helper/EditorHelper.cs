using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorHelper 
{
    
    public static void EditorToolTopLater(BaseData data, ref int selection,
        ref UnityEngine.Object source, int uiWidth)
    {
        EditorGUILayout.BeginHorizontal();
        {
            if(GUILayout.Button("Add", GUILayout.Width(uiWidth)))
            {
                
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}

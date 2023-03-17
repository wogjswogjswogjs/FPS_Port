using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Windows;
using File = System.IO.File;
using UnityEditor;


public class PlayerData
{
    public string playerName;
    public int coin;
    public float health;
    public bool[] levelCleared;
    
    public GameObject weapon;
}

public class Test : EditorWindow
{
    public GameObject obj;
    [MenuItem("Tools/Test Tool")]
    static void Init()
    {
        Test window = GetWindow<Test>(false, "TestTool");
        
    }
    
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        {
            if(GUILayout.Button("Add", GUILayout.Width(100)))
            {
                Debug.Log("Click Add");
                Instantiate(obj, Vector3.zero, Quaternion.identity);
            }
        }
        EditorGUILayout.EndHorizontal();

        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public UnityEngine.Object obj;
    // Start is called before the first frame update
    void Start()
    {
        string test = AssetDatabase.GetAssetPath(obj);
        GameObject obj1 = (GameObject)AssetDatabase.LoadAssetAtPath
            (test, typeof(GameObject));
        
    }

// Update is called once per frame
    void Update()
    {
        
    }
}

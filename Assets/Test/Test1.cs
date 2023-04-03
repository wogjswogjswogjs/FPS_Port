using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;




public class Test1 : MonoBehaviour
{
   public GameObject obj;
   public void Start()
   {
       List<int> list = new List<int>();
       list.Add(1);
       list.RemoveAt(list.Count-1);
       foreach (var VARIABLE in list)
       {
           Debug.Log(VARIABLE);
       }

   }
}

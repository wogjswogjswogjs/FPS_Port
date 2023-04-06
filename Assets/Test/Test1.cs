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
       float test = Mathf.Lerp(0.2f, 1.0f, 0.5f);
       Debug.Log(test);
   }
}

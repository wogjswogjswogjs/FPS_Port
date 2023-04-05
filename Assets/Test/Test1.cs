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
        SoundData soundData = ScriptableObject.CreateInstance<SoundData>();
        soundData.LoadData();
        Debug.Log(soundData.soundClips[0].checkTime[0]);
        
        Debug.Log(soundData.soundClips[0].setTime[0]);
        

    }
}

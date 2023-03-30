using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class Test1 : MonoBehaviour
{
    public Transform test;
    public GameObject obj;
    public Vector3 targetTransform = new Vector3(10,10,0);
    public int b = 10;
    public float testfloat = 0f;
    private Interpolate.Function interpolateFunc;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        interpolateFunc = Interpolate.Ease(Interpolate.EaseType.EaseOutQuart);
        
    }

// Update is called once per frame
    void Update()
    {
        Debug.Log(testfloat);
        if (testfloat > 10)
        {
            return;
        }
        testfloat += Time.deltaTime;
        transform.position = Interpolate.Ease(this.interpolateFunc, transform.position, 
            targetTransform * Time.deltaTime, testfloat, 10);
       


    }

    public void DoFade(float time, AudioSource source)
    {
        
    }

   
}

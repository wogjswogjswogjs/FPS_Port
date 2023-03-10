using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.IO;
using TreeEditor;

public class EffectData : BaseData
{
    public EffectClip[] effectClips = null;

    public string clipPath = "Effects/";
    private string xmlFilePath = string.Empty;
    private string xmlFileName = "effectData.xml";
    private string dataPath = "Data/effectData";
    //XML 구분자
    //private const string EFFECT = "effect" // 저장 키.
    //private const string CLIP = "clip" // 저장 키.

    private EffectData()
    {
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;


/// <summary>
/// 이펙트 클립 리스트와 이펙트 파일 이름과 경로를 가지고 있으며
/// 파일을 읽고 쓰는 기능을 가지고 있다
/// </summary>
public class EffectData : BaseData
{
    public List<EffectClip> effectClips = new List<EffectClip>();
    
    private string clipPath = "Prefabs/Effects/";
    private string jsonFileName = "effectData.json";
    private EffectData()
    {
        
    }
    
    /// <summary>
    /// json으로 저장되어있는 EffectData정보를 불러오는 함수
    /// </summary>
    public void LoadData()
    {
        string jdata;
        EffectData effectData;
        
        try
        {
            // 제이슨 파일을 불러오는 작업
            jdata = System.IO.File.ReadAllText(Application.dataPath + dataDirectory + jsonFileName);
            effectData = JsonConvert.DeserializeObject<EffectData>(jdata);
        }
        catch (Exception e)
        {
            return;
        }
        
        effectClips = effectData.effectClips;
        dataNameList = effectData.dataNameList;
        
        foreach (var clip in effectClips)
        {
            clip.LoadEffect();
        }
    }

    /// <summary>
    /// EffectData정보를 json파일로 저장하는 함수
    /// </summary>
    public void SaveData()
    {
        foreach (var clip in effectClips)
        {
            clip.ReleaseEffect();
        }
        string jdata = JsonConvert.SerializeObject(this);
        System.IO.File.WriteAllText(Application.dataPath + dataDirectory + jsonFileName, jdata);
    }
    

    public override int AddData()
    {
        EffectClip effectClip = new EffectClip();
        effectClip.effectName = "New Effect";
        effectClip.effectPath = clipPath;
        effectClips.Add(effectClip);
        dataNameList.Add(effectClip.effectName);
        
        return GetDataCount();
    }

    public override void RemoveData(int index)
    {
        effectClips.RemoveAt(index);
        dataNameList.RemoveAt(index);
    }

    public void ClearData()
    {
        foreach (var clip in this.effectClips)
        {
            clip.ReleaseEffect();
        }

        effectClips = null;
        dataNameList = null;
    }

    public EffectClip GetCopyEffect(int index)
    {
        if (effectClips[index] == null)
        {
            return null;
        }

        EffectClip original = this.effectClips[index];
        EffectClip retClip = new EffectClip();
        retClip.effectPath = original.effectPath;
        retClip.effectName = original.effectName;
        retClip.effectType = original.effectType;
        retClip.effectID = original.effectID;
        return retClip;
    }

    public EffectClip GetEffect(int index)
    {
        if (effectClips[index] == null)
        {
            return null;
        }
        
        effectClips[index].LoadEffect();
        return effectClips[index];
    }
}

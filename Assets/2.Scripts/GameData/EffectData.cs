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
    public List<EffectClip> effectClips = null;
    
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
        if (effectClips == null)
        {
            effectClips = new List<EffectClip>();
            dataNameList = new List<string>();
        }
        // 제이슨 파일을 불러오는 작업
        string jdata = System.IO.File.ReadAllText(Application.dataPath + dataDirectory + jsonFileName);
        EffectData effectData = JsonConvert.DeserializeObject<EffectData>(jdata);
        
        // 불러온 제이슨 파일을 바탕으로 데이터를 Load하는 과정
        foreach (var effectClip in effectData.effectClips)
        {
            this.effectClips.Add(effectClip);
        }

        foreach (var dataName in effectData.dataNameList)
        {
            this.dataNameList.Add(dataName);
        }
        
    }

    /// <summary>
    /// EffectData정보를 json파일로 저장하는 함수
    /// </summary>
    public void SaveData()
    {
        string jdata = JsonConvert.SerializeObject(this);
        System.IO.File.WriteAllText(Application.dataPath + dataDirectory + jsonFileName, jdata);
    }

    public override int AddData(int dataID, string dataName)
    {
        if (this.effectClips == null)
        {
            this.effectClips = new List<EffectClip>();
            this.dataNameList = new List<string>();
        }

        EffectClip clip = new EffectClip();
        clip.effectID = dataID;
        this.effectClips.Add(clip);
        this.dataNameList.Add(dataName);
        return GetDataCount();
    }

    public override void RemoveData(int index)
    {
        this.effectClips.RemoveAt(index);
        this.dataNameList.RemoveAt(index);
    }

    public void ClearData()
    {
        foreach (var clip in this.effectClips)
        {
            clip.ReleaseEffect();
        }

        this.effectClips = null;
        this.dataNameList = null;
    }

    public EffectClip GetCopyEffect(int index)
    {
        if (index < 0 || index >= this.effectClips.Count)
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
        if (index < 0 || index >= this.effectClips.Count)
        {
            return null;
        }
        
        effectClips[index].LoadEffect();
        return effectClips[index];
    }
}

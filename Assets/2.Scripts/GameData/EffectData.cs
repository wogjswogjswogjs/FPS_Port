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
    private Dictionary<int, EffectClip> effectClips = null;
    
    private string clipPath = "Prefabs/Effects/";
    
    private string jsonFileName = "/effectData.json";
    private string jsonFilePath = "/EffectData";

    private EffectData()
    {
        
    }
    
    /// <summary>
    /// json으로 저장되어있는 EffectData정보를 불러오는 함수
    /// </summary>
    public void LoadData()
    {
        // 제이슨 파일을 불러오는 작업
        string jdata = System.IO.File.ReadAllText(Application.dataPath + dataDirectory + jsonFilePath +jsonFileName);
        EffectData effectData = JsonConvert.DeserializeObject<EffectData>(jdata);
        
        // 불러온 제이슨 파일을 바탕으로 데이터를 Load하는 과정
        foreach (var item in effectData.effectClips)
        {
            this.effectClips.Add(item.Key,item.Value);
        }

        foreach (var item in effectData.dataNameList)
        {
            this.dataNameList.Add(item);
        }
        
    }

    /// <summary>
    /// EffectData정보를 json파일로 저장하는 함수
    /// </summary>
    public void SaveData()
    {
        // 데이터를 저장하기 전에 사용한 이펙트 게임오브젝트 메모리를 해제하는 작업.
        foreach (var item in effectClips)
        {
            item.Value.ReleaseEffect();
        }
        string jdata = JsonConvert.SerializeObject(this);
        System.IO.File.WriteAllText(Application.dataPath + dataDirectory + jsonFilePath + jsonFileName, jdata);
    }
    
    /// <summary>
    /// effectClips에 데이터를 추가하는 함수
    /// </summary>
    public void AddData(int dataID, string dataName, EffectType effectType = EffectType.None)
    {
        if (effectClips == null)
        {
            effectClips = new Dictionary<int, EffectClip>();
        }

        if (dataNameList == null)
        {
            dataNameList = new List<string>();
        }
        
        dataNameList.Add(dataID.ToString() + " - " +  dataName);
        
        effectClips.Add(dataID, new EffectClip());
        effectClips[dataID].effectPath = this.clipPath;
        effectClips[dataID].effectName = dataName;
        effectClips[dataID].effectType = effectType;
    }
    
    /// <summary>
    /// effectClips에있는 데이터를 제거하는 함수
    /// </summary>
    public void RemoveData(int dataID)
    {
        if (effectClips.Count > 0)
        {
            // 혹시몰라서 메모리 한번더 해제
            effectClips[dataID].ReleaseEffect();
            effectClips.Remove(dataID);
        }

        if (effectClips.Count == 0)
        {
            effectClips = null;
        }

        if (dataNameList.Count == 0)
        {
            dataNameList = null;
        }
    }
    
    /// <summary>
    /// effectClips의 모든 데이터를 제거하는 함수
    /// </summary>
    public void ClearData()
    {
        foreach (var clip in effectClips)
        {
            clip.Value.ReleaseEffect();
        }

        effectClips = null;
        dataNameList = null;
    }

    /// <summary>
    /// effectClips의 데이터중 하나의 똑같이 복사하여,
    /// 프리로딩 후 리턴하는 함수
    /// </summary>
    public EffectClip GetCopyClip(int dataID)
    {
        if (effectClips.ContainsKey(dataID) == false)
        {
            return null;
        }

        EffectClip original = effectClips[dataID];
        EffectClip clip = new EffectClip();
        clip.effectPath = original.effectPath;
        clip.effectName = original.effectName;
        clip.effectType = original.effectType;
        clip.PreLoad();
        return clip;
    }

    /// <summary>
    /// effectClips의 데이터중 하나를 프리로딩 후
    /// 리턴하는 함수
    /// </summary>
    public EffectClip GetClip(int dataID)
    {
        if (effectClips.ContainsKey(dataID) == false)
        {
            return null;
        }

        EffectClip effectClip = effectClips[dataID];
        effectClip.PreLoad();
        return effectClip;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class SoundData : BaseData
{
    public List<SoundClip> soundClips = new List<SoundClip>();
    
    private string clipPath = "Prefabs/Sounds/";
    private string jsonFileName = "soundData.json";

    public SoundData()
    {
    }

    public void SaveData()
    {
        foreach (var item in soundClips)
        {
            item.ReleaseSound();
        }
        string jdata = JsonConvert.SerializeObject(this);
        System.IO.File.WriteAllText(Application.dataPath + dataDirectory + jsonFileName, jdata);
    }

    public void LoadData()
    {
        string jdata;
        SoundData soundData;

        try
        {
            jdata = System.IO.File.ReadAllText(Application.dataPath + dataDirectory + jsonFileName);
            soundData = JsonConvert.DeserializeObject<SoundData>(jdata);
        }
        catch (Exception e)
        {
            return;
        }
        
        soundClips = soundData.soundClips;
        dataNameList = soundData.dataNameList;
        
        foreach (var item in soundClips)
        {
            item.PreLoad();
        }
    }

    public override int AddData()
    {
        SoundClip soundClip = new SoundClip();
        soundClip.soundName = "New Sound";
        soundClip.soundPath = clipPath;
        soundClips.Add(soundClip);
        dataNameList.Add(soundClip.soundName);
        
        return GetDataCount();
    }
    
    public override void RemoveData(int index)
    {
        soundClips.RemoveAt(index);
        dataNameList.RemoveAt(index);
    }
    
    public SoundClip GetSound(int index)
    {
        if (soundClips[index] == null)
        {
            return null;
        }
        
        soundClips[index].PreLoad();
        return soundClips[index];
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// 루프, 페이드 관련 속성, 오디오 클립 속성들.
/// </summary>
public class SoundClip
{
    public int soundID = 0;
    public SoundPlayType playType = SoundPlayType.None;
    public string soundName = string.Empty;
    public string soundPath = string.Empty;
    
    // SoundClip 속성들
    public float maxVolume = 1.0f;
    public bool hasLoop = false;
    public List<float> checkTime = new List<float>();
    public List<float> setTime = new List<float>();

    private AudioClip soundPrefab = null;
    public int currentLoop = 0;
    public float pitch = 1.0f;
    public float dopplerLevel = 1.0f;
    public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;
    public float minDistance = 10000.0f;
    public float maxDistance = 50000.0f;
    public float sparialBlend = 1.0f;

    public float fadeTime1 = 0.0f;
    public float fadeTime2 = 0.0f;
    public Interpolate.Function interpolateFunc;
    public bool isFadeIn = false;
    public bool isFadeOut = false;

    public SoundClip()
    {
    }

    public SoundClip(string clipPath, string clipName)
    {
        soundPath = clipPath;
        soundName = clipName;
    }

    public void PreLoad()
    {
        if (soundPrefab == null)
        {
            this.soundPrefab = ResourceManager.Load(soundPath + soundName) as AudioClip;
        }
    }

    public void AddLoop()
    {
        this.checkTime.Add(0.0f);
        this.setTime.Add(0.0f);
    }

    public void RemoveLoop(int index)
    {
        this.checkTime.RemoveAt(index);
        this.setTime.RemoveAt(index);
    }

    public AudioClip GetSound()
    {
        if(this.soundPrefab == null) PreLoad();
        return this.soundPrefab;
    }

    public void ReleaseSound()
    {
        if (this.soundPrefab != null)
        {
            this.soundPrefab = null;
        }
    }

    public bool HasLoop()
    {
        return this.checkTime.Count > 0;
    }

    public void NextLoop()
    {
        this.currentLoop++;
        if (this.currentLoop >= this.checkTime.Count)
        {
            this.currentLoop = 0;
        }
    }

    public void CheckLoop(AudioSource source)
    {
        if (HasLoop() && source.time >= this.checkTime[this.currentLoop])
        {
            source.time = this.setTime[this.currentLoop];
            this.NextLoop();
        }
    }

    public void FadeIn(float time, Interpolate.EaseType easeType)
    {
        this.isFadeOut = false;
        this.fadeTime1 = 0.0f;
        this.fadeTime2 = time;
        this.interpolateFunc = Interpolate.Ease(easeType);
        this.isFadeIn = true;
    }

    public void FadeOut(float time, Interpolate.EaseType easeType)
    {
        this.isFadeIn = false;
        this.fadeTime1 = 0.0f;
        this.fadeTime2 = time;
        this.interpolateFunc = Interpolate.Ease(easeType);
        this.isFadeOut = true;
    }

    /// <summary>
    /// 페이드인,아웃 효과 프로세스
    /// </summary>
    public void DoFade(float time, AudioSource audio)
    {
        if (this.isFadeIn == true)
        {
            this.fadeTime1 += time;
            audio.volume = Interpolate.Ease(this.interpolateFunc, 0, maxVolume, fadeTime1, fadeTime2);
            if (this.fadeTime1 >= this.fadeTime2)
            {
                this.isFadeIn = false;
            }
        }
        else
        {
            this.fadeTime1 += time;
            audio.volume = Interpolate.Ease(this.interpolateFunc, maxVolume, 
                0 - this.maxVolume, fadeTime1, fadeTime2);
            if (this.fadeTime1 >= this.fadeTime2)
            {
                this.isFadeOut = false;
                audio.Stop();
            }
        }
    }
}

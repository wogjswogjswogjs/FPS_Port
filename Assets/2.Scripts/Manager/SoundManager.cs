using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class SoundManager : SingletonMonobehaviour<SoundManager>
{
   public const string MasterGroupName = "Master";
   public const string EffectGroupName = "Effect";
   public const string BGMGroupName = "BGM";
   public const string UIGroupName = "UI";
   public const string EffectVolumeParam = "Volume_Effect";
   public const string BGMVolumeParam = "Volume_BGM";
   public const string UIVolumeParam = "Volume_UI";
   public const string MixerName = "AudioMixer";
   public const string ContainerName = "SoundContainer";
   public const string FadeA = "FadeA";
   public const string FadeB = "FadeB";
   public const string UI = "UI";

   public enum MUSICPLAYINGTYPE
   {
      NONE = 0,
      SOURCEA = 1,
      SOURCEB = 2,
      ATOB = 3,
      BTOA = 4
   }

   public AudioMixer mixer = null;
   public Transform audioRoot = null;
   public AudioSource fadeA_audio = null;
   public AudioSource fadeB_audio = null;
   public AudioSource[] effect_audios = null;
   public AudioSource UI_audio = null;

   public float[] effect_PlayStartTime = null;
   private int EffectChannelCount = 5;
   private MUSICPLAYINGTYPE CURRENTPLAYINGTYPE = MUSICPLAYINGTYPE.NONE;
   private bool isTicking = false;
   private SoundClip currentSound = null;
   private SoundClip lastSound = null;
   private float minVolume = -80.0f;
   private float maxVolume = 0.0f;

   private void Start()
   {
      if (this.mixer == null)
      {
         this.mixer = ResourceManager.Load(MixerName) as AudioMixer;
      }

      if (this.audioRoot == null)
      {
         audioRoot = new GameObject(ContainerName).transform;
         audioRoot.SetParent(transform);
         audioRoot.localPosition = Vector3.zero;
      }

      if (fadeA_audio == null)
      {
         GameObject fadeA = new GameObject(FadeA, typeof(AudioSource));
         fadeA.transform.SetParent(audioRoot);
         this.fadeA_audio = fadeA.GetComponent<AudioSource>();
         this.fadeA_audio.playOnAwake = false;
      }

      if (fadeB_audio == null)
      {
         GameObject fadeB = new GameObject(FadeB, typeof(AudioSource));
         fadeB.transform.SetParent(audioRoot);
         this.fadeB_audio = fadeB.GetComponent<AudioSource>();
         this.fadeB_audio.playOnAwake = false;
      }

      if (UI_audio == null)
      {
         GameObject ui = new GameObject(UI, typeof(AudioSource));
         ui.transform.SetParent(audioRoot);
         this.UI_audio = ui.GetComponent<AudioSource>();
         this.UI_audio.playOnAwake = false;
      }

      if (this.effect_audios == null || this.effect_audios.Length == 0)
      {
         this.effect_PlayStartTime = new float[EffectChannelCount];
         this.effect_audios = new AudioSource[EffectChannelCount];
         for (int i = 0; i < EffectChannelCount; i++)
         {
            effect_PlayStartTime[i] = 0.0f;
            GameObject effect = new GameObject("Effect" + i.ToString(), typeof(AudioSource));
            effect.transform.SetParent(audioRoot);
            this.effect_audios[i] = effect.GetComponent<AudioSource>();
            this.effect_audios[i].playOnAwake = false;
         }
      }

      if (this.mixer != null)
      {
         this.fadeA_audio.outputAudioMixerGroup = mixer.FindMatchingGroups(BGMGroupName)[0];
         this.fadeB_audio.outputAudioMixerGroup = mixer.FindMatchingGroups(BGMGroupName)[0];
         this.UI_audio.outputAudioMixerGroup = mixer.FindMatchingGroups(UIGroupName)[0];
         for (int i = 0; i < this.effect_audios.Length; i++)
         {
            this.effect_audios[i].outputAudioMixerGroup = mixer.FindMatchingGroups(EffectGroupName)[0];
         }
      }
   }

   public void SetBGMVolume(float currentRatio)
   {
      currentRatio = Mathf.Clamp01(currentRatio);
      float volume = Mathf.Lerp(minVolume, maxVolume, currentRatio);
      this.mixer.SetFloat(BGMVolumeParam, volume);
      PlayerPrefs.SetFloat(BGMVolumeParam, volume);
   }

   public float GetBGMVolume()
   {
      if (PlayerPrefs.HasKey(BGMVolumeParam))
      {
         return Mathf.Lerp(minVolume, maxVolume, PlayerPrefs.GetFloat(BGMVolumeParam));
      }
      else
      {
         return maxVolume;
      }
   }

   public void SetEffectVolume(float currentRatio)
   {
      currentRatio = Mathf.Clamp01(currentRatio);
      float volume = Mathf.Lerp(minVolume, maxVolume, currentRatio);
      this.mixer.SetFloat(BGMVolumeParam, volume);
      PlayerPrefs.SetFloat(BGMVolumeParam, volume);
   }

   public float GetEffectVolume()
   {
      if (PlayerPrefs.HasKey(EffectVolumeParam))
      {
         return Mathf.Lerp(minVolume, maxVolume, PlayerPrefs.GetFloat(EffectVolumeParam));
      }
      else
      {
         return maxVolume;
      }
   }

   public void SetUIVolume(float currentRatio)
   {
      currentRatio = Mathf.Clamp01(currentRatio);
      float volume = Mathf.Lerp(minVolume, maxVolume, currentRatio);
      this.mixer.SetFloat(EffectVolumeParam, volume);
      PlayerPrefs.SetFloat(EffectVolumeParam, volume);
   }

   public float GetUIVolume()
   {
      if (PlayerPrefs.HasKey(UIVolumeParam))
      {
         return Mathf.Lerp(minVolume, maxVolume, PlayerPrefs.GetFloat(UIVolumeParam));
      }
      else
      {
         return maxVolume;
      }
   }

   void VolumeInit()
   {
      if (this.mixer != null)
      {
         this.mixer.SetFloat(BGMVolumeParam, GetBGMVolume());
         this.mixer.SetFloat(EffectVolumeParam, GetEffectVolume());
         this.mixer.SetFloat(UIVolumeParam, GetUIVolume());
      }
   }

   void PlayAudioSource(AudioSource source, SoundClip clip, float volume)
   {
      if (currentSound == null || clip == null)
      {
         return;
      }
      
      source.Stop();
      source.clip = clip.GetSound();
      source.volume = volume;
      source.loop = clip.hasLoop;
      source.pitch = clip.pitch;
      source.dopplerLevel = clip.dopplerLevel;
      source.rolloffMode = clip.rolloffMode;
      source.minDistance = clip.minDistance;
      source.maxDistance = clip.maxDistance;
      source.spatialBlend = clip.spartialBlend;
      source.Play();
   }

   void PlayAudioSourceAtPoint(SoundClip clip, Vector3 position, float volume)
   {
      AudioSource.PlayClipAtPoint(clip.GetSound(), position, volume);
   }

   public bool IsPlaying()
   {
      return (int)this.CURRENTPLAYINGTYPE > 0;
   }

   public bool IsDifferentSound(SoundClip clip)
   {
      if (clip == null) return false;
      if (currentSound != null && currentSound.soundID == clip.soundID && IsPlaying() &&
          currentSound.isFadeOut == false)
      {
         return false;
      }
      else
      {
         return true;
      }
   }

   private IEnumerator CheckProcess()
   {
      while (this.isTicking == true && IsPlaying() == true)
      {
         yield return new WaitForSeconds(0.05f);
         if (this.currentSound.HasLoop())
         {
            if (CURRENTPLAYINGTYPE == MUSICPLAYINGTYPE.SOURCEA)
            {
               currentSound.CheckLoop(fadeA_audio);
            }
            else if (CURRENTPLAYINGTYPE == MUSICPLAYINGTYPE.SOURCEB)
            {
               currentSound.CheckLoop(fadeB_audio);
            }
            else if (CURRENTPLAYINGTYPE == MUSICPLAYINGTYPE.ATOB)
            {
               this.lastSound.CheckLoop(this.fadeA_audio);
               this.currentSound.CheckLoop(this.fadeB_audio);
            }
            else if (CURRENTPLAYINGTYPE == MUSICPLAYINGTYPE.BTOA)
            {
               this.lastSound.CheckLoop(this.fadeB_audio);
               this.currentSound.CheckLoop(this.fadeA_audio);
            }
         }
      }
   }

   public void DoCheck()
   {
      StartCoroutine(CheckProcess());
   }

   public void FadeIn(SoundClip clip, float time, Interpolate.EaseType ease)
   {
      if (this.IsDifferentSound(clip))
      {
         
      }
   }

   private void Update()
   {
      
   }
}

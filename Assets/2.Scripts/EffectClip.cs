using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

/// <summary>
/// 이펙트 프리팹과 경로, 타입등의 속성 데이터를 가지고 있다.
/// 프리팹 사전로딩(PreLoad)기능을 갖고 있다 - 추후, 캐싱을 한 후 풀링을 위한 기능
/// 이펙트 인스턴스 기능을 갖고 있다 - 풀링과 연계해서 사용.
/// </summary>
public class EffectClip
{
    public int dataId = 0;

    public EffectType effectType = EffectType.NORMAL;
    public GameObject effectPrefab = null;
    // effectName은 프리팹의 이름이고 effectPath는 프리팹이 위치한 경로이다.
    public string effectPath = string.Empty;
    public string effectName = string.Empty;
    public string effectFullPath = string.Empty;

    public EffectClip()
    {
    }

    public void Preload()
    {
        this.effectFullPath = effectPath + effectName;
        if (effectFullPath != string.Empty && this.effectPrefab == null)
        {
            this.effectPrefab = ResourceManager.Load(effectFullPath) as GameObject;
        }
    }

    public void ReleaseEffect()
    {
        if (effectPrefab != null)
        {
            this.effectPrefab = null;
        }
    }
    
    /// <summary>
    /// 원하는 위치에 내가 원하는 이펙트를 인스턴스화한다
    /// </summary>
    public GameObject Instantiate(Vector3 pos)
    {
        if (this.effectPrefab == null)
        {
            this.Preload();
        }

        if (this.effectPrefab != null)
        {
            GameObject effect = GameObject.Instantiate(effectPrefab, pos, Quaternion.identity);
            return effect;
        }

        return null;
    }
}

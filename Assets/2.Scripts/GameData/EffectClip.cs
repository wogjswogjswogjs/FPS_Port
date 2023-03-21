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
    public int effectID = 0;
    public EffectType effectType = EffectType.NORMAL;
    public GameObject effectPrefab = null;
    // effectName은 프리팹의 이름이고 effectPath는 프리팹이 위치한 경로이다.
    public string effectPath = string.Empty; 
    public string effectName = string.Empty;

    public EffectClip()
    {
    }
    
    /// <summary>
    /// 이펙트를 로드하는 기능
    /// </summary>
    public void LoadEffect()
    {
        if (this.effectPath + effectName != string.Empty && this.effectPrefab == null)
        {
            this.effectPrefab = ResourceManager.Load(effectPath + effectName) as GameObject;
        }
    }
    
    /// <summary>
    /// 메모리를 해제하는 기능.
    /// </summary>
    public void ReleaseEffect()
    {
        if (this.effectPrefab != null)
        {
            this.effectPrefab = null;
        }
    }
    
    /// <summary>
    /// 원하는 위치에 이펙트를 인스턴스화 한다.
    /// </summary>
    public GameObject Instantiate(Vector3 pos)
    {
        if (this.effectPrefab == null)
        {
            this.LoadEffect();
        }

        GameObject effect = GameObject.Instantiate(effectPrefab, pos, Quaternion.identity);
        return effect;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonobehaviour<EffectManager>
{
    private Transform effectRoot = null;
    // 생성될 이펙트들은 이 컨테이너 안에 넣을것
    public const string ContainerName = "EffectContainer";
   
    void Start()
    {
        if (effectRoot == null)
        {
            effectRoot = new GameObject(ContainerName).transform;
            effectRoot.SetParent(transform);
        }
    }
    
    /// <summary>
    /// 원하는 position에 index이펙트를 생성해준다.
    /// </summary>
    public GameObject EffectOneShot(int index, Vector3 position)
    {
        EffectClip clip = DataManager.GetEffectData().GetEffect(index);
        GameObject effectInstance = clip.Instantiate(position);
        effectInstance.SetActive(true);
        return effectInstance;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 데이터들을 읽고,불러오고 담아두는 클래스
/// </summary>
public class DataManager : MonoBehaviour
{
    private static EffectData effectData = null;
    void Start()
    {
        if (effectData == null)
        {
            effectData = ScriptableObject.CreateInstance<EffectData>();
            effectData.LoadData();
        }
    }

    public static EffectData GetEffectData()
    {
        if (effectData == null)
        {
            effectData = ScriptableObject.CreateInstance<EffectData>();
            effectData.LoadData();
        }

        return effectData;
    }
}

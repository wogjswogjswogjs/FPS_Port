using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;

public class EffectTool : EditorWindow
{
    #region UI를 그리는데 필요한 변수들

    public int uiWidthLarge = 300;
    public int uiWidthMiddle = 200;
    private int selection = 0;
    private Vector2 scrollPosition1 = Vector2.zero;
    private Vector2 scrollPosition2 = Vector2.zero;
    
    // 이펙트 클립
    private GameObject effectSource = null;
    // 이펙트 데이터들
    private static EffectData effectData;

    #endregion

    [MenuItem("Tools/Effect Tool")]
    static void Init()
    {
        effectData = ScriptableObject.CreateInstance<EffectData>();
       // effectData.LoadData();

        EffectTool window = GetWindow<EffectTool>(false, "EffectTool");
    }

    private void OnGUI()
    {
        if (effectData == null)
        {
            return;
        }

        
    }
}

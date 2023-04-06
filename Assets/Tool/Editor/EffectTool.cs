using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlasticGui.Configuration.CloudEdition.Welcome;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using Object = System.Object;

public class EffectTool : EditorWindow
{
    #region UI를 그리는데 필요한 변수들

    public int uiWidthLarge = 300;
    public int uiWidthXLarge = 600;
    private int selection = 0;
    private Vector2 scrollPosition1 = Vector2.zero;
    private Vector2 scrollPosition2 = Vector2.zero;
    
    // 이펙트 데이터들, 스크립터블 오브젝트 인스턴스
    private static EffectData effectData;
    private EffectClip source;
    #endregion

    [MenuItem("Tools/Effect Tool")]
    static void Show()
    {
        effectData = ScriptableObject.CreateInstance<EffectData>();
        effectData.LoadData();
        
        EffectTool window = GetWindow<EffectTool>(false, "EffectTool");
        ((EditorWindow)window).Show();
        
    }

    private void OnGUI()
    {
        if (effectData == null) return;
        EditorGUILayout.BeginVertical();
        {
            //-----------------------------------------------------------------------------------
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Add", GUILayout.Width(uiWidthLarge)))
                {
                    effectData.AddData();
                }

                if (effectData.GetDataCount() > 1)
                {
                    if (GUILayout.Button("Remove", GUILayout.Width(uiWidthLarge)))
                    {
                        effectData.RemoveData(selection);
                        if (selection == effectData.dataNameList.Count)
                        {
                            selection -= 1;
                        }
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            //----------------------------------------------------------------------------------
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical(GUILayout.Width(uiWidthLarge));
                {
                    EditorGUILayout.Separator();
                    EditorGUILayout.BeginVertical("box");
                    {
                        scrollPosition1 = EditorGUILayout.BeginScrollView(scrollPosition1);
                        {
                            selection = GUILayout.SelectionGrid(selection, effectData.GetNameList().ToArray(), 1);
                            
                        }
                        EditorGUILayout.EndScrollView();
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();
                //-----------------------------------------------------------------------------------
                EditorGUILayout.BeginVertical();
                {
                    scrollPosition2 = EditorGUILayout.BeginScrollView(scrollPosition2);
                    {
                        if (effectData.GetDataCount() > 0)
                        {
                            EditorGUILayout.BeginVertical();
                            {
                                EditorGUILayout.Separator();

                                EffectClip effect = effectData.GetEffect(selection);
                                effect.effectPrefab = (GameObject)EditorGUILayout.ObjectField("Effect Source",
                                    effect.effectPrefab, typeof(GameObject),GUILayout.Width(uiWidthLarge));
                                EditorGUILayout.Separator();
                                effect.effectID = EditorGUILayout.IntField("Effect ID",
                                    effect.effectID,GUILayout.Width(uiWidthLarge));
                                effect.Effecttype = (EFFECTTYPE)EditorGUILayout.EnumPopup("Effect Type",
                                    effect.Effecttype, GUILayout.Width(uiWidthLarge));
                                if (effect.effectPrefab != null)
                                {
                                    effect.effectPath = EditorGUILayout.TextField("Effect Path",
                                        EditorHelper.GetPath(effect.effectPrefab), GUILayout.Width(uiWidthLarge));
                                
                                    effectData.dataNameList[selection] = EditorGUILayout.TextField("Effect Name",
                                        effect.effectPrefab.name, GUILayout.Width(uiWidthLarge));
                                    effect.effectName = effectData.dataNameList[selection];
                                }

                                
                                EditorGUILayout.Separator();
                            }
                            EditorGUILayout.EndVertical();
                        }
                    }
                    EditorGUILayout.EndScrollView();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            {
                if(GUILayout.Button("SaveData", GUILayout.Width(uiWidthXLarge)))
                {
                    effectData.SaveData();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        
    }
}

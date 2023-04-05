using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlasticGui.Configuration.CloudEdition.Welcome;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using Object = System.Object;

public class SoundTool : EditorWindow
{
    public int uiWidthLarge = 450;
    public int uiWidthMiddle = 300;
    public int uiWidthSmall = 200;
    private int selection = 0;
    private Vector2 scrollPosition1 = Vector2.zero;
    private Vector2 scrollPosition2 = Vector2.zero;
    private AudioClip soundSource;
    private static SoundData soundData;
    
    [MenuItem("Tools/Sound Tool")]
    static void Show()
    {
        soundData = ScriptableObject.CreateInstance<SoundData>();
        soundData.LoadData();
        
        SoundTool window = GetWindow<SoundTool>(false, "SoundTool");
        ((EditorWindow)window).Show();
        
    }

    private void OnGUI()
    {
        if (soundData == null) return;

        EditorGUILayout.BeginVertical();
        {
            UnityEngine.Object source = soundSource;
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Add", GUILayout.Width(uiWidthMiddle)))
                {
                    soundData.AddData();
                }

                if (soundData.GetDataCount() > 1)
                {
                    if (GUILayout.Button("Remove", GUILayout.Width(uiWidthMiddle)))
                    {
                        soundData.RemoveData(selection);
                        if (selection == soundData.dataNameList.Count)
                        {
                            selection -= 1;
                        }
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            soundSource = source as AudioClip;
            //------------------------------------------------------------------------
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical(GUILayout.Width(uiWidthMiddle));
                {
                    EditorGUILayout.Separator();
                    EditorGUILayout.BeginVertical("box");
                    {
                        scrollPosition1 = EditorGUILayout.BeginScrollView(scrollPosition1);
                        {
                            selection = GUILayout.SelectionGrid(selection, soundData.GetNameList().ToArray(), 1);
                            
                        }
                        EditorGUILayout.EndScrollView();
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();
                soundSource = source as AudioClip;

                EditorGUILayout.BeginVertical();
                {
                    scrollPosition2 = EditorGUILayout.BeginScrollView(scrollPosition2);
                    {
                        if (soundData.GetDataCount() > 0)
                        {
                            EditorGUILayout.BeginVertical();
                            {
                                SoundClip sound = soundData.GetSound(selection);
                                EditorGUILayout.Separator();
                                sound.soundPrefab = (AudioClip)EditorGUILayout.ObjectField("Sound Source",
                                    sound.soundPrefab, typeof(AudioClip),GUILayout.Width(uiWidthMiddle));
                                EditorGUILayout.Separator();
                                sound.soundID = EditorGUILayout.IntField("Sound ID",
                                    sound.soundID,GUILayout.Width(uiWidthMiddle));
                                sound.playType = (SoundPlayType)EditorGUILayout.EnumPopup("Sound Type",
                                    sound.playType, GUILayout.Width(uiWidthMiddle));
                                if (sound.soundPrefab != null)
                                {

                                    sound.soundPath = EditorGUILayout.TextField("Effect Path",
                                        EditorHelper.GetPath(sound.soundPrefab), GUILayout.Width(uiWidthMiddle));
                                    soundData.dataNameList[selection] = EditorGUILayout.TextField("Effect Name",
                                        sound.soundPrefab.name, GUILayout.Width(uiWidthMiddle));
                                    sound.soundName = soundData.dataNameList[selection];
                                    EditorGUILayout.Separator();

                                    sound.maxVolume = EditorGUILayout.FloatField("Max Volume",
                                   sound.maxVolume, GUILayout.Width(uiWidthMiddle));
                                    sound.hasLoop = EditorGUILayout.Toggle("hasLoop",
                                        sound.hasLoop, GUILayout.Width(uiWidthMiddle));
                                    sound.pitch = EditorGUILayout.Slider("Pitch",
                                        sound.pitch, -3.0f, 3.0f, GUILayout.Width(uiWidthMiddle));
                                    sound.rolloffMode = (AudioRolloffMode)EditorGUILayout.EnumPopup("Volume Rolloff",
                                        sound.rolloffMode, GUILayout.Width(uiWidthMiddle));
                                    sound.minDistance = EditorGUILayout.FloatField("min Distance",
                                        sound.minDistance, GUILayout.Width(uiWidthMiddle));
                                    sound.maxDistance = EditorGUILayout.FloatField("max Distance",
                                        sound.maxDistance, GUILayout.Width(uiWidthMiddle));
                                    sound.sparialBlend = EditorGUILayout.Slider("PanLevel",
                                        sound.sparialBlend, 0.0f, 1.0f, GUILayout.Width(uiWidthMiddle));
                                }

                                if (GUILayout.Button("Add Loop", GUILayout.Width(uiWidthMiddle)))
                                {
                                    soundData.soundClips[selection].AddLoop();
                                }

                                for (int i = 0; i < soundData.soundClips[selection].checkTime.Count; i++)
                                {
                                    EditorGUILayout.BeginVertical("box");
                                    {
                                        GUILayout.Label("Loop Step" + i, EditorStyles.boldLabel);
                                        if (GUILayout.Button("Remove", GUILayout.Width(uiWidthMiddle)))
                                        {
                                            soundData.soundClips[selection].RemoveLoop(i);
                                           
                                        }
                                       
                                        SoundClip sound1 = soundData.soundClips[selection];
                                        // 마지막 인덱스 데이터 삭제하면 에러발생함 수정해야함
                                        // 기능은 문제없음.
                                        sound1.checkTime[i] = EditorGUILayout.FloatField("check Time",
                                            sound1.checkTime[i], GUILayout.Width(uiWidthLarge));
                                        sound1.setTime[i] = EditorGUILayout.FloatField("set Time",
                                            sound1.setTime[i], GUILayout.Width(uiWidthLarge));
                                        
                                        

                                    }
                                    EditorGUILayout.EndVertical();;
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
            
            //------------------------------------------------------------------------
            EditorGUILayout.BeginHorizontal();
            {
                if(GUILayout.Button("SaveData", GUILayout.Width(uiWidthMiddle)))
                {
                    soundData.SaveData();
                }
            }
            EditorGUILayout.EndHorizontal();
            
        }
        EditorGUILayout.EndVertical();
    }
}

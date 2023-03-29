using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorHelper 
{
    // source를 오브젝트로 받는이유는, 이 함수에 들어갈 source가 오디오소스가될수도있고,
    // 이펙트 소스가 될 수도 있기때문에 캐스팅해서 사용하기 위함이다.
    public static void EditorToolTopLayer(BaseData data, ref int selection, ref UnityEngine.Object source, int uiWidth)
    {
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("ADD", GUILayout.Width(uiWidth)))
            {
                data.AddData();
            }

            if (data.GetDataCount() > 1)
            {
                if (GUILayout.Button("Remove", GUILayout.Width(uiWidth)))
                {
                    data.RemoveData(selection);
                }
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    public static void EditorToolListLayer(ref Vector2 scrollPosition, BaseData data,
        ref int selection, ref UnityEngine.Object source, int uiWidth)
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(uiWidth));
        {
            EditorGUILayout.Separator();
            EditorGUILayout.BeginVertical("box");
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                {
                    selection = GUILayout.SelectionGrid(selection, data.GetNameList().ToArray(), 1);
                        
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }

    public static string GetPath(UnityEngine.Object clip)
    {
        string retString = string.Empty;
        retString = AssetDatabase.GetAssetPath(clip);
        return null;
    }
}

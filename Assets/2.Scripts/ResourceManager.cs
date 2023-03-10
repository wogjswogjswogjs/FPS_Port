using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resources.Load를 래핑하는 클래스
/// 추 후, 어드레서블로 변경 생각중.
/// </summary>
public class ResourceManager 
{
    public static UnityEngine.Object Load(string path)
    {
        //지금은 리소스 로드지만 추후엔 어드레서블 로드로 변경됨.
        return Resources.Load(path);
    }
    
    //리소스를 GameObject로 반환하고 Instantiate하는 함수
    public static GameObject LoadAndInstantiate(string path)
    {
        UnityEngine.Object source = Load(path);
        if (source == null)
        {
            return null;
        }

        return GameObject.Instantiate(source) as GameObject;
    }
}

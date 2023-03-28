using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// data의 기본 클래스.
/// 공통적인 데이터를 가지고 있게 되는데, 이름만 현재 가지고 있음.
/// 데이터의 갯수와 이름의 목록 리스트를 얻을 수 있다.
/// </summary>
public class BaseData : ScriptableObject
{
   public const string dataDirectory = "/9.Resources/Resources/Data/";
   public List<string> dataNameList = new List<string>();
   
   public BaseData()
   {
   }
   
   public int GetDataCount()
   {
      int dataCount = 0;

      if (this.dataNameList !=null)
      {
         dataCount = this.dataNameList.Count;
      }

      return dataCount;
   }
   

   /// <summary>
   /// 툴에 출력하기 위한 이름 목록을 만들어주는 함수
   /// </summary>
   public List<string> GetNameList()
   {
      List<string> retList = new List<string>();
      if (dataNameList == null)
      {
         return retList;
      }
      retList = dataNameList.ToList();
      return retList;
   }
   

   public virtual int AddData()
   {
      return GetDataCount();
   }

   public virtual void RemoveData(int dataID)
   {
   }

   public virtual void Copy(int dataNumber)
   {
   }

}

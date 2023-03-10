using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// data의 기본 클래스.
/// 공통적인 데이터를 가지고 있게 되는데, 이름과 경로만 현재 가지고 있음.
/// 데이터의 갯수와 이름의 목록 리스트를 얻을 수 있다.
/// </summary>
public class BaseData : ScriptableObject
{
   // public List<string> nameList = null;
   public const string dataDirectory = "/9.Resources/Data/";
   
   // 밑의 dictionary처럼 아이템number와 name을 들고있는 상황과
   // 아이템 Data(Clip)과 name을 들고있는 상황 둘중 하나 고민중
   public Dictionary<int, string> dataNumberAndNameDic = new Dictionary<int, string>();
   
   public BaseData()
   {
   }

   /*public int GetDataCount()
   {
      int dataCount = 0;

      if (this.nameList != null)
      {
         dataCount = this.nameList.Count;
      }

      return dataCount;
   }*/

   public int GetDataCount()
   {
      int dataCount = 0;

      if (this.dataNumberAndNameDic != null)
      {
         dataCount = this.dataNumberAndNameDic.Count;
      }

      return dataCount;
   }

   /// <summary>
   /// 툴에 출력하기 위한 이름 목록을 만들어주는 함수
   /// </summary>
   /*public List<string> GetNameList(bool showID, string filterWord = "")
   {
      List<string> retList = new List<string>();
      
      if (this.nameList == null)
      {
         return null;
      }

      for (int i = 0; i < this.nameList.Count; i++)
      {
         if (filterWord != "")
         {
            if (nameList[i].ToLower().Contains(filterWord.ToLower()) == false)
            {
               continue;
            }
         }

         if (showID == true)
         {
            retList.Add(i.ToString() + " : " + nameList[i]);
         }

         retList.Add(nameList[i]);
      }

      return retList;
   }*/

   /// <summary>
   /// 툴에 출력하기 위한 이름 목록을 만들어주는 함수
   /// </summary>
   public List<string> GetNumberAndNameList(bool showId, int filterIndex = 0)
   {
      List<string> retList = new List<string>();

      if (this.dataNumberAndNameDic == null)
      {
         return null;
      }

      foreach (var item in dataNumberAndNameDic)
      {
         if (showId == true)
         {
            retList.Add(item.Key + " : " + item.Value);
         }
         else
         {
            retList.Add(item.Value);
         }
      }
        
      return retList;
   }

   public virtual int AddData(int dataNumber, string dataName)
   {
      return GetDataCount();
   }

   public virtual void RemoveData(int dataNumber)
   {
   }

   public virtual void Copy(int dataNumber)
   {
   }

}

using System.Collections.Generic;
using System.Data;
using UnityEditor.Rendering;
using UnityEngine;


/// <summary> 데이터가 변경되면 해당 키에 맞는 UI를 자동으로 업데이트하기 위한 추상 클래스입니다. </summary>
public abstract class DynamicUIObject : MonoBehaviour
{

    public string DynamicDataKey { get { return _DynamicDatakey; } }
    [SerializeField] protected string _DynamicDatakey;


    public abstract void UpdateUIData(object update_ui_data);

}

namespace GameDataSystem
{
   

    /// <summary> 데이터 테이블에서 가져온 데이터를 관리하는 클래스 </summary>
    public static class StaticGameDataSchema
    {
        static TextAsset RecipeDataTable = Resources.Load<TextAsset>("DataTable/RecipeDataTable");
        static TextAsset CommonCardDataTable = new TextAsset();
        static TextAsset SpecialCardDataTable = new TextAsset();
        static TextAsset TargetCardDataTable = new TextAsset();

        public readonly static RecipeDataBase RECIPE_DATA_BASE = new RecipeDataBase(RecipeDataTable);
        public readonly static CardDataBase CARD_DATA_BASE = new CardDataBase(CommonCardDataTable, SpecialCardDataTable, TargetCardDataTable);

    }



    /// <summary> 게임 중 동적으로 변하는 데이터를 관리하고,  </summary>
    /// <remarks> 데이터에 UIDynamicUIObject가 등록 되어있으면 자동으로 업데이트함 </remarks>
    public static class DynamicGameDataSchema
    {
        static Dictionary<string, object> DynamicDataBase = new Dictionary<string, object>();
        static Dictionary<string, List<DynamicUIObject>> DynamicUIDataBase = new Dictionary<string, List<DynamicUIObject>>(); // DynamicDataBase가 업데이트 했을때 같이 갱신할 UI




        /// <summary> 동적으로 변하는 데이터를 등록 </summary>
        public static void AddDynamicDataBase(string key, object data)
        {
           
            if (DynamicDataBase.TryAdd(key, data) == false)
            {
                Debug.Log("해당키에 이미 데이터가 존재");
            }

        }


        /// <summary> 등록되어 있는 데이터를 업데이트, 반드시 등록 되어있어야함 </summary>
        /// <remarks> data는 해당 key에 원래 저장돼 있던 값의 타입과 같아야 합니다. (기존 int 라면 int형을 넣어야함)</remarks>
        ///  <param name="key">업데이트 할 key값 </param> <param name="data">업데이트 할 data</param>
        public static void UpdateDynamicDataBase(string key, object data)
        {
            if (DynamicDataBase.ContainsKey(key))
            {

                //data의 타입이 기존 key value의 데이터 타입과 같을 때만 업데이트
                if (DynamicDataBase[key].GetType() == data.GetType())
                {
                    DynamicDataBase[key] = data;


                    // DynamicUIDataBase 에 같은 key값으로 등록이 되어있다면 UI Data도 같이 업데이트 
                    if (DynamicUIDataBase.ContainsKey(key))
                    {
                        int uicount = DynamicUIDataBase[key].Count;
                        for (int i = 0; i < uicount; i++)
                        {
                            DynamicUIDataBase[key][i].UpdateUIData(DynamicDataBase[key]);
                        }
                    }
                }
                return;
            }

            Debug.Log("key가 존재하지 않거나, DataType이 일치하지 않음");
        }


        /// <summary> 등록되어 있는 데이터에 DynamicUIObject연결 </summary>
        /// <param name="key">DynamicDataBase에 등록된 key값</param> <param name="data">등록할 DynamicUIObject 데이터</param>
        public static void AddDynamicUIDataBase(string key, DynamicUIObject data)
        {
            //DynamicDataBase에 해당 key가 있어야 DynamicUIDataBase에 등록 가능 
            //DynamicUIDataBase는 DynamicDataBase의 Data를 UI에 업데이트 시키기위해 존재
            if (!DynamicDataBase.ContainsKey(key)) { return; }

            //기존 key가 있으면 UI추가 등록
            if (DynamicUIDataBase.ContainsKey(key))
            {
                DynamicUIDataBase[key].Add(data);
                DynamicUIDataBase[key].Find(n => n == data).UpdateUIData(DynamicDataBase[key]);
               
                return;
            }

            //신규 생성
            if (DynamicUIDataBase.TryAdd(key, new List<DynamicUIObject>()))
            {
                DynamicUIDataBase[key].Add(data);
                
                //데이터가 들어오면 UI 한번 갱신    
                DynamicUIDataBase[key][0].UpdateUIData(DynamicDataBase[key]);
                
                return;
            }

            Debug.LogError("AddDynamicUIDataBase(key , value) : DynamicDataBase에 해당 key가 존재하지 않거나 해당 key의 데이터 타입이 일치하지 않음");
        }
        //데이터 변화시 UI갱신
        //데이터 전달에 필요한 동적 데이터 등록
    }
}

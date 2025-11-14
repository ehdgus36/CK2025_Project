using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using GameDataSystem.KeyCode;


using UnityEngine;


namespace GameDataSystem.KeyCode
{
    /// <summary> DynamicGameDataSchema에 기본적으로 들어가있는 동적데이터의 키값을 정리하는 클래스 </summary>
    public static class DynamicGameDataKeys
    {
        public static readonly string GOLD_DATA = "GOLD_DATA";
        public static readonly string UPGRADE_POINT_DATA = "UPGRADE_POINT_DATA";
        public static readonly string COMMON_CARD_DATA = "COMMON_CARD_DATA";
        public static readonly string SPECIAL_CARD_DATA = "SPECIAL_CARD_DATA";
        public static readonly string TARGET_CARD_DATA = "TARGET_CARD_DATA";

        public static readonly string ITME_DATA = "ITEM_DATA";
        

        public static readonly string STICKER_ITME_INVENTORY_DATA = "STICKER_ITME_INVENTORY_DATA";
        public static readonly string STRAP_ITME_INVENTORY_DATA = "STRAP_ITME_INVENTORY_DATA";
        public static readonly string STRING_ITME_INVENTORY_DATA = "STRING_ITME_INVENTORY_DATA";

        public static string ITEM_HOLD_DATA { get { return "ITEM_HOLD_DATA"; } }

        public static readonly string STAGE_DATA = "STAGE_DATA";
        public static readonly string PLAYER_UNIT_DATA = "PLAYER_HP_DATA";
        public static string SKILL_POINT_DATA { get { return "SKILL_POINT_DATA"; } }

        public static readonly string DACK_DATA = "DACK_DATA";

        public static readonly string RAND_ITEM_DATA = "RAND_ITEM_DATA";



    }
}



/// <summary> 데이터가 변경되면 해당 키에 맞는 UI를 자동으로 업데이트하기 위한 추상 클래스입니다. </summary>
public abstract class DynamicUIObject : MonoBehaviour
{

    public abstract string DynamicDataKey { get; }

    /// <summary> update_ui_data를 저장되어있는 데이터 타입과 동일하게 형변환 후 사용 </summary>
    public abstract void UpdateUIData(object update_ui_data);

    public void OnDestroy()
    {
        //UI 표시해주는 오브젝트 파괴시 자신의 UI데이터 지움
        GameDataSystem.DynamicGameDataSchema.RemoveDynamicUIDataBase(DynamicDataKey);
    }


}



namespace GameDataSystem
{

    public static class DataTableLoader
    {
        /// <summary> 가져온 문자열 데이터를 TextAsset으로 변경하여 반환</summary>
        public static TextAsset LoadTextAsset(string fileName)
        {
            fileName += ".csv";
            string path = Path.Combine(Application.streamingAssetsPath, "DataTable");
            path = Path.Combine(path, fileName);


            if (!File.Exists(path))
            {
                Debug.Log("파일 없음: " + path);
                return null;
            }

            string text = File.ReadAllText(path);
            
            
            // TextAsset 객체 생성
            TextAsset asset = new TextAsset(text);
            return asset;
        }
    }

    /// <summary> 데이터 테이블에서 가져온 데이터를 관리하는 클래스 </summary>
    public static class StaticGameDataSchema
    {
       
        private static TextAsset CardStatusDataTable = new TextAsset();
        private static TextAsset CardDataTable = DataTableLoader.LoadTextAsset("CardDataTable");
        private static TextAsset NoteDataTable = DataTableLoader.LoadTextAsset("NoteDataTable");
        private static TextAsset NoteGroupDataTable = DataTableLoader.LoadTextAsset("NoteGroupDataTable");
        private static TextAsset ShopDataTable = DataTableLoader.LoadTextAsset("ShopDataTable");

        private static TextAsset StickerItemDataTable = DataTableLoader.LoadTextAsset("StickerItemDataTable");
        private static TextAsset StrapItemDataTable = DataTableLoader.LoadTextAsset("StrapItemDataTable");
        private static TextAsset StringItemDataTable = DataTableLoader.LoadTextAsset("StringItemDataTable");

        private static TextAsset EnemyDataTable = DataTableLoader.LoadTextAsset("EnemyDataTable");


        // 카드의 기본 시작 덱이 설정되어있는 거


        private static CardDataBase _CARD_DATA_BASE = new CardDataBase(CardStatusDataTable, CardDataTable);
        private static NoteDataBase _NOTE_DATA_BASE = new NoteDataBase(NoteDataTable, NoteGroupDataTable);
        private static ShopDataBase _SHOP_DATA_BASE = new ShopDataBase(ShopDataTable);
        private static ItemDataBase _ITEM_DATA_BASE = new ItemDataBase(StickerItemDataTable, StrapItemDataTable, StringItemDataTable);
        private static EnemyDataBase _ENEMY_DATA_BASE = new EnemyDataBase(EnemyDataTable);

        
        // 읽기 전용으로 선언
        public static CardDataBase CARD_DATA_BASE => _CARD_DATA_BASE;
        public static NoteDataBase NOTE_DATA_BASE => _NOTE_DATA_BASE;
        public static ShopDataBase Shop_DATA_BASE => _SHOP_DATA_BASE;
        public static ItemDataBase ITEM_DATA_BASE => _ITEM_DATA_BASE;
        public static EnemyDataBase ENEMY_DATA_BASE => _ENEMY_DATA_BASE;


        public readonly static UnitData StartPlayerData = new UnitData();
        public readonly static TextAsset PlayerDeckDataTable = DataTableLoader.LoadTextAsset("PlayerDeckDataTable");

        //초기화 
        public static void Initialize()
        {
           
            CardStatusDataTable = new TextAsset();
            CardDataTable = DataTableLoader.LoadTextAsset("CardDataTable");
            NoteDataTable = new TextAsset();
            ShopDataTable = DataTableLoader.LoadTextAsset("ItemDataTable");

            
            _CARD_DATA_BASE = new CardDataBase(CardStatusDataTable, CardDataTable);
            //_NOTE_DATA_BASE = new NoteDataBase(NoteDataTable);
            _SHOP_DATA_BASE = new ShopDataBase(ShopDataTable);
        }
    }



    /// <summary> 게임 중 동적으로 변하는 데이터를 관리하고,  </summary>
    /// <remarks> 데이터에 UIDynamicUIObject가 등록 되어있으면 자동으로 업데이트함 </remarks>
    public static class DynamicGameDataSchema
    {
        // 유동적으로 변할 데이터 저장
        static Dictionary<string, object> DynamicDataBase = new Dictionary<string, object>();

        // DynamicDataBase가 업데이트 했을때 같이 갱신할 UI
        static Dictionary<string, List<DynamicUIObject>> DynamicUIDataBase = new Dictionary<string, List<DynamicUIObject>>(); 


        static DynamicGameDataSchema()
        {
            Initialize();
        }
        public static void NewGameDataInit()
        {
            DynamicDataBase.Clear();
            DynamicUIDataBase.Clear();
            Initialize();
        }

        static void Initialize()
        {
            //플레이어 기본 스펙정의
            StaticGameDataSchema.StartPlayerData.MaxHp = 1;
            StaticGameDataSchema.StartPlayerData.CurrentHp = StaticGameDataSchema.StartPlayerData.MaxHp;
            StaticGameDataSchema.StartPlayerData.DataKey = DynamicGameDataKeys.PLAYER_UNIT_DATA;

            UnitData playerData = new UnitData();
            playerData.MaxHp = StaticGameDataSchema.StartPlayerData.MaxHp;
            playerData.CurrentHp = playerData.MaxHp;
            playerData.DataKey = StaticGameDataSchema.StartPlayerData.DataKey;

            AddDynamicDataBase(DynamicGameDataKeys.GOLD_DATA, 100);
            AddDynamicDataBase(DynamicGameDataKeys.PLAYER_UNIT_DATA, playerData); 
            AddDynamicDataBase(DynamicGameDataKeys.STAGE_DATA, "1-1");
            AddDynamicDataBase(DynamicGameDataKeys.SKILL_POINT_DATA,7);

            AddDynamicDataBase("MapSave", "");

            //ITEM관련 초기화
            AddDynamicDataBase(DynamicGameDataKeys.STICKER_ITME_INVENTORY_DATA, new List<string>() { "0", "0", "0" });
            AddDynamicDataBase(DynamicGameDataKeys.STRAP_ITME_INVENTORY_DATA, new List<string>() { "0", "0", "0" });
            AddDynamicDataBase(DynamicGameDataKeys.STRING_ITME_INVENTORY_DATA, new List<string>() { "0", "0", "0" });

            AddDynamicDataBase(DynamicGameDataKeys.RAND_ITEM_DATA, new List<string>() { "It01", "It02", "It03", "It11", "It12", "It13", "It31", "It32", "It33" });
          

            AddDynamicDataBase(DynamicGameDataKeys.ITEM_HOLD_DATA, new List<string>() { "It01", "0", "0" });

            //기본 카드데이터 삽입
            List<string> CardCodes = new List<string>();

            for (int i = 0; i < CSVReader.Read(StaticGameDataSchema.PlayerDeckDataTable).Count; i++)
            {
                CardCodes.Add(CSVReader.Read(StaticGameDataSchema.PlayerDeckDataTable)[i]["Card_ID"].ToString()); //Csv 읽기
            }

            AddDynamicDataBase(DynamicGameDataKeys.DACK_DATA, CardCodes);
        }


        /// <summary> 동적으로 변하는 데이터를 등록 </summary>
        public static void AddDynamicDataBase(string key, object data)
        {
           
            if (DynamicDataBase.TryAdd(key, data) == false)
            {
                Debug.Log("해당키에 이미 데이터가 존재");
            }

        }

        /// <summary> 동적으로 변하는 데이터를 가져옴 성공하면 True반환  </summary>
        /// <remarks> data는 해당 key에 원래 저장돼 있던 값의 타입과 같아야 합니다. (기존 int 라면 int형을 넣어야함)</remarks>
        ///  <param name="key">가져올 할 key값 </param> <param name="data">가져온 데이터를 저장할 곳</param>
        public static bool LoadDynamicData<T>(string key , out T data)
        {
            if (DynamicDataBase.ContainsKey(key))
            { 
                data = (T)DynamicDataBase[key];
                if (DynamicUIDataBase.ContainsKey(key))
                {
                    int uicount = DynamicUIDataBase[key].Count;
                    for (int i = 0; i < uicount; i++)
                    {
                        DynamicUIDataBase[key][i].UpdateUIData(DynamicDataBase[key]);
                    }
                }

                return true;
            }

            Debug.LogError("가져갈 데이터 타입과 일치하지 않거나, key값이 존재하지 않음");
            data = default(T);
            return false;
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
                    if (data == null) return;

                    DynamicDataBase[key] = data;

                    // DynamicUIDataBase 에 같은 key값으로 등록이 되어있다면 UI Data도 같이 업데이트 
                    if (DynamicUIDataBase.ContainsKey(key))
                    {
                        int uicount = DynamicUIDataBase[key].Count;
                        for (int i = 0; i < uicount; i++)
                        {
                            if (DynamicUIDataBase[key][i].gameObject.activeSelf == true)
                            {
                                DynamicUIDataBase[key][i].UpdateUIData(DynamicDataBase[key]);
                            }
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

                //등록
                DynamicUIDataBase[key].Add(data);

                //등록후 한번 갱신
                data.UpdateUIData(DynamicDataBase[key]);
               
                return;
            }

            //신규 생성
            if (DynamicUIDataBase.TryAdd(key, new List<DynamicUIObject>()))
            {
                DynamicUIDataBase[key].Add(data);

                //데이터가 들어오면 UI 한번 갱신    
                data.UpdateUIData(DynamicDataBase[key]);
                
                return;
            }

            Debug.LogError("AddDynamicUIDataBase(key , value) : DynamicDataBase에 해당 key가 존재하지 않거나 해당 key의 데이터 타입이 일치하지 않음");
        }

        public static void RemoveDynamicDataBase(string key)
        {
            DynamicDataBase.Remove(key);
            DynamicUIDataBase.Remove(key);
        }

        public static void RemoveDynamicUIDataBase(string key)
        {
            DynamicUIDataBase.Remove(key);
        }

        public static void RemoveAllDynamicUIDataBase()
        {
            DynamicUIDataBase.Clear();
        }

        public static bool DynamicDataBaseContainsKey(string key)
        { 
            return DynamicDataBase.ContainsKey(key);
        }
        //데이터 변화시 UI갱신
        //데이터 전달에 필요한 동적 데이터 등록
    }
}

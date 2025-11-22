using System.Collections.Generic;
using UnityEngine;

public class ItemDataLoader : MonoBehaviour
{
    public int PCMaxHP_UP { get; private set; }
    public int FireDm_UP  { get; private set; }
    public int EnDm_Down  { get; private set; }
    public int EnDf_Down  { get; private set; }
    public int EnAF_Up    { get; private set; }


    public void LoadData()
    { 
    
        /// 아이템 정보 수정으로 인해 삭제
        //List<string> itemcodes = new List<string>();
        //ItemData data;

        //GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.ITME_DATA,out itemcodes);
        
        //for (int i = 0; i < itemcodes.Count; i++)
        //{
        //    GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(itemcodes[i], out data);

        //    PCMaxHP_UP += data.PCMaxHP_UP;
        //    FireDm_UP += data.FireDm_UP;
        //    EnDm_Down += data.EnDm_Down;
        //    EnDf_Down += data.EnDf_Down;
        //    EnAF_Up += 0;// 가라 나중에 구현 중요X
        //}
    
    }
}

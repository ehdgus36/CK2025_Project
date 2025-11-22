using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDataLoader : MonoBehaviour
{
    public int PCMaxHP_UP { get; private set; }
    public int FireDm_UP  { get; private set; }
    public int EnDm_Down  { get; private set; }
    public int EnDf_Down  { get; private set; }
    public int EnAF_Up    { get; private set; }



   
    public StickerItemData stickerData{ get; private set; }
    public StrapItemData strapData{ get; private set; }
    public StringItemData stringData{ get; private set; }

    

    public void LoadData()
    {
        stickerData = new StickerItemData();
        strapData = new StrapItemData();
        stringData = new StringItemData();

        List<string> itemcodes = new List<string>();

        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.ITEM_HOLD_DATA, out itemcodes);

        Debug.Log("아이템 장착 현황" + string.Join(" ," ,itemcodes));

        for (int i = 0; i < itemcodes.Count; i++)
        {
            object data = null;
            if (GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(itemcodes[i], out data))
            {
                if (data is StickerItemData)
                {
                    stickerData = (StickerItemData)data;
                }

                if (data is StrapItemData)
                {
                    strapData = (StrapItemData)data;
                }

                if (data is StringItemData)
                {
                    stringData = (StringItemData)data;
                }

            }

        }

    }
}

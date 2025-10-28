using UnityEngine;

public class StringItem : Item
{
    
    protected override void Initialized()
    {
        object data = null;
        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(ItemID, out data);
        StringItemData stringItemData = (StringItemData)data;


        ItemType = "string";
        ItemName = stringItemData.ItemNameKR;
        ItemDesc = stringItemData.ItemDes;
    }
}

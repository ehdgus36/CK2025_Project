using UnityEngine;

public class StrapItem : Item
{
    protected override void Initialized()
    {
         object data = null;

        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(ItemID, out data);
        StrapItemData strapItemData = (StrapItemData)data;

        ItemType = "strap";
        ItemName = strapItemData.ItemNameKR;
        ItemDesc = strapItemData.ItemDes;
    }
}

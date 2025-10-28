using UnityEngine;

public class StickerItem : Item
{
    protected override void Initialized()
    {

        object data = null;

        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(ItemID, out data);
        StickerItemData stickerItemData= (StickerItemData)data;
        
        ItemType = "string";
        ItemName = stickerItemData.ItemNameKR;
        ItemDesc = stickerItemData.ItemDes;
    }
}

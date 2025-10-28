using UnityEngine;
using UnityEngine.UI;

public class StrapItem : Item
{
    protected override void Initialized()
    {
         object data = null;

        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(ItemID, out data);
        StrapItemData strapItemData = (StrapItemData)data;
       
        string Path = "ItemImage/" + strapItemData.ItemImage;
        Sprite cardSprite = Resources.Load<Sprite>(Path);

        GetComponent<Image>().sprite = cardSprite;


        ItemType = "strap";
        ItemName = strapItemData.ItemNameKR;
        ItemDesc = strapItemData.ItemDes;
    }
}

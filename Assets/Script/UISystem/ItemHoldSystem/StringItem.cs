using UnityEngine;
using UnityEngine.UI;
public class StringItem : Item
{
    
    protected override void Initialized()
    {
        object data = null;
        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(ItemID, out data);
        StringItemData stringItemData = (StringItemData)data;

        string Path = "ItemImage/" + stringItemData.ItemImage;
        Sprite cardSprite = Resources.Load<Sprite>(Path);

        GetComponent<Image>().sprite = cardSprite;
       
        ItemType = "string";
        ItemName = stringItemData.ItemNameKR;
        ItemDesc = stringItemData.ItemDes;
    }
}

using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StickerItem : Item ,IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        RuntimeManager.PlayOneShot("event:/UI/Item_Stage/Item_Click");
    }

    protected override void Initialized()
    {
        Debug.Log("초기화 :" + ItemID);
        object data = null;

        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(ItemID, out data);
        StickerItemData stickerItemData= (StickerItemData)data;

        

        string Path = "ItemImage/" + stickerItemData.ItemImage;
        Sprite cardSprite = Resources.Load<Sprite>(Path);
        Debug.Log("초기화 :" + cardSprite.name);
        GetComponent<Image>().sprite = cardSprite;

        ItemType = "sticker";
        ItemName = stickerItemData.ItemNameKR;
        ItemDesc = stickerItemData.ItemDes;
    }
}

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

        object data = null;

        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(ItemID, out data);
        StickerItemData stickerItemData= (StickerItemData)data;

        string Path = "ItemImage/" + stickerItemData.ItemImage;
        Sprite cardSprite = Resources.Load<Sprite>(Path);

        GetComponent<Image>().sprite = cardSprite;

        ItemType = "string";
        ItemName = stickerItemData.ItemNameKR;
        ItemDesc = stickerItemData.ItemDes;
    }
}

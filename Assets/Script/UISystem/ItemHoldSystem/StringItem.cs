using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StringItem : Item, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        RuntimeManager.PlayOneShot("event:/UI/Item_Stage/Item_Click");
    }
    protected override void Initialized()
    {
        object data = null;
        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(ItemID, out data);
        StringItemData stringItemData = (StringItemData)data;

        GetComponent<RectTransform>().sizeDelta = new Vector2(115.2f, 115.2f);

        string Path = "ItemImage/" + stringItemData.ItemImage;
        Sprite cardSprite = Resources.Load<Sprite>(Path);

        GetComponent<Image>().sprite = cardSprite;
       
        ItemType = "string";
        ItemName = stringItemData.ItemNameKR;
        ItemDesc = stringItemData.ItemDes;
    }
}

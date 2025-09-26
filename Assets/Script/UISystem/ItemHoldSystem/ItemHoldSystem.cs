using Spine;
using UnityEngine;
using TMPro;

public class ItemHoldSystem : MonoBehaviour
{
     public SlotGroup HoldSlotGroup { get; private set; }

    // 0 : itemType , 1: itemName , 2: itemDescript
    [SerializeField] TextMeshProUGUI[] StickerDesc;
    [SerializeField] TextMeshProUGUI[] StrapDesc;
    [SerializeField] TextMeshProUGUI[] StringDesc;

    public void Start()
    {
        for (int i = 0; i < HoldSlotGroup.Getsloat().Length; i++)
        {
            HoldSlotGroup.Getsloat()[i].AddInsertEvent(HoldSlotInsertEvent);
            HoldSlotGroup.Getsloat()[i].AddRemoveEvent(UnHoldSlotInsertEvent);
        }

        HoldSlotGroup = GetComponent<SlotGroup>();

        StickerDesc[0].text = "";
        StickerDesc[1].text = "";
        StickerDesc[2].text = "";


        StrapDesc[0].text = "";
        StrapDesc[1].text = "";
        StrapDesc[2].text = "";


        StringDesc[0].text = "";
        StringDesc[1].text = "";
        StringDesc[2].text = "";

    }


    void HoldSlotInsertEvent(SlotUI target_slot)
    {
        //아이템 올릴때마다 설명창 업데이트

        Item data = target_slot.ReadData<Item>();
        switch (data)
        {
            case StickerItem item:
                StickerDesc[0].text = item.ItemType;
                StickerDesc[1].text = item.ItemName;
                StickerDesc[2].text = item.ItemDesc;

                break;

            case StrapItem item:
                StrapDesc[0].text = item.ItemType;
                StrapDesc[1].text = item.ItemName;
                StrapDesc[2].text = item.ItemDesc;

                break;

            case StringItem item:
                StringDesc[0].text = item.ItemType;
                StringDesc[1].text = item.ItemName;
                StringDesc[2].text = item.ItemDesc;

                break;

        }
    }

    void UnHoldSlotInsertEvent(SlotUI target_slot)
    {
        switch (target_slot)
        {
            case StickerSlot item:
                StickerDesc[0].text = "";
                StickerDesc[1].text = "";
                StickerDesc[2].text = "";

                break;

            case StrapSlot item:
                StrapDesc[0].text = "";
                StrapDesc[1].text = "";
                StrapDesc[2].text = "";

                break;

            case StringSlot item:
                StringDesc[0].text = "";
                StringDesc[1].text = "";
                StringDesc[2].text = "";

                break;

        }
    }
}

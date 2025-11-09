using Spine;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class ItemHoldSystem : MonoBehaviour
{
     public SlotGroup HoldSlotGroup { get; private set; }

    // 0 : itemType , 1: itemName , 2: itemDescript
    [SerializeField] TextMeshProUGUI[] StickerDesc;
    [SerializeField] TextMeshProUGUI[] StrapDesc;
    [SerializeField] TextMeshProUGUI[] StringDesc;

    [SerializeField] List<string> HoldData;

    public void Start()
    {
        HoldSlotGroup = GetComponent<SlotGroup>();

        for (int i = 0; i < HoldSlotGroup.Getsloat().Length; i++)
        {
            HoldSlotGroup.Getsloat()[i].AddInsertEvent(HoldSlotInsertEvent);
            HoldSlotGroup.Getsloat()[i].AddRemoveEvent(UnHoldSlotInsertEvent);
        }

       

        StickerDesc[0].text = "";
        StickerDesc[1].text = "";
        StickerDesc[2].text = "";


        StrapDesc[0].text = "";
        StrapDesc[1].text = "";
        StrapDesc[2].text = "";


        StringDesc[0].text = "";
        StringDesc[1].text = "";
        StringDesc[2].text = "";


        ItemHoldDataInitialize();
    }

    void ItemHoldDataInitialize()
    {
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.ITEM_HOLD_DATA , out HoldData);
        SlotUI[] slots = HoldSlotGroup.Getsloat();

        if (HoldData.Count == 0) return;

        //item 생성하는 코드
        for (int i = 0; i < slots.Length; i++)
        {
            GameObject itemobj = new GameObject("PlayerItem");
            itemobj.AddComponent<RectTransform>().sizeDelta = new Vector3(128f,128f);
            itemobj.AddComponent<Image>();
            itemobj.AddComponent<DragDropUI>();
            itemobj.AddComponent<CanvasGroup>();

            if (HoldData[i] == "0") { Destroy(itemobj); continue; }
            if (HoldData[i][2] == '0') 
            {
                itemobj.AddComponent<StickerItem>().Initialized(HoldData[i]);
                Debug.Log(HoldData[i]);
            }
            if (HoldData[i][2] == '1')
            { 
                itemobj.AddComponent<StrapItem>().Initialized(HoldData[i]);
                Debug.Log(HoldData[i]);
            }
            if (HoldData[i][2] == '3') 
            { 
                itemobj.AddComponent<StringItem>().Initialized(HoldData[i]);
                Debug.Log(HoldData[i]);
            }

            slots[i].InsertData(itemobj);
        }
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

    public void SaveData()
    {
        List<string> saveData = new List<string>();
        for (int i = 0; i < HoldSlotGroup.Getsloat().Length; i++)
        {
            if (HoldSlotGroup.Getsloat()[i].ReadData<Item>() == null)
            {
                saveData.Add("0");
            }
            else
            {
                Debug.Log("Savecode:"+ HoldSlotGroup.Getsloat()[i].ReadData<Item>().ItemCode);
                saveData.Add(HoldSlotGroup.Getsloat()[i].ReadData<Item>().ItemCode);
            }
            
        }

        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.ITEM_HOLD_DATA, saveData);
    
    }
}

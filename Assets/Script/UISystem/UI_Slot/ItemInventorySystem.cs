using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemInventorySystem : MonoBehaviour
{
    [SerializeField] SlotGroup _StickerItemSlotGroup;
    [SerializeField] SlotGroup _StrapItemSlotGroup;
    [SerializeField] SlotGroup _StringItemSlotGroup;

    [SerializeField] Button _StickerItemSlotButton;
    [SerializeField] Button _StrapItemSlotButton;
    [SerializeField] Button _StringItemSlotButton;

    [SerializeField] CanvasGroup _StickerGroup;
    [SerializeField] CanvasGroup _StrapGroup;
    [SerializeField] CanvasGroup _StringGroup;

    Sprite startStickerSprite;
    Sprite startStrapSprite;
    Sprite startStringSprite;

    

  
  
    private void Start()
    {
        //인벤토리 활성화 후 초기화
        _StickerItemSlotGroup.gameObject.SetActive(true);
        _StrapItemSlotGroup.gameObject.SetActive(true);
        _StringItemSlotGroup.gameObject.SetActive(true);

        SetUpItem();// 초기화


        //비활성화
        _StickerItemSlotGroup.gameObject.SetActive(false);
        _StrapItemSlotGroup.gameObject.SetActive(false);
        _StringItemSlotGroup.gameObject.SetActive(false);

        //버튼의 기본 시작이미지 설정
        startStickerSprite = (_StickerItemSlotButton.targetGraphic as Image).sprite;
        startStrapSprite = (_StrapItemSlotButton.targetGraphic as Image).sprite;
        startStringSprite = (_StringItemSlotButton.targetGraphic as Image).sprite;


        //아이템 장착칸 알파데이터 설정
        _StickerGroup.alpha = .5f;
        _StrapGroup.alpha = .5f; ;
        _StringGroup.alpha = .5f; ;


        _StickerItemSlotButton.onClick.AddListener(() =>{ 
            _StickerItemSlotGroup.gameObject.SetActive(true);
            _StrapItemSlotGroup.gameObject.SetActive(false);
            _StringItemSlotGroup.gameObject.SetActive(false);

            (_StickerItemSlotButton.targetGraphic as Image).sprite = _StickerItemSlotButton.spriteState.selectedSprite;
            (_StrapItemSlotButton.targetGraphic as Image).sprite = startStrapSprite;
            (_StringItemSlotButton.targetGraphic as Image).sprite = startStringSprite;
           
            _StickerGroup.alpha = 1f;
            _StrapGroup.alpha = .5f; ;
            _StringGroup.alpha = .5f;

            (_StickerItemSlotButton.targetGraphic as Image).SetNativeSize();
            (_StrapItemSlotButton.targetGraphic as Image).SetNativeSize();
            (_StringItemSlotButton.targetGraphic as Image).SetNativeSize();
        });

        _StrapItemSlotButton.onClick.AddListener(() => {
            _StickerItemSlotGroup.gameObject.SetActive(false);
            _StrapItemSlotGroup.gameObject.SetActive(true);
            _StringItemSlotGroup.gameObject.SetActive(false);

            (_StickerItemSlotButton.targetGraphic as Image).sprite = startStickerSprite;
            (_StrapItemSlotButton.targetGraphic as Image).sprite = _StrapItemSlotButton.spriteState.selectedSprite;
            (_StringItemSlotButton.targetGraphic as Image).sprite = startStringSprite;
           

            _StickerGroup.alpha = .5f;
            _StrapGroup.alpha = 1f; ;
            _StringGroup.alpha = .5f;

            (_StickerItemSlotButton.targetGraphic as Image).SetNativeSize();
            (_StrapItemSlotButton.targetGraphic as Image).SetNativeSize();
            (_StringItemSlotButton.targetGraphic as Image).SetNativeSize();
        });

        _StringItemSlotButton.onClick.AddListener(() => {
            _StickerItemSlotGroup.gameObject.SetActive(false);
            _StrapItemSlotGroup.gameObject.SetActive(false);
            _StringItemSlotGroup.gameObject.SetActive(true);

            (_StickerItemSlotButton.targetGraphic as Image).sprite = startStickerSprite;
            (_StrapItemSlotButton.targetGraphic as Image).sprite = startStrapSprite;
            (_StringItemSlotButton.targetGraphic as Image).sprite = _StringItemSlotButton.spriteState.selectedSprite;
           

            _StickerGroup.alpha = .5f;
            _StrapGroup.alpha = .5f; ;
            _StringGroup.alpha = 1f; ;

            (_StickerItemSlotButton.targetGraphic as Image).SetNativeSize();
            (_StrapItemSlotButton.targetGraphic as Image).SetNativeSize();
            (_StringItemSlotButton.targetGraphic as Image).SetNativeSize();
        });

        //처음 시작은 스티커 인벤토리로
        _StickerItemSlotButton.onClick.Invoke();



    }



    public void SetUpItem()
    {
        List<string> Data = new List<string>();

        if (_StickerItemSlotGroup.gameObject.activeSelf == true)
        {
            
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.STICKER_ITME_INVENTORY_DATA, out Data);

            Debug.Log("sticker : "+string.Join(", ", Data));
            CreateInventoryItem(_StickerItemSlotGroup, Data);
        }

        if (_StrapItemSlotGroup.gameObject.activeSelf == true)
        {
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.STRAP_ITME_INVENTORY_DATA, out Data);
            Debug.Log("strap : " + string.Join(", ", Data));
            CreateInventoryItem(_StrapItemSlotGroup, Data);
        }

        if (_StringItemSlotGroup.gameObject.activeSelf == true)
        {
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.STRING_ITME_INVENTORY_DATA, out Data);
            Debug.Log("string : " + string.Join(", ", Data));
            CreateInventoryItem(_StringItemSlotGroup, Data);
        }


       
    }

    void CreateInventoryItem(SlotGroup inventory , List<string> Data)
    {

        for (int i = 0; i < Data.Count; i++)
        { 
            if (Data[i] == "0") continue; 

            GameObject itemobj = new GameObject("PlayerInvenItem");
            itemobj.AddComponent<RectTransform>().sizeDelta = new Vector2(128f, 128f);
            itemobj.AddComponent<Image>();
            itemobj.AddComponent<DragDropUI>();
            itemobj.AddComponent<CanvasGroup>();

            if (Data[i][2] == '0')
            {
                itemobj.AddComponent<StickerItem>().Initialized(Data[i]);
                Debug.Log(Data[i]);
            }
            if (Data[i][2] == '1')
            {
                itemobj.AddComponent<StrapItem>().Initialized(Data[i]);
                Debug.Log(Data[i]);
            }
            if (Data[i][2] == '3')
            {
                itemobj.AddComponent<StringItem>().Initialized(Data[i]);
                Debug.Log(Data[i]);
            }

            inventory.InsertData(itemobj);
        }
    }

    public void SaveInventory()
    {
        List<string> inventoryData = new List<string>();


        for (int i = 0; i < _StickerItemSlotGroup.Getsloat().Length; i++)
        {
            inventoryData.Add(_StickerItemSlotGroup.Getsloat()[i].ReadData<Item>() != null ? _StickerItemSlotGroup.Getsloat()[i].ReadData<Item>().ItemCode : "0");
            Debug.Log(_StickerItemSlotGroup.Getsloat()[i].ReadData<Item>() != null ? _StickerItemSlotGroup.Getsloat()[i].ReadData<Item>().ItemCode : "0");
        }

        Debug.Log("stickerInvan : " + string.Join(", ", inventoryData));

        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STICKER_ITME_INVENTORY_DATA, inventoryData);


        inventoryData = new List<string>();

        for (int i = 0; i < _StrapItemSlotGroup.Getsloat().Length; i++)
        {
            inventoryData.Add(_StrapItemSlotGroup.Getsloat()[i].ReadData<Item>() != null ? _StrapItemSlotGroup.Getsloat()[i].ReadData<Item>().ItemCode :"0");
        }

        Debug.Log("strapInvan : " + string.Join(", ", inventoryData));
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STRAP_ITME_INVENTORY_DATA, inventoryData);


        inventoryData = new List<string>();

        for (int i = 0; i < _StringItemSlotGroup.Getsloat().Length; i++)
        {
            inventoryData.Add(_StringItemSlotGroup.Getsloat()[i].ReadData<Item>() != null ? _StringItemSlotGroup.Getsloat()[i].ReadData<Item>().ItemCode : "0");
        }

        Debug.Log("stringInvan : " + string.Join(", ", inventoryData));
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STRING_ITME_INVENTORY_DATA, inventoryData);

    }

}

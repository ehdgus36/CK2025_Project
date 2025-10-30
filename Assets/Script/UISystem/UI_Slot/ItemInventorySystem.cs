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

    [SerializeField] List<string> Data = new List<string>();

  
  
    private void Start()
    {
        _StickerItemSlotGroup.gameObject.SetActive(false);
        _StrapItemSlotGroup.gameObject.SetActive(false);
        _StringItemSlotGroup.gameObject.SetActive(false);

        startStickerSprite = (_StickerItemSlotButton.targetGraphic as Image).sprite;
        startStrapSprite = (_StrapItemSlotButton.targetGraphic as Image).sprite;
        startStringSprite = (_StringItemSlotButton.targetGraphic as Image).sprite;


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
            SetUpItem();
            _StickerGroup.alpha = 1f;
            _StrapGroup.alpha = .5f; ;
            _StringGroup.alpha = .5f; ;
        });
        _StrapItemSlotButton.onClick.AddListener(() => {
            _StickerItemSlotGroup.gameObject.SetActive(false);
            _StrapItemSlotGroup.gameObject.SetActive(true);
            _StringItemSlotGroup.gameObject.SetActive(false);

            (_StickerItemSlotButton.targetGraphic as Image).sprite = startStickerSprite;
            (_StrapItemSlotButton.targetGraphic as Image).sprite = _StrapItemSlotButton.spriteState.selectedSprite;
            (_StringItemSlotButton.targetGraphic as Image).sprite = startStringSprite;
            SetUpItem();

            _StickerGroup.alpha = .5f;
            _StrapGroup.alpha = 1f; ;
            _StringGroup.alpha = .5f; ;
        });
        _StringItemSlotButton.onClick.AddListener(() => {
            _StickerItemSlotGroup.gameObject.SetActive(false);
            _StrapItemSlotGroup.gameObject.SetActive(false);
            _StringItemSlotGroup.gameObject.SetActive(true);

            (_StickerItemSlotButton.targetGraphic as Image).sprite = startStickerSprite;
            (_StrapItemSlotButton.targetGraphic as Image).sprite = startStrapSprite;
            (_StringItemSlotButton.targetGraphic as Image).sprite = _StringItemSlotButton.spriteState.selectedSprite;
            SetUpItem();

            _StickerGroup.alpha = .5f;
            _StrapGroup.alpha = .5f; ;
            _StringGroup.alpha = 1f; ;
        });
        _StickerItemSlotButton.onClick.Invoke();



    }



    public void SetUpItem()
    {
        if (_StickerItemSlotGroup.gameObject.activeSelf == true)
        {
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.STICKER_ITME_INVENTORY_DATA, out Data);

        }

        if (_StrapItemSlotGroup.gameObject.activeSelf == true)
        {
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.STRAP_ITME_INVENTORY_DATA, out Data);
        }

        if (_StringItemSlotGroup.gameObject.activeSelf == true)
        {
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.STRING_ITME_INVENTORY_DATA, out Data);
        }


        (_StickerItemSlotButton.targetGraphic as Image).SetNativeSize();
        (_StrapItemSlotButton.targetGraphic as Image).SetNativeSize();
        (_StringItemSlotButton.targetGraphic as Image).SetNativeSize();
    }

    public void SaveInventory()
    {
        List<string> inventoryData = new List<string>();


        for (int i = 0; i < _StickerItemSlotGroup.ReadData<Item>().Count; i++)
        {
            inventoryData.Add(_StickerItemSlotGroup.ReadData<Item>()[i].ItemCode);
        }

        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STICKER_ITME_INVENTORY_DATA, inventoryData);


        inventoryData = new List<string>();

        for (int i = 0; i < _StrapItemSlotGroup.ReadData<Item>().Count; i++)
        {
            inventoryData.Add(_StickerItemSlotGroup.ReadData<Item>()[i].ItemCode);
        }

        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STRAP_ITME_INVENTORY_DATA, inventoryData);


        inventoryData = new List<string>();

        for (int i = 0; i < _StringItemSlotGroup.ReadData<Item>().Count; i++)
        {
            inventoryData.Add(_StickerItemSlotGroup.ReadData<Item>()[i].ItemCode);
        }

        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STRING_ITME_INVENTORY_DATA, inventoryData);







        
       

    }

}

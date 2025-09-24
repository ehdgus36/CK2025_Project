using UnityEngine;
using UnityEngine.UI;

public class ItemInventorySystem : MonoBehaviour
{
    [SerializeField] SlotGroup _StickerItemSlotGroup;
    [SerializeField] SlotGroup _StrapItemSlotGroup;
    [SerializeField] SlotGroup _StringItemSlotGroup;

    [SerializeField] Button _StickerItemSlotButton;
    [SerializeField] Button _StrapItemSlotButton;
    [SerializeField] Button _StringItemSlotButton;

    





    private void Start()
    {
        _StickerItemSlotGroup.gameObject.SetActive(false);
        _StrapItemSlotGroup.gameObject.SetActive(false);
        _StringItemSlotGroup.gameObject.SetActive(false);

        _StickerItemSlotButton.onClick.AddListener(() =>{ 
            _StickerItemSlotGroup.gameObject.SetActive(true);
            _StrapItemSlotGroup.gameObject.SetActive(false);
            _StringItemSlotGroup.gameObject.SetActive(false);
            SetUpItem();
        });
        _StrapItemSlotButton.onClick.AddListener(() => {
            _StickerItemSlotGroup.gameObject.SetActive(false);
            _StrapItemSlotGroup.gameObject.SetActive(true);
            _StringItemSlotGroup.gameObject.SetActive(false);
            SetUpItem();
        });
        _StringItemSlotButton.onClick.AddListener(() => {
            _StickerItemSlotGroup.gameObject.SetActive(false);
            _StrapItemSlotGroup.gameObject.SetActive(false);
            _StringItemSlotGroup.gameObject.SetActive(true);
            SetUpItem();
        });
        _StickerItemSlotButton.onClick.Invoke();



    }



    public void SetUpItem()
    {
       

        if (_StickerItemSlotGroup.gameObject.activeSelf == true)
        { 
        
        
        }

        if (_StrapItemSlotGroup.gameObject.activeSelf == true)
        {

        }

        if (_StringItemSlotGroup.gameObject.activeSelf == true)
        {

        }
    }
}

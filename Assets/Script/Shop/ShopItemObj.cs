using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ShopItemObj :MonoBehaviour, IPointerDownHandler
{

    [SerializeField] string ItemID;
    [SerializeField] string ItemType;
    [SerializeField] TextMeshProUGUI ItemNameText;
    [SerializeField] TextMeshProUGUI ItemPriceText;

    [HideInInspector] public ShopEvent ShopEvent;

    

    public void OnPointerDown(PointerEventData eventData)
    {
        ShopEvent.SelectItemID = ItemID;
        if (ItemType == "Tape") ShopEvent.SelectTape(ShopEvent.TapeList.IndexOf(this));
        if (ItemType == "Peak") ShopEvent.SelectPeak(ShopEvent.PeakList.IndexOf(this));

        
    }
}

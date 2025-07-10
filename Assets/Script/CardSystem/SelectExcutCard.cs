using UnityEngine;
using UnityEngine.EventSystems;

public class SelectExcutCard : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] SlotUI CardSlot;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CardSlot.ReadData<Card>() == null) return;
        

        GameManager.instance.ExcutSelectCardSystem.SetSelectCard(CardSlot.ReadData<Card>());
    }
}

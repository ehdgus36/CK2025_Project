using UnityEngine;
using UnityEngine.EventSystems;

public class SelectExcutCard : MonoBehaviour, IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] SlotUI CardSlot;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CardSlot.ReadData<Card>() == null) return;
        
        GameManager.instance.ExcutSelectCardSystem.SetSelectCard(CardSlot.ReadData<Card>());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardSlot.ReadData<Card>() == null) return;

        if (CardSlot.ReadData<Card>().cardData.Range_Type == 3)
        {
            GameManager.instance.DimBackGroundObject.SetActiveDim("Enemy");//enemy¸¦ ¾îµÓ°Ô
        }
        else
        {
            GameManager.instance.DimBackGroundObject.SetActiveDim("Player");
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.instance.ExcutSelectCardSystem.IsSelectCard == false)
        {
            GameManager.instance.DimBackGroundObject.gameObject.SetActive(false);
        }
    }

   
}

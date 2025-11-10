using UnityEngine;
using UnityEngine.EventSystems;

public class SelectExcutCard : MonoBehaviour, IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler, IDragHandler
{
    [SerializeField] SlotUI CardSlot;
    [SerializeField] EffectSystem EffectSystem;

    private Canvas canvas;

    Card card;

    public void OnDrag(PointerEventData eventData)
    {
       

       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CardSlot.ReadData<Card>() == null) return;
        if (GameManager.instance.ExcutSelectCardSystem.CurrentMana == 0) return;
        card = CardSlot.ReadData<Card>();


        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/UI/Card_Click");
        GameManager.instance.ExcutSelectCardSystem.SetSelectCard(card);
        canvas = GetComponentInParent<Canvas>();


        card?.EffectSystem?.PlayEffect("CardHold_Effect", card.transform, new Vector3(62,62,62));


        GetComponent<HoverEffectUI>()?.HoldEffect(true);
        GetComponent<RectTransform>().sizeDelta = new Vector2(2100f, 300f);

    }

 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardSlot.ReadData<Card>() == null) return;


        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/UI/Card_Over");

        if (CardSlot.ReadData<Card>().cardData.Target_Type == "1")
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
            card?.EffectSystem?.StopEffect("CardHold_Effect");
            GetComponent<HoverEffectUI>()?.HoldEffect(false);
            GetComponent<RectTransform>().sizeDelta = new Vector2(150f, 150f);
        }
    }

  
}

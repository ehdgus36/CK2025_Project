using UnityEngine;
using UnityEngine.EventSystems;

public class SelectExcutCard : MonoBehaviour, IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] SlotUI CardSlot;
    [SerializeField] EffectSystem EffectSystem;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (CardSlot.ReadData<Card>() == null) return;
        
        GameManager.instance.ExcutSelectCardSystem.SetSelectCard(CardSlot.ReadData<Card>());
        ;
        EffectSystem.PlayEffect("CardHold_Effect" , new Vector3(CardSlot.ReadData<Card>().transform.position.x,
                                                                -4.733334f,
                                                                CardSlot.ReadData<Card>().transform.position.z));
        GetComponent<HoverEffectUI>()?.HoldEffect(true);
        GetComponent<RectTransform>().sizeDelta = new Vector2(2100f, 300f);

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
            EffectSystem.StopEffect("CardHold_Effect");
            GetComponent<HoverEffectUI>()?.HoldEffect(false);
            GetComponent<RectTransform>().sizeDelta = new Vector2(150f, 150f);
        }
    }

   
}

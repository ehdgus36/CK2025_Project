using UnityEngine;
using UnityEngine.EventSystems;

public class SelectExcutCard : MonoBehaviour, IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] SlotUI CardSlot;
    [SerializeField] EffectSystem EffectSystem;

    private Canvas canvas;

    Card card;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (CardSlot.ReadData<Card>() == null) return;
        if (GameManager.instance.ExcutSelectCardSystem.CurrentMana == 0 && CardSlot.ReadData<SkillCard>() == null) return;
        card = CardSlot.ReadData<Card>();


        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/UI/Card_Click");
        GameManager.instance.ExcutSelectCardSystem.SetSelectCard(card);
        canvas = GetComponentInParent<Canvas>();


        string cardEffecCode = "";

        if (card.cardData.Card_Rank == 0) cardEffecCode = "CardHold_Effect";
        if (card.cardData.Card_Rank == 1) cardEffecCode = "CardHold_Effect";
        if (card.cardData.Card_Rank == 2) cardEffecCode = "CardHold_Effect_Epic";
        if (card.cardData.Card_Rank == 3) cardEffecCode = "CardHold_Effect_Legend";

        card?.EffectSystem?.PlayEffect(cardEffecCode, card.transform, new Vector3(62,62,62));


        GetComponent<HoverEffectUI>()?.HoldEffect(true);
        GetComponent<RectTransform>().sizeDelta = new Vector2(2100f, 300f);

        if (CardSlot.ReadData<SkillCard>() == true) // 스킬은 전용사운드 출력
        {
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/UI/Skill_Click");
        }
    }

 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardSlot.ReadData<Card>() == null) return;


        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/UI/Card_Over");

        if (CardSlot.ReadData<Card>().cardData.Target_Type == "1")
        {
            GameManager.instance.DimBackGroundObject.SetActiveDim("Enemy");//enemy를 어둡게
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
            card?.EffectSystem?.StopEffect("CardHold_Effect_Epic");
            card?.EffectSystem?.StopEffect("CardHold_Effect_Legend");


            
            GetComponent<HoverEffectUI>()?.HoldEffect(false);
            GetComponent<RectTransform>().sizeDelta = new Vector2(150f, 150f);
        }
    }

  
}

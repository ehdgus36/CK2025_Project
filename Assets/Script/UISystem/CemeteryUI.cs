using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Spine;

public class CemeteryUI : MonoBehaviour,IDropHandler
{
    [SerializeField] public Transform CemeteryPos;
    [SerializeField] public Transform MovePos;
    [SerializeField] List<Card> CemeteryCard;

    public List<Card> CemeteryCardList { get => CemeteryCard; }

    EffectSystem effectSystem;
   

    public List<Card> GetCemeteryCards()
    {
        GameManager.instance.UIManager.CardCemeteryUI.UpdateUI(CemeteryCard.Count);
        return CemeteryCard;
    }


    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag.gameObject.GetComponent<Card>())
        {
            Insert(eventData.pointerDrag.gameObject.GetComponent<Card>());         
        }
       
    }

    public void ReflashInsert(Card card)
    {
        card.transform.SetParent(CemeteryPos.transform);

        card.transform.position = CemeteryPos.position;
        card.transform.localScale = Vector3.one;

        //카드 묘지에 넣기
        CemeteryCard.Add(card);
        card.gameObject.SetActive(true);
        GameManager.instance.UIManager.CardCemeteryUI.UpdateUI(CemeteryCard.Count);
    }

    public void Insert(Card card)
    {
        if (effectSystem == null)
        {
            effectSystem = GetComponent<EffectSystem>();
        }

        

       

        //카드 묘지 이동효과 출력


        card.transform.SetParent(CemeteryPos.transform);
        StartCoroutine(MoveCardEffect( MovePos.GetComponent<RectTransform>(), card));


        card.gameObject.SetActive(true); 
    }

    IEnumerator MoveCardEffect( RectTransform UItargetPos , Card card)
    {

        float t = 0;

        Vector3 targetPos = card.transform.localPosition + new Vector3(0f, 200f, 0f);

        // 이펙트 생성
        GameObject CardSelectEffect = effectSystem.EffectObject("CardHold_Effect", card.transform.position);

        // 신규 만든거 기획서 시스템
        for (int i = 0; i < 10; i++)
        {
            t += .1f;
            card.transform.localPosition = Vector3.Lerp(card.transform.localPosition, targetPos, t);
            CardSelectEffect.transform.position = card.transform.position;
            yield return new WaitForSeconds(.02f);
        }
        yield return new WaitForSeconds(.4f);


        //지우는 이펙트
        effectSystem.StopEffect("CardHold_Effect");
        effectSystem.PlayUIEffect("CardRemove_Effect", card.GetComponent<RectTransform>());


        yield return new WaitForSeconds(0.2f);
        effectSystem.StopEffect("CardRemove_Effect");

        //이동이펙트
        GameObject moveEffect = effectSystem.UIEffectObject("CardMove_Effect", card.GetComponent<RectTransform>());
        Transform moveTarget = moveEffect.transform;
        moveTarget.transform.position = card.transform.position;
        moveEffect.SetActive(true);


        t = 0;

        for (int i = 0; i < 10; i++)
        {
            t += 0.1f;

            card.transform.localScale = Vector3.Lerp(card.transform.localScale, Vector3.zero, t);
            card.transform.eulerAngles = Vector3.Lerp(card.transform.eulerAngles, new Vector3(0,0,180f), t);
            yield return new WaitForSeconds(.02f);
        }

        t = 0;
        for (int i = 0; i < 20; i++)
        {
            t += 0.05f;

            moveTarget.transform.position = Vector3.Lerp(moveTarget.transform.position, UItargetPos.transform.position, t);
            yield return new WaitForSeconds(.025f);
        }


        //도착하면 묘지 카드 위치로전송
        card.transform.position = CemeteryPos.position;
        card.transform.localScale = Vector3.one;

        //카드 묘지에 넣기
        CemeteryCard.Add(card);
        card.gameObject.SetActive(true);
        GameManager.instance.UIManager.CardCemeteryUI.UpdateUI(CemeteryCard.Count);

        moveTarget.gameObject.SetActive(false);

    }


    public void Insert(List <Card> cards)
    {
        Card card = null;

        for (int i = 0; i < cards.Count; i++)
        {
            card = cards[i];

            card.transform.position = CemeteryPos.position;
            card.transform.SetParent(CemeteryPos);

            CemeteryCard.Add(card);
            card.gameObject.SetActive(true);
        }
        GameManager.instance.UIManager.CardCemeteryUI.UpdateUI(CemeteryCard.Count);
    }
}

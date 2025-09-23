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

    EffectSystem effectSystem;
   

    public List<Card> GetCemeteryCards()
    {
        return CemeteryCard;
    }


    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag.gameObject.GetComponent<Card>())
        {
            Insert(eventData.pointerDrag.gameObject.GetComponent<Card>());         
        }
       
    }

    public void Insert(Card card)
    {
        if (effectSystem == null)
        {
            effectSystem = GetComponent<EffectSystem>();
        }

        effectSystem.PlayUIEffect("CardRemove_Effect", card.GetComponent<RectTransform>());

        GameObject moveEffect = effectSystem.UIEffectObject("CardMove_Effect", card.GetComponent<RectTransform>());

        moveEffect.SetActive(true);

        //카드 묘지 이동효과 출력
        card.transform.SetParent(CemeteryPos.transform);
        StartCoroutine(MoveCardEffect(moveEffect.transform, MovePos.GetComponent<RectTransform>(), card));


        card.gameObject.SetActive(true); 
    }

    IEnumerator MoveCardEffect(Transform moveTarget, RectTransform UItargetPos , Card card)
    {      
        Camera uiCamera = GameManager.instance.Shake.gameObject.GetComponent<Camera>(); // Canvas에 지정된 Camera

        Vector3 targetPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            UItargetPos,
            RectTransformUtility.WorldToScreenPoint(uiCamera, UItargetPos.position),
            uiCamera,
            out targetPos
        );



        float t = 0;

        for (int i = 0; i < 10; i++)
        {
            t += 0.1f;

            card.transform.localScale = Vector3.Lerp(card.transform.localScale, Vector3.zero, t);
            card.transform.eulerAngles = Vector3.Lerp(card.transform.eulerAngles, new Vector3(0,0,180f), t);
            yield return new WaitForSeconds(.01f);
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

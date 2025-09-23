using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class HoverEffectUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    [Range(1,2)]float hoverScale = 1f;

    [SerializeField] CardDescription cardDescription;
    [SerializeField] bool isHoverEffect = true;
   

    Vector3 StartScale;
    Vector3 StartPos;

    int layerind;
    bool isHold = false;


    Coroutine HoverEffectCoroutine = null;


    

    private void Start()
    {
        StartScale = Vector3.one;


        StartPos = Vector3.zero;
        layerind = this.transform.GetSiblingIndex();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cardDescription == null)
        {
            cardDescription = GameManager.instance.UIManager.CardDescription;
        }

        if (HoverEffectCoroutine != null) StopCoroutine(HoverEffectCoroutine);

        HoverEffectCoroutine = StartCoroutine(HoverEffect(StartPos + new Vector3(0f,100f,0f) , StartScale * hoverScale));

        

        this.transform.SetAsLastSibling();

        Card card = GetComponent<SlotUI>().ReadData<Card>();
      

        if (card != null)
        {
            //cardDescription.UpdateDescription(card.DescSprite); 구버전
            cardDescription.UpdateDescription(card.cardData.Card_Name_KR, card.cardData.Card_Des, this.transform.position);
            cardDescription.gameObject.SetActive(true);
            
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/UI/Card_UI/Card_Mouse_UP");
        }

    }

    



    public void OnPointerExit(PointerEventData eventData)
    {

        if (isHold == false)
        {
            //호버 이펙트 실행
            if (HoverEffectCoroutine != null) StopCoroutine(HoverEffectCoroutine);

           HoverEffectCoroutine = StartCoroutine(HoverEffect(StartPos, StartScale));

            this.transform.SetSiblingIndex(layerind);
            cardDescription.gameObject.SetActive(false);
        }

       
        
    }

    public void HoldEffect(bool isHolding)
    {
        isHold = isHolding;

        if (isHold == false) OnPointerExit(null);
    }


    IEnumerator HoverEffect(Vector3 targetPos , Vector3 targetScale)
    {
        if (isHoverEffect == false) yield break;

        float t = 0;
        for (int i = 0; i < 10; i++)
        {
            t += 0.1f;

            Card card = GetComponent<SlotUI>().ReadData<Card>();
            if (card != null)
            {
                card.transform.localPosition = Vector3.Lerp(card.transform.localPosition, targetPos, t);
                card.transform.localScale = Vector3.Lerp(card.transform.localScale, targetScale, t);
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
}

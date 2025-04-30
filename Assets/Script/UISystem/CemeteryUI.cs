using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Spine;

public class CemeteryUI : MonoBehaviour,IDropHandler
{
    [SerializeField] public Transform CemeteryPos;
    [SerializeField] List<Card> CemeteryCard;



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
        card.transform.position = CemeteryPos.position;
        card.transform.SetParent(CemeteryPos);

        CemeteryCard.Add(card);
        card.gameObject.SetActive(true);
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] public Card card;
    [SerializeField] public bool isSlot = true; // false == 사용 불가
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSlot == false) { return; }

        if (collision.gameObject.GetComponent<Card>() == true)
        {
            card = collision.gameObject.GetComponent<Card>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (card != null && card.gameObject == collision.gameObject)
        {
            card = null;
            isSlot = true;
        }
    }

    private void Update()
    {
        if (card == null) { return; }
        if (card.isHold == false)
        {
            Insert(card);
            card.transform.position = this.transform.position;
        }
    }

    public void Insert(Card _card)
    {
        card = _card;
        isSlot = false;

    }
}

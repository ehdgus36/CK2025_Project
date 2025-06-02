using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardDropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Card dropCard = eventData.pointerDrag.gameObject.GetComponent<Card>();

        if (dropCard != null)
        {
            GameManager.instance.PlayerCardCastPlace.AddCard(dropCard);
        }
        Debug.Log("SetSlot");

        gameObject.SetActive(false);
    }
}

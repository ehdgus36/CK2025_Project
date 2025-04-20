using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffectUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    [Range(1,2)]float hoverScale = 1f;
    Vector3 StartScale;
    Vector3 StartPos;

    [SerializeField] CardDescription cardDescription;

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        StartScale = transform.localScale;
        StartPos = transform.position;
        transform.localScale = StartScale * hoverScale;

        transform.position += new Vector3(0f, 0f, -1f);

        Card card = GetComponent<SlotUI>().ReadData<Card>();
        cardDescription.gameObject.SetActive(true);
        cardDescription.transform.position = this.transform.position;
        cardDescription.UpdateDescription(card.name, card.Example, card.SubExample, card.Grade_Point);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = StartScale;
        transform.position = StartPos;
        cardDescription.gameObject.SetActive(false);
    }
}

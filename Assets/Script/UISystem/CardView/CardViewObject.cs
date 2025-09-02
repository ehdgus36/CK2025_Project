using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardViewObject : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    [SerializeField]Image outline;
    [SerializeField]Image cardImage;
    [HideInInspector]public PlayerCardView PlayerCardView;

    [HideInInspector] public bool isSelect = false;

   [SerializeField] CardData Data;
    public void UpdateCardViewObject(CardData CardData)
    {
        Data = CardData;
        string Path = "CardImage/" + CardData.Card_Im;
        cardImage.sprite = Resources.Load<Sprite>(Path);
        isSelect = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color color = outline.color;

        color.a = 1f;
        outline.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelect == true) return;

        Color color = outline.color;

        color.a = 0f;
        outline.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Color color = outline.color;

        color.a = 1f;
        outline.color = color;

        isSelect = true;

        PlayerCardView.SelectCardViewObject(Data, this);
    }
}

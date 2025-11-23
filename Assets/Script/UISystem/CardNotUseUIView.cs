using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardNotUseUIView : MonoBehaviour,IPointerEnterHandler,IPointerDownHandler
{
    [SerializeField] TextMeshProUGUI CardText;
    [SerializeField] TextMeshProUGUI SubText;

    public void OnPointerDown(PointerEventData eventData)
    {
        RuntimeManager.PlayOneShot("event:/UI/Card_Click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RuntimeManager.PlayOneShot("event:/UI/Card_Over");
    }

    //ManaSystem에서 받아오는 "CurrentMana/MaxMana 형태의 데이터를 받아와 UI표시"
    public void UpdateUI(int count)
    {
        CardText.text = count.ToString();
        if (SubText != null) SubText.text = CardText.text;
    }
}

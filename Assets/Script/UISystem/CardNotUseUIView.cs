using UnityEngine;
using TMPro;

public class CardNotUseUIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CardText;
    [SerializeField] TextMeshProUGUI SubText;

    //ManaSystem에서 받아오는 "CurrentMana/MaxMana 형태의 데이터를 받아와 UI표시"
    public void UpdateUI(int count)
    {
        CardText.text = count.ToString();
        if (SubText != null) SubText.text = CardText.text;
    }
}

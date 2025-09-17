using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CardUpGradeView : MonoBehaviour
{
    [SerializeField] Image CardImage;
    [SerializeField] Image BgImage;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI CurrentCardDesc;
    [SerializeField] TextMeshProUGUI UpGradeCardDesc;
    [SerializeField] Button UpGradeButton;
    


    public void UpdateUI(CardData cardData,CardData UpGradeCardData ,Action ButtonEvent)
    {
        
        CardImage.sprite = Resources.Load<Sprite>("CardImage/" + cardData.Card_Im);       
        Name.text = cardData.Card_Name_KR;

        StringBuilder stringBuilder = new StringBuilder(cardData.Card_ID);


        CurrentCardDesc.text = cardData.Card_Des;
        UpGradeCardDesc.text = cardData.Card_Des;

        UpGradeButton.onClick.RemoveAllListeners();
        UpGradeButton.onClick.AddListener(() => { ButtonEvent.Invoke(); });
    }

  


}

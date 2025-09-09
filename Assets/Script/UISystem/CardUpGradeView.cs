using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CardUpGradeView : MonoBehaviour
{
    [SerializeField]Image CardImage;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] Button UpGradeButton;
    CardData CardData;


    public void UpdateUI(string cardCode ,Action<CardData> ButtonEvent)
    {
        object Data;

        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(cardCode, out Data);

        CardData cardData = (CardData)Data;

        CardData = cardData;
        CardImage.sprite = Resources.Load<Sprite>("CardImage/" + cardData.Card_Im);       
        Name.text = cardData.Card_Name_KR;

        UpGradeButton.onClick.AddListener(() => { ButtonEvent.Invoke(cardData); });
    }

  


}

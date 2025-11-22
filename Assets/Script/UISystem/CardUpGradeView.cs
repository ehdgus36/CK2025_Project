using Google.GData.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CardUpGradeView : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image CardImage;


    [SerializeField] Image BgImage;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI CurrentCardDesc;
    [SerializeField] TextMeshProUGUI UpGradeCardDesc;
    [SerializeField] Button UpGradeButton;

    [SerializeField] GameObject NextButton;

    bool isSelect = false;


    string UpGradeName = "";
    string CurrentName = "";

    public void UpdateUI(CardData cardData, CardData UpGradeCardData, Action ButtonEvent)
    {

        CardImage.sprite = Resources.Load<Sprite>("CardImage/" + cardData.Card_Im);
        BgImage.sprite = CardImage.sprite;
        Name.text = cardData.Card_Name_KR;

        StringBuilder stringBuilder = new StringBuilder(cardData.Card_ID);
        stringBuilder[stringBuilder.Length - 1] = '2';

        CurrentCardDesc.text = cardData.Card_Des;


        UpGradeCardDesc.text = UpGradeCardData.Card_Des;


        UpGradeName = UpGradeCardData.Card_Name_KR;
        CurrentName = cardData.Card_Name_KR;

        UpGradeButton.onClick.RemoveAllListeners();
        UpGradeButton.onClick.AddListener(() => { ButtonEvent.Invoke();

            UpGradeCardDesc.text = UpGradeCardData.Card_Des;
            UpGradeCardDesc.gameObject.SetActive(true);
            NextButton?.SetActive(true);
            isSelect = true;

            UpGradeButton.image.sprite = UpGradeButton.spriteState.selectedSprite;
        });
    }





    public void OnPointerEnter(PointerEventData eventData)
    {
        Name.text = UpGradeName;
        CurrentCardDesc.gameObject.SetActive(false);

        UpGradeCardDesc.gameObject.SetActive(true);
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/UI/Upgrade/Upgrade_Up");
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelect == false)
        {
            Name.text = CurrentName;
            CurrentCardDesc.gameObject.SetActive(true);

            UpGradeCardDesc.gameObject.SetActive(false);
        }
    }
}

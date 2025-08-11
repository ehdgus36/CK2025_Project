using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerCardView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MainDesc;
    [SerializeField] TextMeshProUGUI SubDesc1;
    [SerializeField] TextMeshProUGUI SubDesc2;

    [SerializeField] Button ESCButton;

    [SerializeField] List<string> CardData;

    [SerializeField] Dack PlayerDack;
    [SerializeField] CemeteryUI PlayerCemetery;

    [SerializeField] CardViewObject[] cardViewObjects;

    [SerializeField] CardViewObject SelectObject;

    private void OnEnable()
    {
        for (int i = 0; i < cardViewObjects.Length; i++)
        {
            cardViewObjects[i].gameObject.SetActive(false);
        }

        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, out CardData);

        if (PlayerDack != null)
        {
            for (int i = 0; i < PlayerDack.GetDackDatas.Count; i++)
            {
                cardViewObjects[i].gameObject.SetActive(true);
                cardViewObjects[i].UpdateCardViewObject(PlayerDack.GetDackDatas[i].cardData);
            }
        }

        if (PlayerCemetery != null)
        {
            for (int i = 0; i < PlayerCemetery.GetCemeteryCards().Count; i++)
            {
                cardViewObjects[i].gameObject.SetActive(true);
                cardViewObjects[i].UpdateCardViewObject(PlayerCemetery.GetCemeteryCards()[i].cardData);
            }
        }
    }

    private void OnDisable()
    {
        MainDesc.text = "";
        SubDesc1.text = "";
        SubDesc2.text = "";
    }

    private void Start()
    {
        ESCButton.onClick.AddListener(() => { this.gameObject.SetActive(false); });

        for (int i = 0; i < cardViewObjects.Length; i++)
        {
            cardViewObjects[i].PlayerCardView = this; 
        }
    }



    public void SelectCardViewObject(CardData data, CardViewObject selectObj)
    {
        if (SelectObject != null)
        {
            SelectObject.isSelect = false;
            SelectObject.OnPointerExit(null);
        }


        MainDesc.text = data.Card_Des;
        SelectObject = selectObj;
    }
}

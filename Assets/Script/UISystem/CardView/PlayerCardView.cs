using Spine;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class PlayerCardView : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI CardName;
    [SerializeField] TextMeshProUGUI CardRank;
    [SerializeField] TextMeshProUGUI MainDesc;
    [SerializeField] TextMeshProUGUI SubDesc1;
    [SerializeField] TextMeshProUGUI SubDesc2;

    [SerializeField] Button ESCButton;

    //[SerializeField] List<string> CardData;

    [SerializeField] Dack PlayerDack;
    [SerializeField] CemeteryUI PlayerCemetery;

    [SerializeField] CardViewObject[] cardViewObjects;

    [SerializeField] CardViewObject SelectObject;

    [SerializeField] Image CardImage;

    [SerializeField] Button NextButton;
    [SerializeField] Button PreviousButton;

    [SerializeField] RectTransform DescLayout;

    int currentPage;
    int MaxPage;

    private void OnEnable()
    {
        NextButton.onClick.RemoveAllListeners();
        PreviousButton.onClick.RemoveAllListeners();

        NextButton.onClick.AddListener(NextPage);
        PreviousButton.onClick.AddListener(PreviousPage);

        //무조건 키면 1번 페이지
        PreviousButton.interactable = false;
        NextButton.interactable = true;

        

        //GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, out CardData);

        if (PlayerDack != null)
        {          
            currentPage = 1;
            MaxPage = (PlayerDack.GetDackDatas.Count + 12) / 12;
        }

        if (PlayerCemetery != null)
        {        
            currentPage = 1;
            MaxPage = (PlayerCemetery.GetCemeteryCards().Count + 12) / 12;
        }

        
        if(MaxPage == 1) NextButton.interactable = false;



        LoadPage();
    }

   

    private void OnDisable()
    {
        CardImage.color = new Color(0f, 0f, 0f, 0f);


        CardName.text = "";
        CardRank.text = "";

        MainDesc.text = "";
        SubDesc1.text = "";
        SubDesc2.text = "";

        SelectObject = null;
    }

    private void Start()
    {
        ESCButton.onClick.AddListener(() => { this.gameObject.SetActive(false); });

        for (int i = 0; i < cardViewObjects.Length; i++)
        {
            cardViewObjects[i].PlayerCardView = this; 
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) this.gameObject.SetActive(false);
    }


    public void SelectCardViewObject(CardData data, CardViewObject selectObj)
    {
        if (SelectObject != null)
        {
            SelectObject.isSelect = false;
            SelectObject.OnPointerExit(null);
        }

        CardImage.sprite = Resources.Load<Sprite>("CardImage/" + data.Card_Im);
        CardImage.color = Color.white;
        CardName.text = data.Card_Name_KR;

        switch (data.Card_Rank)
        {
            case 1:
                CardRank.text = "일반 등급";

                break;
            case 2:
                CardRank.text = "희귀 등급";
                break;
            case 3:
                CardRank.text = "전설 등급";
                break;
        }
        

        MainDesc.text = data.Card_Des;
        SubDesc1.text = data.Buff_Ex;
        SubDesc2.text = data.Buff_Ex2;

        if (data.Buff_Ex == "0")
            SubDesc1.text = "";


        if (data.Buff_Ex2 == "0")
            SubDesc2.text = "";
        
        SelectObject = selectObj;

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(DescLayout);
    }


    public void NextPage()
    {
        CardImage.color = new Color(0f, 0f, 0f, 0f);

        currentPage++;

        if (currentPage >= MaxPage) NextButton.interactable = false;

        PreviousButton.interactable = true;

        LoadPage();
    }

    public void PreviousPage()
    {


        CardImage.color = new Color(0f, 0f, 0f, 0f);
        currentPage--;
       
        if(currentPage == 1) PreviousButton.interactable = false;
        
        NextButton.interactable = true;

        LoadPage();
    }

    void LoadPage()
    {
        for (int i = 0; i < cardViewObjects.Length; i++)
        {
            cardViewObjects[i].gameObject.SetActive(false);
            cardViewObjects[i].isSelect = false;
            cardViewObjects[i].OnPointerExit(null);
        }


        // 12개씩 페이지를 로드하기 위해 검색할 인덱스 지정
        int startIndex = 12 * (currentPage - 1);
        int endIndex = 0;  

        if (PlayerDack != null)
        {
            endIndex = Mathf.Clamp(12 * currentPage, 1, PlayerDack.GetDackDatas.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                cardViewObjects[i].gameObject.SetActive(true);
                cardViewObjects[i].UpdateCardViewObject(PlayerDack.GetDackDatas[i].cardData);
            }
        }

        if (PlayerCemetery != null)
        {
            endIndex = Mathf.Clamp(12 * currentPage, 1, PlayerCemetery.GetCemeteryCards().Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                cardViewObjects[i].gameObject.SetActive(true);
                cardViewObjects[i].UpdateCardViewObject(PlayerCemetery.GetCemeteryCards()[i].cardData);
            }
        }

    }
}

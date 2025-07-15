using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopEvent : MonoBehaviour
{
    [SerializeField] List<ShopItemObj> _TapeList;
    [SerializeField] List<GameObject> TapeSelectList;

    [SerializeField] List<ShopItemObj> _PeakList;
    [SerializeField] List<GameObject> PeakSelectList;
    [SerializeField] SelectItemDescPopUP SelectDescPopUp;

    [SerializeField] TextMeshProUGUI CoinText;
    [SerializeField] GameObject NoGold;  // 골드가 없으면 나옴

    [SerializeField] Button BuyButton;

    public List<ShopItemObj> TapeList { get { return _TapeList; } }
    public List<ShopItemObj> PeakList { get { return _PeakList; } }
    // Update is called once per frame

    public string SelectItemID;

    int SelectTapeIndex = -1;
    int SelectPeakIndex = -1;

    private void Start()
    {
        for (int i = 0; i < TapeList.Count; i++)
        {
            TapeList[i].ShopEvent = this;
            TapeSelectList[i].GetComponent<SelectShopItemObj>().Initialize(TapeList[i].gameObject);

        }

        for (int i = 0; i < PeakList.Count; i++)
        {
            PeakList[i].ShopEvent = this;
            PeakSelectList[i].GetComponent<SelectShopItemObj>().Initialize(PeakList[i].gameObject);
        }
        SelectDescPopUp.gameObject.SetActive(false);
        int coin = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, out coin);

        CoinText.text = coin.ToString();

    }


    public void SelectTape(int index)
    {
        for (int i = 0; i < TapeSelectList.Count; i++)
        {
            TapeSelectList[i].gameObject.SetActive(false);
        }

        TapeSelectList[index].gameObject.SetActive(true);
        SelectTapeIndex = index;
        SelectDescPopUp.gameObject.SetActive(true);
        SelectDescPopUp.ViewPopUP(SelectItemID);

        BuyButton.interactable = true;
    }

    public void SelectPeak(int index)
    {
        for (int i = 0; i < PeakSelectList.Count; i++)
        {
            PeakSelectList[i].gameObject.SetActive(false);
        }

        PeakSelectList[index].gameObject.SetActive(true);
        SelectPeakIndex = index;
        SelectDescPopUp.gameObject.SetActive(true);
        SelectDescPopUp.ViewPopUP(SelectItemID);

        BuyButton.interactable = true;
    }

    public void SoldOutEvent(int index)
    {


    }

    public void ExitShop()
    {
        SceneManager.LoadScene("GameMap");
    }

    public void BuyEvent()
    {

        NoGold.SetActive(false);

        ItemData data;
        int coins = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, out coins);
        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(SelectItemID, out data);


        if (coins < data.Price) // 돈없으면 리턴
        {
            NoGold.SetActive(true);
            return;
        }

        if (SelectTapeIndex != -1)
        {
            //구매를 위한 데이터 가져오기
           
            
            List<string> playerItemData = new List<string>();

                    
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.ITME_DATA, out playerItemData);


           

            //실질적인 구매 실행
            playerItemData.Add(SelectItemID);
            coins -= data.Price;

            //UI 업데이트
            CoinText.text = coins.ToString();


            //정보 갱신
          
            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.ITME_DATA, playerItemData);




            TapeSelectList[SelectTapeIndex].SetActive(false);
            _TapeList[SelectTapeIndex].SoldOut();
        }


        if (SelectPeakIndex != -1)
        {
            List<string> playerDackDatas = new List<string>();
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, out playerDackDatas);

            if (playerDackDatas != null)
            {
                playerDackDatas.Add(SelectItemID);
                coins -= data.Price;
            }

            CoinText.text = coins.ToString();

            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, playerDackDatas);

           PeakSelectList[SelectPeakIndex].SetActive(false);
            _PeakList[SelectPeakIndex].SoldOut();
        }

        SelectTapeIndex = -1;
        SelectPeakIndex = -1;
        BuyButton.interactable = false;
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, coins);
    }
}

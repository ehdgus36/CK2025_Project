using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;
using Unity.Mathematics;
using Spine.Unity;

public class ShopEvent : MonoBehaviour
{
    [SerializeField] List<ShopItemObj> _TapeList;
    [SerializeField] List<GameObject> TapeSelectList;

    [SerializeField] List<ShopItemObj> _PeakList;
    [SerializeField] List<GameObject> PeakSelectList;
    [SerializeField] SelectItemDescPopUP SelectDescPopUp;

    
    [SerializeField] GameObject NoGold;  // 골드가 없으면 나옴

    [SerializeField] Button BuyButton;
    [SerializeField] Button ResetButton;

    [SerializeField] TextMeshProUGUI ResetPriceText;

    [SerializeField] ItemDataLoader ItemDataLoader;

    [SerializeField] int ResetCount = 1;
    int ResetPrice = 20;


    public ItemDataLoader GetItemDataLoader { get { return ItemDataLoader; } }

    public List<ShopItemObj> TapeList { get { return _TapeList; } }
    public List<ShopItemObj> PeakList { get { return _PeakList; } }

    [SerializeField] SkeletonGraphic UIAnime;
    // Update is called once per frame

    public string SelectItemID;

    int SelectTapeIndex = -1;
    int SelectPeakIndex = -1;

    private void Start()
    {
        if (ItemDataLoader == null) ItemDataLoader = GetComponent<ItemDataLoader>();

        ItemDataLoader.LoadData();

        UIAnime.AnimationState.SetAnimation(0, "idle", true);

        RuntimeManager.PlayOneShot("event:/UI/Store/Store_In");
        for (int i = 0; i < PeakList.Count; i++)
        {
            PeakList[i].ShopEvent = this;
            PeakList[i].ResetCard( i == 0 ? null : PeakList[i - 1]);
        }
        SelectDescPopUp.gameObject.SetActive(false);
       
        ResetButton.onClick.AddListener(ResetItem);
        ResetPriceText.text = (ResetPrice + ItemDataLoader.strapData.Reroll_Cost).ToString();

        _PeakList[0].OnPointerEnter(null);
        _PeakList[0].OnPointerDown(null);
    }

    private void ResetItem()
    {

        int useGold = 0;

        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, out useGold);

        if (useGold >= ResetPrice)
        {
            useGold -= ResetPrice;
            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, useGold);
        }
        else{ return; }

        ResetCount++;

        ResetPrice = 20 + ItemDataLoader.strapData.Reroll_Cost;//(int)(((((float)ResetCount + 10f) * ((float)ResetCount + 10f)) / ((10f + 10f) * (10f + 10f))) *150f);
       
        ResetPriceText.text = ResetPrice.ToString();

        SelectDescPopUp.gameObject.SetActive(false);
        for (int i = 0; i < PeakList.Count; i++)
        {
            PeakList[i].ResetCard(i == 0 ? null : PeakList[i - 1]);
        }

        SelectDescPopUp.gameObject.SetActive(false);
        for (int i = 0; i < PeakList.Count; i++)
        {
            PeakList[i].PositionReset();
        }
    }


    public void SelectTape(int index)
    {
        SelectPeakIndex = -1;
        SelectTapeIndex = -1;

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
        SelectPeakIndex = -1;
        SelectTapeIndex = -1;

        //for (int i = 0; i < PeakSelectList.Count; i++)
        //{
        //    PeakSelectList[i].gameObject.SetActive(false);
        //}

        //PeakSelectList[index].gameObject.SetActive(true);
        SelectPeakIndex = index;
        SelectDescPopUp.gameObject.SetActive(true);
        SelectDescPopUp.ViewPopUP(SelectItemID);

        for (int i = 0; i < PeakList.Count; i++)
        {
            PeakList[i].PositionReset();
        }

        BuyButton.interactable = true;
    }

   

    public void ExitShop()
    {
        RuntimeManager.PlayOneShot("event:/UI/Store/Store_Out");
        SceneManager.LoadScene("GameMap");
    }

    public void BuyEvent()
    {
        RuntimeManager.PlayOneShot("event:/UI/Store/Buy_Card");
        NoGold.SetActive(false);

        ShopData data;
        int coins = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, out coins);
        GameDataSystem.StaticGameDataSchema.Shop_DATA_BASE.SearchData(SelectItemID, out data);


        int itemPrice = data.Price;
        if (ItemDataLoader.strapData.Shop_Sale > 0)
        {
            itemPrice = Mathf.RoundToInt(((float)data.Price * ((100f - (float)ItemDataLoader.strapData.Shop_Sale) / 100f)));
        }


        if (coins < itemPrice) // 돈없으면 리턴
        {
            StartCoroutine(NoGoldEvent());
            UIAnime.AnimationState.SetAnimation(0, "no-sell", false).Complete += Clear => { UIAnime.AnimationState.SetAnimation(0, "idle", true); };
            return;
        }


        UIAnime.AnimationState.SetAnimation(0, "buy", false).Complete += Clear => { UIAnime.AnimationState.SetAnimation(0, "idle", true); };

        if (SelectTapeIndex != -1)
        {
            //구매를 위한 데이터 가져오기
           
            
            List<string> playerItemData = new List<string>();

                    
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.ITME_DATA, out playerItemData);

            //실질적인 구매 실행
            playerItemData.Add(SelectItemID);
            coins -= itemPrice;

          

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
                coins -= itemPrice;
            }


            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, playerDackDatas);

          // PeakSelectList[SelectPeakIndex].SetActive(false);
            _PeakList[SelectPeakIndex].SoldOut();
        }

        SelectTapeIndex = -1;
        SelectPeakIndex = -1;
        BuyButton.interactable = false;
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, coins);
    }

    IEnumerator NoGoldEvent()
    {
        NoGold.SetActive(true);
        yield return new WaitForSeconds(.5f);
        NoGold.SetActive(false);
    }
}

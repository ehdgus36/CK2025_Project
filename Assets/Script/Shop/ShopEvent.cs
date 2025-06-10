using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ShopEvent : MonoBehaviour
{
    [SerializeField] List<ShopItemObj> _TapeList;
    [SerializeField] List<GameObject> TapeSelectList;

    [SerializeField] List<ShopItemObj> _PeakList;
    [SerializeField] List<GameObject> PeakSelectList;
    [SerializeField] SelectItemDescPopUP SelectDescPopUp;

    [SerializeField] TextMeshProUGUI CoinText;

    public List<ShopItemObj> TapeList { get { return _TapeList; } }
    public List<ShopItemObj> PeakList { get { return _PeakList; } }
    // Update is called once per frame

    public string SelectItemID;

    int SelectTapeIndex = -1;
    int SelectPeakIndex = -1;

    private void Start()
    {
        for (int i = 0; i < TapeSelectList.Count; i++)
        {
            TapeList[i].ShopEvent=this;
            

        }

        for (int i = 0; i < PeakSelectList.Count; i++)
        {
            PeakList[i].ShopEvent = this;
            
        }
        SelectDescPopUp.gameObject.SetActive(false);
        int coin = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA,out coin);

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
       // SelectDescPopUp.ViewPopUP(SelectItemID);
    }

    public void SoldOutEvent(int index)
    {
        

    }

    public void BuyEvent()
    {

        ItemData data;
        int coins = 0;
        List<string> playerItemData = new List<string>();

        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(SelectItemID, out data);
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, out coins);
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.ITME_DATA, out playerItemData);

        playerItemData.Add(SelectItemID);
        coins -= data.Price;

        CoinText.text = coins.ToString();
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, coins);
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.ITME_DATA, playerItemData);

       
    }
}

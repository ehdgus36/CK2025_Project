using FMODUnity;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;
using TMPro;

public class CardSellSystem : MonoBehaviour
{
    [SerializeField] PlayerCardView cardView;
    [SerializeField] int SellPrice = 30;
    [SerializeField] TextMeshProUGUI CardLossView;
    

    private void Start()
    {
        
    }

    public void SellEvent()
    {



      
        //돈가져오기
        int coin = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, out coin);





        //카드 리스트 가져오기
        List<string> DackData = new List<string>();
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, out DackData);



        //돈 검사
        if (coin < SellPrice)
        {
            CardLossView.text = "돈이 부족합니다.";
            StartCoroutine(DisPlayCardLoss());
            return;
        } 

        //카드 수량 검사
        if (DackData.Count <= 12) 
        {
            CardLossView.text = "카드가 12장 이하입니다";
            StartCoroutine(DisPlayCardLoss());
            return;
        }



        RuntimeManager.PlayOneShot("event:/UI/Store/Buy_Card");

        coin -= SellPrice;

        //버리기
        DackData.Remove(cardView.SelectCardCode);
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, coin);
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, DackData);



        cardView.LoadPage();
    }

    IEnumerator DisPlayCardLoss()
    {
        CardLossView.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(.6f);
        CardLossView.transform.parent.gameObject.SetActive(false);
    }
}

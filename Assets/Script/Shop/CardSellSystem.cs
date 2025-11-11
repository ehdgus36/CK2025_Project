using FMODUnity;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class CardSellSystem : MonoBehaviour
{
    [SerializeField] PlayerCardView cardView;
    [SerializeField] int SellPrice = 30;
    [SerializeField] GameObject CardLossView;
    

    private void Start()
    {
        
    }

    public void SellEvent()
    {



        RuntimeManager.PlayOneShot("event:/UI/Store/Buy_Card");
        //돈가져오기
        int coin = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, out coin);





        //카드 리스트 가져오기
        List<string> DackData = new List<string>();
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, out DackData);



        //돈 검사
        if (coin < SellPrice) return;

        //카드 수량 검사
        if (DackData.Count <= 12) 
        {
            StartCoroutine(DisPlayCardLoss());
            return;
        }
        


        coin -= SellPrice;

        //버리기
        DackData.Remove(cardView.SelectCardCode);
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, DackData);



        cardView.LoadPage();
    }

    IEnumerator DisPlayCardLoss()
    {
        CardLossView.gameObject.SetActive(true);
        yield return new WaitForSeconds(.3f);
        CardLossView.gameObject.SetActive(true);
    }
}

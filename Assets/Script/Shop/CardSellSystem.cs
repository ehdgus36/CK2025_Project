using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class CardSellSystem : MonoBehaviour
{
    [SerializeField] PlayerCardView cardView;
    [SerializeField] int SellPrice;


    public void SellEvent()
    {
        RuntimeManager.PlayOneShot("event:/UI/Store/Buy_Card");
        //돈나가는거
        int coin = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, out coin);

        //돈 검사하는거



        //버리는 부분
        List<string> DackData = new List<string>();
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, out DackData);

        DackData.Remove(cardView.SelectCardCode);
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, DackData);


        cardView.LoadPage();
    }
}

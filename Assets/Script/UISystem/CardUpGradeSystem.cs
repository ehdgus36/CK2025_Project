using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(CardUpGradeView))]
public class CardUpGradeSystem : MonoBehaviour
{
    CardUpGradeView CardUpGradeView;

    CardData cardData;
    CardData UpGradcardData;

    bool selectButton = false;
    public void SetUp(List<string> DackData)
    {
        CardUpGradeView = GetComponent<CardUpGradeView>();

       

        string randCode = "";

        while (true)
        {
            randCode = DackData[Random.Range(0, DackData.Count)];
            if (randCode[randCode.Length-1] == '1')
            {
                break;
            }
        }


        StringBuilder UPrandCode = new StringBuilder(randCode);
        UPrandCode[UPrandCode.Length - 1] = '2';

        object Data, UPData;

        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(randCode, out Data);


        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(UPrandCode.ToString(), out UPData);

        cardData = (CardData)Data;
        UpGradcardData = (CardData)UPData;

        CardUpGradeView.UpdateUI(cardData, UpGradcardData, UpGradeEvent);

        DackData.Remove(randCode);

    }

    void UpGradeEvent()
    {
        if (selectButton == true) return;

        List<string> DackData = new List<string>();

        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, out DackData);

        DackData.Remove(cardData.Card_ID);
       
        DackData.Add(UpGradcardData.Card_ID);


        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, DackData);

        Debug.Log("UpGrade¿‘¥œ¥Ÿ");

        selectButton = true;
    }

}

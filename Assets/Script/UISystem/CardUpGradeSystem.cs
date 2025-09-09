using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(CardUpGradeView))]
public class CardUpGradeSystem : MonoBehaviour
{
    CardUpGradeView CardUpGradeView;
    void Start()
    {
        CardUpGradeView = GetComponent<CardUpGradeView>();

        List<string> DackData = new List<string>();

        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, out DackData);

        string randCode = "";

        while (true)
        {
            randCode = DackData[Random.Range(0, DackData.Count)];
            if (randCode[randCode.Length-1] == '1')
            {
                break;
            }
        }

        CardUpGradeView.UpdateUI(randCode , UpGradeEvent);

    }

    void UpGradeEvent(CardData cardData)
    {
        List<string> DackData = new List<string>();

        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, out DackData);

        DackData.Remove(cardData.Card_ID);
        StringBuilder UpgradeCode = new StringBuilder(cardData.Card_ID);
        UpgradeCode[UpgradeCode.Length-1] = '2';
        DackData.Add(UpgradeCode.ToString());


        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, DackData);
    }

}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class Dack : MonoBehaviour
{
   
  

    [SerializeField] TextMeshProUGUI TextCardCount;
    [SerializeField] SlotGroup CardSlots;
    [SerializeField] CemeteryUI Cemetery;

    

    [SerializeField] List<Card> DackDatas = new List<Card>();
    [SerializeField] Transform CardPos;


    [SerializeField] List<Card> CardDatas;


    bool isOnce = false;


    //현재 덱에 있는 카드를 반환 덱에 카드가 없다면 묘지에서 카드를 가져온후 반환
    Card CardDrow()
    {
        ShuffleList<Card>(DackDatas);
        if (DackDatas.Count == 0)
        {
            DackDatas = Cemetery.GetCemeteryCards();
           
        }


        DackDatas[0].Initialized(CardSlots);
        Card result = DackDatas[0];
        DackDatas.Remove(DackDatas[0]);
      
        return result;
    }



    //외부에서 덱의 카드를 드로우 할때 호출
    public void DrawFromDeck()
    {
        if (isOnce == false)
        {
            List<string> DackData = new List<string>();
            if (GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, out DackData))
            {
                Debug.Log("DackData Count" + DackData.Count.ToString());
                for (int i = 0; i < DackData.Count; i++)
                {
                   
                    for (int j = 0; j < CardDatas.Count; j++)
                    {
                        
                        if (DackData[i] == CardDatas[j].CardID)
                        {
                            GameObject NewCard = Instantiate(CardDatas[j].gameObject);
                            NewCard.transform.SetParent(CardPos);
                            NewCard.transform.position = CardPos.position;
                            NewCard.transform.localScale = Vector3.one;

                            DackDatas.Add(NewCard.GetComponent<Card>());
                        }
                    }

                }
            }
            else
            {
                Debug.Log("카드 불가능?");
                //덱 못가져옴
            }




            isOnce = true;
        }


        for (int i = 0; i < CardSlots.Getsloat().Length ; i++)
        {
            Debug.Log(gameObject.name + "Drow");
            if (CardSlots.Getsloat()[i].ReadData<Card>() == null)
            {
                CardSlots.Getsloat()[i].InsertData(CardDrow().gameObject);
                DackDatas.Remove(CardDrow());

                Debug.Log(gameObject.name + "Drow");
            }
        }

        if(TextCardCount != null)
            TextCardCount.text = DackDatas.Count.ToString() + "/" + DackDatas.Count.ToString();


    }

    //카드 다시 넣기
    public void InsertCard(Card cardData)
    {

        //사용하고 남은 카드 넣을때 사용
        DackDatas.Insert(0,cardData);
        cardData.transform.position = CardPos.position;
        cardData.transform.SetParent(CardPos);
        //TextCardCount.text = DackDatas.Count.ToString() + "/" + DackDatas.Count.ToString();
    }
    private List<T> ShuffleList<T>(List<T> list)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }
}

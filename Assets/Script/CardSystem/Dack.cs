using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class Dack : MonoBehaviour
{
   
  

    [SerializeField] TextMeshProUGUI TextCardCount;
    [SerializeField] SlotGroup CardSlots;
    [SerializeField] CemeteryUI Cemetery;

    

    [SerializeReference] List<Card> DackDatas;
    [SerializeField] Transform CardPos;



    //현재 덱에 있는 카드를 반환 덱에 카드가 없다면 묘지에서 카드를 가져온후 반환
    Card CardDrow()
    {
        if (DackDatas.Count == 0)
        {
            DackDatas = Cemetery.GetCemeteryCards();
            ShuffleList<Card>(DackDatas);
        }

        Card result = DackDatas[0];
        return result;
    }

    public void DrawFromDeck()
    {
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
        DackDatas.Add(cardData);
        cardData.transform.position = CardPos.position;
        cardData.transform.SetParent(CardPos);
        TextCardCount.text = DackDatas.Count.ToString() + "/" + DackDatas.Count.ToString();
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

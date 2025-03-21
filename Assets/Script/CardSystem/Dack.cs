using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class Dack : MonoBehaviour
{
   
    [SerializeField] Card CardPrefab;
    [SerializeField] Card CardPrefab2;
    [SerializeField] Card CardPrefab3;

    [SerializeField] TextMeshProUGUI TextCardCount;

    [SerializeField] int DackCount = 30;

    [SerializeReference] List<Card> DackDatas;
    [SerializeField] Transform CardPos;

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            Card result = Instantiate(CardPrefab);
            result.gameObject.transform.SetParent(this.transform);
            result.gameObject.transform.position = CardPos.position;
            DackDatas.Add(result);
        }

        for (int i = 0; i < 10; i++)
        {
            Card result = Instantiate(CardPrefab2);
            result.gameObject.transform.SetParent(this.transform);
            result.gameObject.transform.position = CardPos.position;
            DackDatas.Add(result);
        }

        for (int i = 0; i < 10; i++)
        {
            Card result = Instantiate(CardPrefab3);
            result.gameObject.transform.SetParent(this.transform);
            result.gameObject.transform.position = CardPos.position;
            DackDatas.Add(result);
        }

        DackCount = DackDatas.Count;
        TextCardCount.text = DackDatas.Count.ToString() + "/" + DackCount.ToString();
        DackDatas = ShuffleList(DackDatas);
    }


    // Start is called before the first frame update
    public List<Card> CardDrow(int drowCount)
    {
        List<Card> drowCard = DackDatas.GetRange(0,drowCount );
        DackDatas.RemoveRange(0, drowCount );

        TextCardCount.text = DackDatas.Count.ToString() + "/" + DackCount.ToString();
        return drowCard;
    }

    //카드 다시 넣기
    public void InsertCard(Card cardData)
    {

        //사용하고 남은 카드 넣을때 사용
        DackDatas.Add(cardData);
        cardData.transform.position = CardPos.position;
        cardData.transform.SetParent(CardPos);
        TextCardCount.text = DackDatas.Count.ToString() + "/" + DackCount.ToString();
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

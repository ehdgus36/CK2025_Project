using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class Dack : MonoBehaviour
{
   
  

    [SerializeField] TextMeshProUGUI TextCardCount;
    [SerializeField] SlotGroup CardSlots;
    [SerializeField] CemeteryUI Cemetery;

    

    public List<Card> GetDackDatas { get { return DackDatas; } }
    [SerializeField] List<Card> DackDatas = new List<Card>();

    [SerializeField] Transform CardPos;


    [SerializeField] List<Card> CardDatas;

    [SerializeField] Card BaseCardPrefab;


    bool isOnce = false;


    //현재 덱에 있는 카드를 반환 덱에 카드가 없다면 묘지에서 카드를 가져온후 반환
    Card CardDrow()
    {
        ShuffleList<Card>(DackDatas);
        if (DackDatas.Count == 0)
        {
            for (int i = 0; i < Cemetery.GetCemeteryCards().Count; i++)
            {
                DackDatas.Add(Cemetery.GetCemeteryCards()[i]);
            }

            Cemetery.GetCemeteryCards().Clear();
            
        }


        DackDatas[0].Initialized(CardSlots);
        Card result = DackDatas[0];
       
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

                //신규 오토 생성
                for (int i = 0; i < DackData.Count; i++)
                {
                    GameObject NewCard = Instantiate(BaseCardPrefab.gameObject);
                    NewCard.transform.SetParent(CardPos);
                    NewCard.transform.position = CardPos.position;
                    NewCard.transform.localScale = Vector3.one;

                    NewCard.GetComponent<Card>().Initialized(DackData[i]);
                    DackDatas.Add(NewCard.GetComponent<Card>());
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
                Card drowCard = CardDrow();
                CardSlots.Getsloat()[i].InsertData(drowCard.gameObject);
                
                DackDatas.Remove(drowCard);

                Debug.Log(gameObject.name + "Drow");
            }
        }

        if(TextCardCount != null)
            TextCardCount.text = DackDatas.Count.ToString() + "/" + DackDatas.Count.ToString();

        GameManager.instance.UIManager.CardNotUseUI.UpdateUI(DackDatas.Count);
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

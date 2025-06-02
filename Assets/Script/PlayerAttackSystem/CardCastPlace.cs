using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CardCastPlace : MonoBehaviour
{
    [SerializeField] List<Card> cards;
    [SerializeField] PlayerCastCardStatus status;
    [SerializeField] int MaxCardCount;
    [SerializeField] int CurrentCount;


    public Enemy TargetEnemy 
    { 
        set
        {
            if (cards.Count != 0)
            {
                _TargetEnemy = value;
                Excute();
            }
            
        }
    }

    Enemy _TargetEnemy;
    Vector3 PlayerStartPos;

    public void Reset()
    {
        CurrentCount = MaxCardCount;
        cards.Clear();
        status.Reset();
        _TargetEnemy = null;

        GameManager.instance.UIManager.UseCardCountText.text = string.Format("{0}/{1}", CurrentCount, MaxCardCount);
        GameManager.instance.UIManager.Black.SetActive(false);
    }


    public void AddCard(Card addCard)
    {
        if (cards.Count == MaxCardCount) return;
        if (CurrentCount == 0) return;
         cards.Add(addCard);
        GameManager.instance.CardCemetery.Insert(addCard);
        status.UpdateUI(cards.Count);

        CurrentCount--;

        GameManager.instance.UIManager.UseCardCountText.text = string.Format("{0}/{1}", CurrentCount, MaxCardCount);
        GameManager.instance.UIManager.Black.SetActive(true);
    }

    public void Excute()
    {
        GameManager.instance.UIManager.Black.SetActive(false);
        PlayerStartPos = GameManager.instance.Player.transform.position;

        GameManager.instance.Player.transform.position = _TargetEnemy.transform.position - new Vector3(2, 0, 0);
        int count = cards.Count;
        StartCoroutine(TargetAttack(count));
    }

    IEnumerator TargetAttack(int count)
    {
        
        for (int i = 0; i < count; i++)
        {
            cards[0].TargetExcute(_TargetEnemy);
            yield return new WaitUntil(() => cards[0].IsCardEnd); // 카드효과가 마무리 할때까지 대기
            yield return new WaitForSeconds(.5f); // 너무 바로 시작하는 느낌들어서 딜레이 줌
            cards.RemoveAt(0);
        }

        yield return null;

        GameManager.instance.Player.transform.position = PlayerStartPos;
        status.Reset();
      
       
    }
}

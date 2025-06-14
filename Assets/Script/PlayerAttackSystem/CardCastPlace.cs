using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CardCastPlace : MonoBehaviour
{
    [SerializeField] List<Card> cards;
    [SerializeField] PlayerCastCardStatus status;
    [SerializeField] int MaxCardCount;
    [SerializeField] int CurrentCount;
    [SerializeField] Button TurnEnd;
    [SerializeField] GameObject BreakOutCut;

    [SerializeField] Animator ByeByeCut;


    public Enemy TargetEnemy
    {
        get { return _TargetEnemy; }
        set
        {
            if (cards.Count != 0)
            {
                _TargetEnemy = value;
                
                Excute();
            }
            
        }
    }

    [SerializeField]Enemy _TargetEnemy;
    Vector3 PlayerStartPos;
    bool isByeBye = false; // ByeBye는 한번 공격에 1회만 실행 true = 공격함, false = 안함
    bool _isByeByeEnd = false;

    List<Enemy> ByeByeEnemys = new List<Enemy>();

    public bool isByeByeEnd { get { return _isByeByeEnd; } }
    public void Reset()
    {
        PlayerStartPos = GameManager.instance.Player.transform.position;

        CurrentCount = MaxCardCount;
        cards.Clear();
        status.Reset();
        _TargetEnemy = null;
        isByeBye = false;

        GameManager.instance.UIManager.UseCardCountText.text = string.Format("{0}/{1}", CurrentCount, MaxCardCount);
        GameManager.instance.UIManager.Black.SetActive(false);
    }


    public void AddCard(Card addCard)
    {
        if (cards.Count == MaxCardCount) return;
        if (CurrentCount == 0) return;
         cards.Add(addCard);
        GameManager.instance.CardCemetery.Insert(addCard);
        status.UpdateUI(cards.Count, addCard.cardData.Card_Type);

        CurrentCount--;

        GameManager.instance.UIManager.UseCardCountText.text = string.Format("{0}/{1}", CurrentCount, MaxCardCount);
        GameManager.instance.UIManager.Black.SetActive(true);
       
        TurnEnd.interactable = false;
    }

    public void Excute()
    {
       
        GameManager.instance.UIManager.Black.SetActive(false);
        PlayerStartPos = GameManager.instance.Player.transform.position;

        int count = cards.Count;
        StartCoroutine(TargetAttack());
    }

    IEnumerator TargetAttack()
    {
        int count = cards.Count;
       
        for (int i = 0; i < count; i++)
        {
            if(cards[0].cardData.MoveType == "M" && isByeBye == false)
            {
                GameManager.instance.Player.transform.position = _TargetEnemy.transform.position - new Vector3(2, 0, 0);
            }

            yield return new WaitForSeconds(.1f); // 이동 후 딜레이

            if (cards.Count >= 2) cards[0].TargetExcute(_TargetEnemy, cards[1]);
            if (cards.Count == 1) cards[0].TargetExcute(_TargetEnemy);

            Debug.Log("aaaaa");
            yield return new WaitUntil(() => cards[0].IsCardEnd); // 카드효과가 마무리 할때까지 대기
            yield return new WaitForSeconds(.5f); // 너무 바로 시작하는 느낌들어서 딜레이 줌

            GameManager.instance.Player.transform.position = PlayerStartPos;

           
            cards.RemoveAt(0);
            yield return new WaitForSeconds(.2f);

           
        }

        yield return null;
   
        status.Reset();
        TurnEnd.interactable = true;
        _TargetEnemy = null;
    }

    public void AddByeByeSystem(Enemy target)
    {
        if (target.EnemyData.EnemyUnitData.CurrentHp <= 0)
        {
            _isByeByeEnd = false;
            ByeByeEnemys.Add(target);


            if (ByeByeEnemys.Count == 1)
            {
                StartCoroutine(ByeBye(target.gameObject));
            }
        }
    }


    IEnumerator ByeBye(GameObject Target)
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < ByeByeEnemys.Count; i++)
        {
            yield return new WaitForSeconds(.5f);

            GameManager.instance.Player.transform.position = ByeByeEnemys[i].gameObject.transform.position - new Vector3(2, 0, 0); // 앞으로 가기

            yield return new WaitForSeconds(.2f);

            GameManager.instance.Player.PlayerAnimator.PlayAnimation("gard1");
            yield return new WaitForSeconds(.15f); // 애니메이션 타이밍 나중에 이벤트로 처리


            BreakOutCut.SetActive(true);
            ByeByeEnemys[i].UnitAnimationSystem.PlayAnimation("hit");

            GameManager.instance.Player.PlayerEffectSystem.PlayEffect("ByeBye_Effect", GameManager.instance.Player.transform.position);
            StartCoroutine(ByeByeEnemyMove(ByeByeEnemys[i].gameObject));


            Time.timeScale = 1;
            GameManager.instance.ComboUpdate(Random.Range(30024, 32025));

            // 브레이크 아웃실행
            yield return new WaitForSeconds(.33f);

            yield return new WaitForSeconds(1f);
            GameManager.instance.Player.transform.position = PlayerStartPos;

            ByeByeCut.Play("ByeByeCutAnimeReturn");
           
            yield return new WaitForSeconds(1f);
            BreakOutCut.SetActive(false);

           
           
        }
    }


    IEnumerator ByeByeEnemyMove(GameObject target)
    {
        for (int j = 0; j < 9; j++)
        {
            target.transform.position += new Vector3(2f, 2f, 0);
            yield return new WaitForSeconds(.1f);
        }
    }
}


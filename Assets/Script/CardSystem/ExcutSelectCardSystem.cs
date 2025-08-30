using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public struct CardReservedData
{
    public Card card;
    public Enemy Target;

    public CardReservedData(Card c, Enemy T)
    {
        card = c;
        Target = T;
    }

}

public class ExcutSelectCardSystem : MonoBehaviour
{
    [SerializeField] GameObject ArrowUIObject;
    [SerializeField] int MaxExcutCardCount;
    [SerializeField] int CurrentExcutCardCount;
    [SerializeField] DimBackGroundObject DimObject;



    [SerializeField] List<dicobj> disobject = new List<dicobj>();
    Enemy _TargetEnemy;

    Card _PreviousCard;
    Card _SelectCard;

    bool isTargeting = false; // 몬스터 타겟팅이 가능한지 확인

    ManaSystem ManaSystem; // 마나 시스템

    Queue<CardReservedData> _CardQueue = new Queue<CardReservedData>();

    [SerializeField] List<Card> ThisTurnExcutCard = new List<Card>();
    Dictionary<string, bool> AbilityConditionData = new Dictionary<string, bool>();

    public int BuffDamage; // 임시 구조생각하기


    Coroutine ReservedCardCoroutine;
    public int UseManaCount { get { return ManaSystem.UseManaCount(); } }

    //1회성 어빌리티가 아닌 다회성 어빌리티 관리
    public void ExcutAbiltyCondition(string key)
    {
        if (AbilityConditionData.ContainsKey(key))
        {
            AbilityConditionData[key] = true;// 조건을 발동 상태로 만듬

            //이번턴에 사용한 카드 중에서 조건에 맞을때 실행할 어빌리티 
            for (int i = 0; i < ThisTurnExcutCard.Count; i++)
            {
                ThisTurnExcutCard[i].AbilieySystem();

                if (ThisTurnExcutCard[i].cardData.Ability_Type == "None" || ThisTurnExcutCard[i].cardData.Ability_Type == "Onec")
                {
                    ThisTurnExcutCard.Remove(ThisTurnExcutCard[i]);
                }
            }

            AbilityConditionData[key] = false;
        }
    }

    public bool GetAbiltyCondition(string key)
    {
        if (AbilityConditionData.ContainsKey(key))
        {
            return AbilityConditionData[key];
        }

        return false;
    }



    public void initialize() // 1회성으로 초기화 해야하는것
    {
        ManaSystem = new ManaSystem(MaxExcutCardCount);
        ManaSystem.Initialize();
        AbilityConditionData.TryAdd("0", true); // 항상 참인 조건
        AbilityConditionData.TryAdd("IsBarrierActive", false);
        AbilityConditionData.TryAdd("IsCardPlayed", false);
        AbilityConditionData.TryAdd("IsNotFullHP", false);
        AbilityConditionData.TryAdd("IsEnemyHit", false);
        AbilityConditionData.TryAdd("IsPlayerHit", false);

        List<string> keys = new List<string>(AbilityConditionData.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            dicobj obj = new dicobj();
            obj.key = keys[i];
            obj.value = AbilityConditionData[keys[i]];

            disobject.Add(obj);
        }

    }

    public void Reset()// 시스템 로직에서 특정타이밍 마다 초기화 해야하는것 플레이어 엔드턴일때 사용
    {
        CurrentExcutCardCount = 0;
        GameManager.instance.UIManager.UseCardCountText.text = string.Format("{0}/{1}", CurrentExcutCardCount, MaxExcutCardCount);// 리셋하면서 한번 UI 갱신
        _PreviousCard = null;
        _SelectCard = null;
        _TargetEnemy = null;
        BuffDamage = 0;

        StopCoroutine(ReservedCardCoroutine);
        ReservedCardCoroutine = null;
    }

    public void StartTurnRest()
    {
        ManaSystem.EndTurnReset();
        ReservedCardCoroutine = StartCoroutine(UseReservedCard()); // 예약시스템 실행
    }

    public void SetSelectCard(Card card) // 선택한 카드를 등록
    {
        //if (MaxExcutCardCount == CurrentExcutCardCount) return;
        _SelectCard = card;
        isTargeting = true;
        ArrowUIObject.SetActive(true);
        ArrowUIObject.transform.position = card.transform.position;

        //player에게 사용하는 카드
        if (card.cardData.Range_Type == 3)
        {
            DimObject.SetActiveDim("Enemy");//enemy를 어둡게
        }
        else
        {
            DimObject.SetActiveDim("Player");
        }
    }

    public void SetTargetEnemy(Enemy enemy) // 타겟팅한 몬스터 등록
    {
        //if (MaxExcutCardCount == CurrentExcutCardCount) return;
        if (_SelectCard == null || isTargeting == false) return;

        if (_SelectCard.cardData.Range_Type == 3) return;

        _TargetEnemy = enemy;



    }

    public void SetTargetPlayer(Player player) // 타겟팅한 몬스터 등록
    {
        if (player == null) _TargetEnemy = null;
        //if (MaxExcutCardCount == CurrentExcutCardCount) return;
        if (_SelectCard == null || isTargeting == false) return;


        if (_SelectCard.cardData.Range_Type != 3) return;
        _TargetEnemy = GameManager.instance.EnemysGroup.Enemys[0];



    }

    private void Update()
    {
        //배리어 있을때
        if (GameManager.instance.Player.PlayerUnitData.CurrentBarrier > 0)
        {
            AbilityConditionData["IsBarrierActive"] = true;
        }
        else
        {
            AbilityConditionData["IsBarrierActive"] = false;
        }

        if (GameManager.instance.Player.PlayerUnitData.CurrentHp < GameManager.instance.Player.PlayerUnitData.MaxHp)
        {
            AbilityConditionData["IsNotFullHP"] = true;
        }
        else
        {
            AbilityConditionData["IsNotFullHP"] = false;
        }

        if (disobject != null)
        {
            for (int i = 0; i < disobject.Count; i++)
            {
                var ddd = disobject[i];
                ddd.value = AbilityConditionData[disobject[i].key];

                disobject[i] = ddd;// AbilityConditionData[disobject[i].key];
            }
        }

        //if (isTargeting == false) return;

        if (Input.GetMouseButtonUp(0) == true)
        {
            if (_SelectCard != null)
            {
                if (_TargetEnemy != null)
                {
                    if (ManaSystem.UseMana(_SelectCard.cardData.Cost_Type))
                    {

                        ReservedCard(_SelectCard, _TargetEnemy);// 큐에 예약 데이터 넣기
                        ArrowUIObject.SetActive(false);
                        //CardExcutEvent();

                        
                    }
                }

                


            }
            _TargetEnemy = null;
            _SelectCard = null;
            ArrowUIObject.SetActive(false);
            DimObject.gameObject.SetActive(false);
        }



    }

    private void CardExcutEvent(Card selecCard, Enemy enemy)
    {
        if (selecCard.GetComponent<SkillCard>() == true)
        {

            //카드 사용
            selecCard.TargetExcute(enemy);

            isTargeting = false;


        }
        else
        {

            int combo = 0;
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, out combo);
            combo++;
            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, combo);

            // 사용한 카드 저장
            ThisTurnExcutCard.Add(selecCard);


            ExcutAbiltyCondition("IsCardPlayed");
            if (_PreviousCard != null)
            {
                selecCard.DamageBuff = _PreviousCard.cardData.Damage_Buff;

                if (_PreviousCard.GetType() == typeof(Drain_Card))
                {
                    selecCard.Buff_Recover_HP = selecCard.cardData.Damage;
                }
            }

            //카드 사용
            selecCard.TargetExcute(enemy);

            isTargeting = false;
            _PreviousCard = _SelectCard;


            CurrentExcutCardCount++;
            //GameManager.instance.UIManager.UseCardCountText.text = string.Format("{0}/{1}", CurrentExcutCardCount, MaxExcutCardCount);

        }

        
    }

    private void ReservedCard(Card card, Enemy enemy)
    {
        card.SetOutLineColor(Color.blue);
        _CardQueue.Enqueue(new CardReservedData(card, enemy));
    }

    private IEnumerator UseReservedCard()
    {
        while (true)
        {
            if (_CardQueue.Count != 0)
            {
                GameManager.instance.GetEndTurnButton.interactable = false;
                CardReservedData cardData = _CardQueue.Dequeue();

                if (cardData.Target.isDie != true) //타겟이 죽지 않았을 때만
                {
                    CardExcutEvent(cardData.card, cardData.Target);
                    yield return new WaitUntil(() => { return cardData.card.IsCardEnd == true; });
                }
                else // 타겟 죽으면 해당시점에 감소한 마나 회복
                {
                    ManaSystem.RecoveryMana();
                }

                cardData.card.SetOutLineColor(Color.white); // 예약이사용 되면 색원상 복구
            }

            GameManager.instance.GetEndTurnButton.interactable = true;
            yield return null;
        }
    }

    public void RecoveryMana()
    {
        ManaSystem.RecoveryMana();
    }
}

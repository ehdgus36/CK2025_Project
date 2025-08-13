using UnityEngine;
using System.Collections.Generic;

public class ExcutSelectCardSystem : MonoBehaviour
{
    [SerializeField] GameObject ArrowUIObject;
    [SerializeField] int MaxExcutCardCount;
    [SerializeField] int CurrentExcutCardCount;

    Enemy _TargetEnemy;

    Card _PreviousCard;
    Card _SelectCard;

    bool isTargeting = false; // 몬스터 타겟팅이 가능한지 확인

    ManaSystem ManaSystem; // 마나 시스템

    List<Card> ThisTurnExcutCard = new List<Card>();
    Dictionary<string, bool> AbilityConditionData = new Dictionary<string, bool>();

    public int UseManaCount{ get { return ManaSystem.UseManaCount(); } }
   
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
    }

    public void Reset()// 시스템 로직에서 특정타이밍 마다 초기화 해야하는것
    {
        CurrentExcutCardCount = 0;
        GameManager.instance.UIManager.UseCardCountText.text = string.Format("{0}/{1}", CurrentExcutCardCount, MaxExcutCardCount);// 리셋하면서 한번 UI 갱신
        _PreviousCard = null;
        _SelectCard = null;
        _TargetEnemy = null;
         ManaSystem.EndTurnReset();
    }

    public void SetSelectCard(Card card) // 선택한 카드를 등록
    {
        if (MaxExcutCardCount == CurrentExcutCardCount) return;
        _SelectCard = card;
        isTargeting = true;
        ArrowUIObject.SetActive(true);
        ArrowUIObject.transform.position = card.transform.position;
    }

    public void SetTargetEnemy(Enemy enemy) // 타겟팅한 몬스터 등록
    {
        if (MaxExcutCardCount == CurrentExcutCardCount) return;
        if (_SelectCard == null && isTargeting == false) return;

        _TargetEnemy = enemy;
        

        
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


        if (isTargeting == false) return;

        if (Input.GetMouseButtonUp(0) == true)
        {
            if (_SelectCard != null)
            {
                if (_TargetEnemy != null && ManaSystem.UseMana(_SelectCard.cardData.Cost_Type))
                {
                    
                     ThisTurnExcutCard.Add(_SelectCard);
                    

                    ExcutAbiltyCondition("IsCardPlayed");
                    if (_PreviousCard != null)
                    {
                        _SelectCard.DamageBuff = _PreviousCard.cardData.Damage_Buff;

                        if (_PreviousCard.GetType() == typeof(Drain_Card))
                        {
                            _SelectCard.Buff_Recover_HP = _SelectCard.cardData.Damage;
                        }
                    }

                    _SelectCard.TargetExcute(_TargetEnemy);

                    isTargeting = false;
                    // 사용한 카드 묘지로 보내는 기능
                    GameManager.instance.CardCemetery.Insert(_SelectCard);
                    // 사용한 카드 저장
                    


                    CurrentExcutCardCount++;
                    GameManager.instance.UIManager.UseCardCountText.text = string.Format("{0}/{1}", CurrentExcutCardCount, MaxExcutCardCount);

                }

                _PreviousCard = _SelectCard;

                _TargetEnemy = null;
                _SelectCard = null;
                ArrowUIObject.SetActive(false);
            }
        }



        

        
    }
}

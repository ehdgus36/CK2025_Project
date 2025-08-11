using UnityEngine;

public class ExcutSelectCardSystem : MonoBehaviour
{
    [SerializeField] GameObject ArrowUIObject;
    [SerializeField] int MaxExcutCardCount;
    [SerializeField] int CurrentExcutCardCount;

    Enemy _TargetEnemy;

    Card _PreviousCard;
    Card _SelectCard;

    bool isTargeting = false; // 몬스터 타겟팅이 가능한지 확인

    ManaSystem ManaSystem = new ManaSystem(); // 마나 시스템

    public void initialize() // 1회성으로 초기화 해야하는것
    {
        ManaSystem.Initialize();
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
        if (isTargeting == false) return;

        if (Input.GetMouseButtonUp(0) == true)
        {
            if (_TargetEnemy != null && ManaSystem.UseMana(_SelectCard.cardData.Cost_Type))
            {
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

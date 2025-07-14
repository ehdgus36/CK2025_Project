using UnityEngine;

public class ExcutSelectCardSystem : MonoBehaviour
{
    [SerializeField] GameObject ArrowUIObject;
    [SerializeField] int MaxExcutCardCount;
    [SerializeField] int CurrentExcutCardCount;

    Enemy _TargetEnemy;
    Card _SelectCard;

    bool isTargeting = false; // 몬스터 타겟팅이 가능한지 확인

    public void Reset()
    {
        CurrentExcutCardCount = 0;
        GameManager.instance.UIManager.UseCardCountText.text = string.Format("{0}/{1}", CurrentExcutCardCount, MaxExcutCardCount);
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
            if (_TargetEnemy != null)
            {
                _SelectCard.TargetExcute(_TargetEnemy);

                isTargeting = false;
                // 사용한 카드 묘지로 보내는 기능
                GameManager.instance.CardCemetery.Insert(_SelectCard);

              

                CurrentExcutCardCount++;
                GameManager.instance.UIManager.UseCardCountText.text = string.Format("{0}/{1}", CurrentExcutCardCount, MaxExcutCardCount);

            }
            _TargetEnemy = null;
            _SelectCard = null;
            ArrowUIObject.SetActive(false);
            
        }



        

        
    }
}

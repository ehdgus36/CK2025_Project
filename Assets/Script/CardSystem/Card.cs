
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using GameDataSystem;
using Spine;




public class Card : MonoBehaviour
{
    [SerializeField] protected string CardID;
    [SerializeField] public Sprite DescSprite;

    Buff CardBuff = null;
    public CardData cardData { get; private set; }

    [HideInInspector] public int DamageBuff = 0;
    [HideInInspector] public int Buff_Recover_HP = 0 ;

    bool isCardEnd = false;
    Enemy EnemyTarget;
    public bool IsCardEnd { get { return isCardEnd; } }
    public virtual void Initialized() 
    {
        isCardEnd = false;
        object data = null;
        if (StaticGameDataSchema.CARD_DATA_BASE.SearchData(CardID, out data))
        {
            cardData = (CardData)data;
            Debug.Log("카드 데이터 입력완료");
        }
        else
        {
           Debug.LogError("카드데이터를 불러오지못했습니다. CardID를 확인해주세요. 혹은 저장된 값이 없습니다");
        }

        Debug.Log("수치 :" + cardData.Damage);
    }


    public virtual void TargetExcute(Enemy Target , Card nextCard = null)
    {
        if (nextCard != null) nextCard.DamageBuff = cardData.Damage_Buff; // 조건문 만족시 버프 추가
        Debug.Log("이번 공격 애니메이션에서 Slash 이벤트 감지!"); // 대충 데미지 넣는거 구현   

        EnemyTarget = Target;
        GameManager.instance.Player.PlayerAnimator.PlayAnimation("attack",false ,AttackEvent , CompleteEvent); // 최종형 
    }


    //스파인에서 AttackEvent가 발생할 때 실행할거
    public virtual void AttackEvent(TrackEntry entry, Spine.Event e)
    {
         EnemyTarget.TakeDamage(cardData.Damage + DamageBuff,cardData.CardBuff);
         Debug.Log("이번 공격 애니메이션에서 Slash 이벤트 감지!"); // 대충 데미지 넣는거 구현   
         GameManager.instance.ComboUpdate(Random.Range(17010, 21204));

        GameManager.instance.Player.addHP(cardData.Recover_HP + Buff_Recover_HP);
    }

    // 애니메이션이 마무리 될때 할거
    public virtual void CompleteEvent(TrackEntry entry) 
    {
        isCardEnd=true;
        DamageBuff = 0; // 추가버프는 1회용이기 때문에 항상 초기화
        Buff_Recover_HP = 0;


    }
}

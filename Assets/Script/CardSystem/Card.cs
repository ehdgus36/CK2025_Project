using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using GameDataSystem;
using Spine;


public class Card : MonoBehaviour
{
    // [SerializeField] int Damage = 1;
    //public bool isHold = false;

    [SerializeField] String CardID;
    [SerializeField] public Sprite DescSprite;
    CommonCardData cardData;

    bool isCardEnd = false;
    Enemy EnemyTarget;
    public bool IsCardEnd { get { return isCardEnd; } }
    public virtual void Initialized() 
    {
        isCardEnd = false;
        object data;
        if (StaticGameDataSchema.CARD_DATA_BASE.SearchData(CardID, out data))
        {
            cardData = (CommonCardData)data;
        }
        else
        {
           // Debug.LogError("카드데이터를 불러오지못했습니다. CardID를 확인해주세요. 혹은 저장된 값이 없습니다");
        }
    }


    public virtual void TargetExcute(Enemy Target)
    {
        EnemyTarget = Target;
        GameManager.instance.Player.PlayerAnimator.PlayAnimation("attack",false ,AttackEvent , CompleteEvent); // 최종형 
    }


    //스파인에서 AttackEvent가 발생할 때 실행할거
    public virtual void AttackEvent(TrackEntry entry, Spine.Event e)
    {
         EnemyTarget.TakeDamage(5); // 고정 데미지
         Debug.Log("이번 공격 애니메이션에서 Slash 이벤트 감지!"); // 대충 데미지 넣는거 구현   
    }

    // 애니메이션이 마무리 될때 할거
    public virtual void CompleteEvent(TrackEntry entry) 
    {
       isCardEnd=true; 
    }
}

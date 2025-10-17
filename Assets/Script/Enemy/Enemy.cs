using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using GameDataSystem;
using UnityEngine.InputSystem;
using TMPro;
using static UnityEngine.EventSystems.EventTrigger;
using System;

[System.Flags]
public enum BuffLayer
{
    None = 0,                  
    Fire_1 = 1 << 0,            
    Eletric_1 = 1 << 1,           
    Captivate_1 = 1 << 2,      
    Curse_1 = 1 << 3,
    Everything = ~0            
}
[System.Serializable]
public class EnemyData
{
    [SerializeField] public UnitData EnemyUnitData;
    [SerializeField] public int MaxSkillPoint;

    public int CurrentSkillPoint;

  

    [SerializeField] public int MaxDamage;
    public int CurrentDamage;

    [SerializeField] public Sprite Enemy_Sprite;
}

public class Enemy : Unit, IPointerDownHandler ,IPointerUpHandler, IPointerEnterHandler , IPointerExitHandler
{
    //Unit정보 체력 공격력 등등
    [SerializeField] public EnemyData EnemyData;
    [SerializeField] UnitAnimationSystem EnemyAnimator;

    //애니메이션
    [SerializeField] EnemyStatus EnemyStatus;
   
  
    protected bool IsAttack;

    [SerializeField] BuffLayer buffLayer;
    [SerializeField] ImageFontSystem fontSystem;
    [SerializeField] EffectSystem EffectSystem;
  
   

    [SerializeField]public Vector3 AttackOffset = new Vector3(2, 0, 0);


    int startLayer = 0;

    Vector3 StargPos = Vector3.zero;

    EnemysGroup EnemyGroup;


    public UnitAIMachine AIMachine { get; private set; }

    public bool isDie { get; private set; }



    public bool isAttackEnd; // EnemyGrope에서 Enemy객체가 공격했는지를 판단




    public UnitAnimationSystem UnitAnimationSystem { get { return EnemyAnimator; } }

    public EffectSystem GetEffectSystem { get { return EffectSystem; } }
    public EnemyStatus GetEnemyStatus { get { return EnemyStatus; } }


    public bool isBarbedArmor = false;

    public virtual void Initialize(int index,  EnemysGroup group) 
    {
        if (group == null)
            throw new ArgumentNullException(nameof(group), "EnemyGroup은 null이 될 수 없습니다.");
        EnemyGroup = group;

        AIMachine = GetComponent<UnitAIMachine>();
        isDie = false;

        //EnemyUnitData 설정 
        UnitData = EnemyData.EnemyUnitData;
        
        EnemyData.CurrentDamage = EnemyData.MaxDamage;
        EnemyStatus?.Initialize(this); // UI 초기화
        EnemyStatus?.NextAttackUI.gameObject.SetActive(true);

        StartTurnEvent = () =>
        {
            isAttackEnd = false; //턴 시작시 공격가능하게 초기화
            EnemyStatus?.UpdateStatus(); //UI 갱신
            EnemyStatus?.NextAttackUI.gameObject.SetActive(false);// 다음 공격 표시끄기
            AIMachine.StartAI(this);
        };


        EndTurnEvent = () =>
        {
            //턴 종료시 버프로 감소된 변수 원상복구
            EnemyData.CurrentDamage = EnemyData.MaxDamage;

            //UI 갱신
            EnemyStatus?.UpdateStatus();

            // 다음 공격 표시
            EnemyStatus?.NextAttackUI.gameObject.SetActive(true);        
        };

        DieEvent += () =>
        {
             EnemyDieEvent();
        };
    }

    protected override void TakeDamageEvent(Unit formUnit, int damage, int resultDamage, Buff buff = null)
    {
        //애니메이션 재생
        EnemyAnimator.PlayAnimation("hit");
        //이팩트 , 사운드
        EffectSystem.PlayEffect("Hit_Effect", this.transform.position); // 자신에게
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Monster_Hurt");
        //UI 갱신
        EnemyStatus?.UpdateStatus();
        GameManager.instance.Shake.PlayShake();
        GameManager.instance.AbilitySystem.PlayeEvent(AbilitySystem.KEY_IS_ENEMY_HIT , this);

        if (isBarbedArmor == true)
        {
            if(formUnit.GetType() == typeof(Player))
                StartCoroutine(BarbedArmor(formUnit, damage));
        }

    }

    public override void AddBuff(Buff buff)
    {
        base.AddBuff(buff);
        EnemyStatus?.UpdateStatus();
    }

    IEnumerator BarbedArmor(Unit unit , int damage)
    {
        yield return new WaitForSeconds(.5f);

        unit.TakeDamage(this, ((damage + 1) / 2));
    }

    

    void EnemyDieEvent()
    {
        EffectSystem.PlayEffect("Monster_Die_Effect", this.transform.position);      
        GameManager.instance.PlayerCardCastPlace.AddByeByeSystem(this);
        this.transform.position = new Vector3(200, 200, 200);
        
        EnemyGroup.RemoveSelf(this); // EnemyGroup에서 자기자신 지우기
        isDie = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
       
    }
    public void RecoverHP(int _hp)
    {
        EnemyData.EnemyUnitData.CurrentHp += _hp;
        EffectSystem.PlayEffect("RecoverHP_Effect", transform.position);
        EnemyStatus.UpdateStatus();
    }
    public void CurrentDamageDown(int downDamage)
    {
        EnemyData.CurrentDamage -= downDamage;
        EnemyData.CurrentDamage = Mathf.Clamp(EnemyData.CurrentDamage, 0, 100);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        EnemyStatus.StatusPopUp.SetActive(true);
        GameManager.instance.ExcutSelectCardSystem.SetTargetEnemy(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        EnemyStatus.StatusPopUp.SetActive(false);
        GameManager.instance.ExcutSelectCardSystem.SetTargetEnemy(null);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
       
    }
}
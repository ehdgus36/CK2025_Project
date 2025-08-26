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
    [SerializeField] public int CurrentSkillPoint;

    [SerializeField] public string EnemyCode;
    [SerializeField] public string EnemyName;
    
    [SerializeField] public int MaxDamage;
    [SerializeField] public int CurrentDamage;


    [SerializeField] public float VulnerabilityPercent;


    [SerializeField] public int Barrier;

    [SerializeReference]public List<Buff> buffs;
}

public class Enemy : Unit, IPointerDownHandler ,IPointerUpHandler, IPointerEnterHandler , IPointerExitHandler
{
    //Unit정보 체력 공격력 등등
    [SerializeField] public EnemyData EnemyData;
    [SerializeField] UnitAnimationSystem EnemyAnimator;

    //애니메이션
    [SerializeField] EnemyStatus EnemyStatus;
  
    protected DieEnemy DieEvent;
    protected bool IsAttack;


    [SerializeField] BuffLayer buffLayer;
    [SerializeField] ImageFontSystem fontSystem;
    [SerializeField] EffectSystem EffectSystem;


    int EnemyIndex = 0;

    int startLayer = 0;
    bool isDescription = false;

    Vector3 StargPos = Vector3.zero;

    [SerializeField] Vector3 AttackOffset = new Vector3(2, 0, 0);
    public bool isDie { get; private set; }
    public bool isAttackEnd { get; private set; } // EnemyGrope에서 Enemy객체가 공격했는지를 판단

    public UnitAnimationSystem UnitAnimationSystem { get { return EnemyAnimator; } }

    public EffectSystem GetEffectSystem { get { return EffectSystem; } }
    public EnemyStatus GetEnemyStatus { get { return EnemyStatus; } }

    public virtual void Initialize(int index)
    {
        isDie = false;  
        CurrentBuff = new List<Buff>();

        // 받을수 있는 버프 제작
        if ((buffLayer & BuffLayer.Fire_1) != 0 ) CurrentBuff.Add(new FireBuff(BuffType.End, 0, 1 + GameManager.instance.ItemDataLoader.FireDm_UP)) ;
        if ((buffLayer & BuffLayer.Eletric_1) != 0) CurrentBuff.Add(new DefenseDebuff(BuffType.End, 0, 1));
        if ((buffLayer & BuffLayer.Captivate_1) != 0) CurrentBuff.Add(new CaptivBuff(BuffType.Start, 0, 1));
        if ((buffLayer & BuffLayer.Curse_1) != 0) CurrentBuff.Add(new AttackDamageDownBuff(BuffType.Start, 0, 2));

       
        //EnemyUnitData 설정 
        UnitData = EnemyData.EnemyUnitData;
        EnemyData.buffs = CurrentBuff;
        //EnemyData.MaxDamage -= GameManager.instance.ItemDataLoader.EnDm_Down;
        EnemyData.VulnerabilityPercent -= GameManager.instance.ItemDataLoader.EnDf_Down;

        EnemyData.CurrentDamage = EnemyData.MaxDamage;
       // EnemyData.CurrentDefense = EnemyData.MaxDefense;


        EnemyStatus?.Initialize(EnemyData); // UI 초기화

        EnemyStatus?.NextAttackUI.UpdateUI(EnemyData.CurrentDamage.ToString(), NextAttackUIView.AttackIconEnum.Attack);
        EnemyStatus?.NextAttackUI.gameObject.SetActive(true);
        StartTurnEvent = () =>
        {
            isAttackEnd = false; //턴 시작시 공격가능하게 초기화
            EnemyStatus?.UpdateStatus(EnemyData); //UI 갱신
            EnemyStatus?.NextAttackUI.gameObject.SetActive(false);// 다음 공격 표시끄기


            EnemyData.VulnerabilityPercent = 0;
            if (EnemyData.CurrentSkillPoint >= EnemyData.MaxSkillPoint)
            {
                EnemyData.CurrentSkillPoint = 0;
                

                // 스킬 실행
                EnemyData.EnemyUnitData.CurrentHp += 5;// 5회복
                isAttackEnd = true;

                EnemyStatus?.UpdateStatus(EnemyData);// ui갱신
            }
            else
            {
                EnemyData.CurrentSkillPoint++;
                StartCoroutine("EnemyAi"); //AI 실행
            }

           
           


        };


        EndTurnEvent = () =>
        {
            //턴 종료시 버프로 감소된 변수 원상복구
            EnemyData.CurrentDamage = EnemyData.MaxDamage;
            

            //UI 갱신
            EnemyStatus?.UpdateStatus(EnemyData);
          
            //AI 정지
            StopCoroutine("EnemyAi");

            // 다음 공격 표시
            EnemyStatus?.NextAttackUI.gameObject.SetActive(true);

            if (EnemyData.CurrentSkillPoint >= EnemyData.MaxSkillPoint)
            {
                // 다음 스킬 표시
                EnemyStatus?.NextAttackUI.UpdateUI("", NextAttackUIView.AttackIconEnum.RecverHP);
            }
            else
            {
                EnemyStatus?.NextAttackUI.UpdateUI(EnemyData.CurrentDamage.ToString(), NextAttackUIView.AttackIconEnum.Attack);
            }

        };
    }

    public void SetDieEvent(DieEnemy dieEvent)
    {
        DieEvent += dieEvent;
    }

    IEnumerator EnemyAi()
    {
        // 위치 이동
        StargPos = transform.position;
        transform.position = GameManager.instance.Player.transform.position + AttackOffset;
        yield return new WaitForSeconds(.5f);


        //애니메이션 재생및 공격
        EnemyAnimator.PlayAnimation("attack",false , (entry, e) => { GameManager.instance.Player.TakeDamage(EnemyData.CurrentDamage,this); } , null); 
        yield return new WaitForSeconds(1.0f);


        //완료 이벤트
        isAttackEnd = true; // 공격함
        transform.position = StargPos;
        yield return null;
    }

    public void TakeDamage(int damage, Buff buff = null)
    {
        if (buff != null )
        {
            if (CurrentBuff != null)
            {
                for (int i = 0; i < CurrentBuff.Count; i++)
                {
                    if (CurrentBuff[i].GetType() == buff.GetType())
                    {
                        CurrentBuff[i].AddBuffTurnCount(buff.GetBuffDurationTurn());

                        if (buff is FireBuff)
                        {
                            break;
                        }


                        CurrentBuff[i].StartBuff(this);
                        break;
                    }
                }
            }
            EnemyStatus?.UpdateStatus(EnemyData);
        }
        EnemyStatus?.UpdateStatus(EnemyData);

        if (damage <= 0)
        {
            return;
        }
        else
        {
            //취약 효과에 따른 데미지 구현/ 원래 데미지 + 취약효과 데미지(값이 0 이면 추가 데미지 0)
            int resultDamage = damage + (int)((float)damage * (EnemyData.VulnerabilityPercent/100f));

            if (EnemyData.Barrier > 0)
            {
                EnemyData.Barrier -= resultDamage;

                if (EnemyData.Barrier >=0)
                {
                    resultDamage = 0;
                }

                if (EnemyData.Barrier < 0)
                {
                    resultDamage = -EnemyData.Barrier;
                    EnemyData.Barrier = 0;
                }
            }


            base.TakeDamage(resultDamage);
            fontSystem.FontConvert(resultDamage.ToString());
        }



        //애니메이션 재생
        EnemyAnimator.PlayAnimation("hit");

        //이팩트 , 사운드
        EffectSystem.PlayEffect("Hit_Effect", this.transform.position); // 자신에게
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Monster_Hurt");

        //UI 갱신
        EnemyStatus?.UpdateStatus(EnemyData);
        
        
        GameManager.instance.Shake.PlayShake();

        GameManager.instance.ExcutSelectCardSystem.ExcutAbiltyCondition("IsEnemyHit");

    }

    protected override void Die()
    {
        
        DieEvent?.Invoke(this);
        isDie = true;

        GameManager.instance.PlayerCardCastPlace.AddByeByeSystem(this);

    }

    public void SlowMotionEffect(bool onoff)
    {
        if(onoff == false)
        {

            EnemyStatus?.OnPassiveDescription();
            startLayer = this.gameObject.layer;

            ChangeLayerRecursively(this.gameObject, 7);
            isDescription = true;
            return;
        }

        if (onoff == true)
        {
            EnemyStatus?.OffPassiveDescription();
            ChangeLayerRecursively(this.gameObject, startLayer);
            isDescription = false;
            return;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       // GameManager.instance.ExcutSelectCardSystem.SetTargetEnemy(this);
    }
    private void ChangeLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, layer);
        }
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
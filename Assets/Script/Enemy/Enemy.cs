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

    [SerializeField] public string EnemyCode;
    [SerializeField] public string EnemyName;
    
    [SerializeField] public int MaxDamage;
    [SerializeField] public int CurrentDamage;

    [SerializeField] public int MaxDefense;
    [HideInInspector] public int CurrentDefense;

    public List<Buff> buffs;
}

public class Enemy : Unit, IPointerDownHandler , IPointerEnterHandler , IPointerExitHandler
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


    public virtual void Initialize(int index)
    {
        isDie = false;  
        CurrentBuff = new List<Buff>();

        // 받을수 있는 버프 제작
        if ((buffLayer & BuffLayer.Fire_1) != 0 ) CurrentBuff.Add(new FireBuff(BuffType.End, 0, 1 + GameManager.instance.ItemDataLoader.FireDm_UP)) ;
        if ((buffLayer & BuffLayer.Eletric_1) != 0) CurrentBuff.Add(new ElecBuff(BuffType.End, 0, 1));
        if ((buffLayer & BuffLayer.Captivate_1) != 0) CurrentBuff.Add(new CaptivBuff(BuffType.Start, 0, 1));
        if ((buffLayer & BuffLayer.Curse_1) != 0) CurrentBuff.Add(new CurseBuff(BuffType.Start, 0, 2));

       
        //EnemyUnitData 설정 
        UnitData = EnemyData.EnemyUnitData;
        EnemyData.buffs = CurrentBuff;
        EnemyData.MaxDamage -= GameManager.instance.ItemDataLoader.EnDm_Down;
        EnemyData.MaxDefense -= GameManager.instance.ItemDataLoader.EnDf_Down;

        EnemyData.CurrentDamage = EnemyData.MaxDamage;
        EnemyData.CurrentDefense = EnemyData.MaxDefense;


        EnemyStatus?.Initialize(EnemyData); // UI 초기화

        StartTurnEvent = () =>
        {
            isAttackEnd = false; //턴 시작시 공격가능하게 초기화
            EnemyStatus?.UpdateStatus(EnemyData); //UI 갱신

            StartCoroutine("EnemyAi"); //AI 실행
        };


        EndTurnEvent = () =>
        {
            //턴 종료시 버프로 감소된 변수 원상복구
            EnemyData.CurrentDamage = EnemyData.MaxDamage;
            EnemyData.CurrentDefense = EnemyData.MaxDefense;

            //UI 갱신
            EnemyStatus?.UpdateStatus(EnemyData);
          
            //AI 정지
            StopCoroutine("EnemyAi");
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
        EnemyAnimator.PlayAnimation("attack",false , (entry, e) => { GameManager.instance.Player.TakeDamage(EnemyData.CurrentDamage); } , null); 
        yield return new WaitForSeconds(1.0f);


        //완료 이벤트
        isAttackEnd = true; // 공격함
        transform.position = StargPos;
        yield return null;
    }

    public void TakeDamage(int damage, Buff buff = null)
    {
        if (damage <= 0)
        {
            Debug.Log("TakeDamge함수에 0보다 작은 수치가 들어옴");
            return;
        }

        if (buff != null)
        {
            if (CurrentBuff != null)
            {
                for (int i = 0; i < CurrentBuff.Count; i++)
                {
                    if (CurrentBuff[i].GetType() == buff.GetType())
                    {
                        CurrentBuff[i].AddBuffTurnCount(buff.GetBuffDurationTurn());
                        break;
                    }
                }
            }
          
        }

       
       
        base.TakeDamage(damage - EnemyData.CurrentDefense);



        //애니메이션 재생
        EnemyAnimator.PlayAnimation("hit");

        //이팩트 , 사운드
        EffectSystem.PlayEffect("Hit_Effect", this.transform.position); // 자신에게
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Character/Monster/Monster_Hurt");

        //UI 갱신
        EnemyStatus?.UpdateStatus(EnemyData);
        
        
        GameManager.instance.Shake.PlayShake();


        fontSystem.FontConvert(damage.ToString());
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
        if (GameManager.instance.PlayerCardCastPlace.TargetEnemy == null)
        {
            GameManager.instance.PlayerCardCastPlace.TargetEnemy = this;
            Debug.Log("select Enemy");
        }
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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnemyStatus.StatusPopUp.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EnemyStatus.StatusPopUp.SetActive(false);
    }
}
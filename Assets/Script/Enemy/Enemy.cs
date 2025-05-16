using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using GameDataSystem;



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
    [HideInInspector] public int CurrentDamage;

    [SerializeField] public int MaxDefense;
    [HideInInspector] public int CurrentDefense;

    public List<Buff> buffs;
}

public class Enemy : Unit, IPointerDownHandler
{
    public EnemyData EnemyData;

    [SerializeField] Animator EnemyAnimator;
    [SerializeField] EnemyStatus EnemyStatus;

    [SerializeField] GameObject BG;

    protected DieEnemy DieEvent;
    protected bool IsAttack;

    [SerializeField] BuffLayer buffLayer;

    int EnemyIndex = 0;

    int startLayer = 0;
    bool isDescription = false;

    public bool isAttack { get; private set; } // EnemyGrope에서 Enemy객체가 공격했는지를 판단

    public void SetIsAttack(bool b)
    {
        IsAttack = b;
    }
  
    public virtual void Initialize(int index)
    {
        EnemyIndex = index;
      

        EnemyStatus.Initialize(EnemyData.EnemyUnitData.MaxHp, EnemyData.MaxDamage, EnemyIndex, EnemyData.EnemyName);

        UnitData.CurrentHp = UnitData.MaxHp;
        CurrentBuff = new List<Buff>();

        if ((buffLayer & BuffLayer.Fire_1) != 0 ) CurrentBuff.Add(new FireBuff(BuffType.End, 0, 1)) ;
        if ((buffLayer & BuffLayer.Eletric_1) != 0) CurrentBuff.Add(new ElecBuff(BuffType.End, 0, 1));
        if ((buffLayer & BuffLayer.Captivate_1) != 0) CurrentBuff.Add(new CaptivBuff(BuffType.End, 0, 1));
        if ((buffLayer & BuffLayer.Curse_1) != 0) CurrentBuff.Add(new CurseBuff(BuffType.End, 0, 1));

        EnemyData.EnemyUnitData = UnitData;
        EnemyData.buffs = CurrentBuff;
        EnemyData.CurrentDamage = EnemyData.MaxDamage;
        EnemyData.CurrentDefense = EnemyData.MaxDefense;
        StartTurnEvent = () =>
        {
            isAttack = false; //공격 안함
            EnemyStatus.UpdateBuffIcon(CurrentBuff);
           
            StartCoroutine("SampleAi");

        };


        EndTurnEvent = () =>
        {
            EnemyStatus.UpdateStatus(UnitData.CurrentHp, EnemyData.CurrentDamage, EnemyIndex);
            EnemyStatus.UpdateBuffIcon(CurrentBuff);
            //턴 종료시 버프로 감소된 변수 원상복구
            EnemyData.CurrentDamage = EnemyData.MaxDamage;
            EnemyData.CurrentDefense = EnemyData.MaxDefense;
            StopCoroutine("SampleAi");
        };

        DynamicGameDataSchema.AddDynamicDataBase(EnemyData.EnemyUnitData.DataKey, EnemyData);
    }

    public void SetDieEvent(DieEnemy dieEvent)
    {
        DieEvent += dieEvent;
    }

    IEnumerator SampleAi()
    {

        EnemyAnimator.Play("attack");
        yield return new WaitForSeconds(1.0f);
        GameManager.instance.Player.TakeDamage(EnemyData.CurrentDamage);

        yield return new WaitForSeconds(1.0f);

        isAttack = true; // 공격함
        yield return null;
    }




    public void TakeDamage(int damage, Buff buff)
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
        EnemyAnimator.Play("hit");
        EnemyStatus.UpdateStatus(EnemyData.EnemyUnitData.CurrentHp, EnemyData.CurrentDamage, EnemyIndex);
        EnemyStatus.UpdateBuffIcon(CurrentBuff);

        EnemyData.EnemyUnitData = UnitData;
        DynamicGameDataSchema.UpdateDynamicDataBase(EnemyData.EnemyUnitData.DataKey, EnemyData);
        

    }

    protected override void Die()
    {
        this.gameObject.SetActive(false);
        DieEvent?.Invoke(this);

    }

    public void SlowMotionEffect(bool onoff)
    {
        if(onoff == false)
        {

            EnemyStatus.OnPassiveDescription();
            startLayer = this.gameObject.layer;

            ChangeLayerRecursively(this.gameObject, 7);
            isDescription = true;
            return;
        }

        if (onoff == true)
        {
            EnemyStatus.OffPassiveDescription();
            ChangeLayerRecursively(this.gameObject, startLayer);
            isDescription = false;
            return;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDescription == false)
        {

            EnemyStatus.OnPassiveDescription();
            startLayer = this.gameObject.layer;

            ChangeLayerRecursively(this.gameObject, 7);
            isDescription = true;
            return;
        }

        if (isDescription == true)
        {
            EnemyStatus.OffPassiveDescription();
            ChangeLayerRecursively(this.gameObject, startLayer);
            isDescription = false;
            return;
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


    public void Setindex(int index)
    { 
        EnemyIndex = index;
        EnemyStatus.UpdateStatus(EnemyData.EnemyUnitData.CurrentHp, EnemyData.CurrentDamage, EnemyIndex);
    }
}
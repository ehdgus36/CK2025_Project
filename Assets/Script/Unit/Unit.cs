using GameDataSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable]
public class UnitData
{
    [SerializeField] public string DataKey;
    [SerializeField] public int MaxHp;

    [SerializeField]private int _CurrentHp;
    [HideInInspector] public int CurrentHp
    {
        get { return _CurrentHp; }
        set
        {
            _CurrentHp = Mathf.Clamp(value , 0 , MaxHp);  
        }
    }

    [SerializeField] private int _CurrentBarrier;
    [HideInInspector] public int CurrentBarrier
    {
        get { return _CurrentBarrier; }
        set
        {
            _CurrentBarrier = value; // 아마 1000까지는 올라가지 않을듯
        }
    }

  [SerializeReference] public List<Buff> buffs = new List<Buff>();



}

public class Unit : MonoBehaviour
{

    [SerializeField]
    protected UnitData UnitData;

    [HideInInspector]public bool IsTurn = false; //자신의 턴을 활성화 //일단 임시로 스턴효과 만들기위해 public

    protected int TurnCount = 0;

    [SerializeField] protected UnityAction StartTurnEvent;
    protected UnityAction EndTurnEvent;
    protected UnityAction DieEvent;
    // protected int UnitDamage =1;

    

    public void InitTurnCount() { TurnCount = 0; }

    protected virtual void TakeDamageEvent(Unit form, int damage, int resultDamage, Buff buff = null) { }
    public void TakeDamage(Unit form ,int damage, Buff buff = null)
    {
        
        if (damage <= 0) return;
        int resultDamage = damage;

        if (UnitData.CurrentBarrier > 0)
        {

           
            if (UnitData.CurrentBarrier - resultDamage >= 0) //베리어가 남거나 0이면
            {
                UnitData.CurrentBarrier -= resultDamage;
                resultDamage = 0;
            }

            if (UnitData.CurrentBarrier - resultDamage < 0) // 베리어가 0미만이면 데미지
            {
                resultDamage -= UnitData.CurrentBarrier;
                UnitData.CurrentBarrier = 0;
            }
        }


        UnitData.CurrentHp -= resultDamage;

        if (UnitData.CurrentHp <= 0)
        {
            UnitData.CurrentHp = 0;
           
            TakeDamageEvent(form, damage, resultDamage, buff);
            Die();
            return;
        }

        AddBuff(buff);
        TakeDamageEvent(form, damage, resultDamage, buff);
    }

    void Die() 
    {
        
        DieEvent?.Invoke();
    }

    public virtual void AddBuff(Buff buff)
    {
        if (buff == null) return;

        if (HellfireAction.isHellFire == true)
        {
            if (buff.GetType() == typeof(FireBuff))
            {

                Debug.Log("듀" );
                buff = new FireBuffBrunOut(BuffType.Start, buff.GetBuffDurationTurn(), 12); //번아웃
            }
        }


        if (buff != null)
        {
            bool IsBuffType = false;

            for (int i = 0; i < UnitData.buffs.Count; i++)
            {
                if (UnitData.buffs[i].GetType() == buff.GetType())
                {
                    UnitData.buffs[i].AddBuffTurnCount(buff.GetBuffDurationTurn() , this);

                    IsBuffType = true;
                }
            }

            if (IsBuffType == false)
            {
                UnitData.buffs.Add(buff.Clone());
            }
        }
    }

    //Unit의 턴이 시작했을 때 호출
    public void StartTurn() 
    {
        IsTurn = true;
        BuffExecution(BuffType.Start);
      
        if (IsTurn == false) return; 
        
        StartTurnEvent?.Invoke();
        
        TurnCount++;
    }

    //Unit의 턴이 끝났을 때 호출
    public void EndTurn()
    {
        IsTurn = false;
        BuffExecution(BuffType.End);
        EndTurnEvent?.Invoke();
    }

    public void RemoveBuff(Buff buff)
    { 
        UnitData.buffs.Remove(buff);
    }

    void BuffExecution(BuffType type)
    {
        if (UnitData.buffs == null) return;

       
        if (UnitData.buffs.Count != 0)
        {
            for (int i = 0; i < UnitData.buffs.Count; i++)
            {
                if (UnitData.buffs[i].GetBuffType() == type)
                {
                    UnitData.buffs[i].StartBuff(this);
                   
                }
            }
        }


        for (int i = 0; i < UnitData.buffs.Count; i++)
        {
            if (UnitData.buffs[i].GetBuffDurationTurn() < 0)
            {
                RemoveBuff(UnitData.buffs[i]);  
            }
        }


    }

    public virtual void LossHP(int HP)
    {
        UnitData.CurrentHp -= HP;

        if (UnitData.CurrentHp <= 0)
            TakeDamage(this, 1);

       
    }
}

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
            _CurrentBarrier = Mathf.Clamp(value, 0, 1000); // 아마 1000까지는 올라가지 않을듯
        }
    }

   public List<Buff> buffs = new List<Buff>();



}

public class Unit : MonoBehaviour
{

    [SerializeField]
    protected UnitData UnitData;
   
    protected List<Buff> CurrentBuff;



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
            UnitData.CurrentBarrier -= resultDamage;

            if (UnitData.CurrentBarrier >= 0) //베리어가 남거나 0이면
            {
                resultDamage = 0;
            }

            if (UnitData.CurrentBarrier < 0) // 베리어가 0미만이면 데미지
            {
                resultDamage = - UnitData.CurrentBarrier;
                UnitData.CurrentBarrier = 0;
            }
        }


        UnitData.CurrentHp -= resultDamage;

        if (UnitData.CurrentHp <= 0)
        {
            UnitData.CurrentHp = 0;
            Die();
            return;
        }


        if (buff != null)
        {
            bool IsBuffType = false;

            for (int i = 0; i < UnitData.buffs.Count; i++)
            {
                if (UnitData.buffs[i].GetType() == buff.GetType())
                {
                    UnitData.buffs[i].AddBuffTurnCount(buff.GetBuffDurationTurn());
                }
            }

            if (IsBuffType == false)
            {
                UnitData.buffs.Add(buff);
            }
        }
        TakeDamageEvent(form, damage, resultDamage, buff);


    }

    void Die() 
    {
        
        DieEvent?.Invoke();
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

    void BuffExecution(BuffType type)
    {
        if (CurrentBuff == null) return;

        Debug.Log("buffCount :"+ CurrentBuff.Count.ToString());
        if (CurrentBuff.Count != 0)
        {
            for (int i = 0; i < CurrentBuff.Count; i++)
            {
                if (CurrentBuff[i].GetBuffType() == type)
                {
                    CurrentBuff[i].StartBuff(this);
                }
            }
        }
    }
}

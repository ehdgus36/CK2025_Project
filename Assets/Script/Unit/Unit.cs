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
            _CurrentHp = value;
            //GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(DataKey, this);
        }
    }

    [SerializeField] private int _CurrentBarrier;
    [HideInInspector] public int CurrentBarrier
    {
        get { return _CurrentBarrier; }
        set
        {
            _CurrentBarrier = value;
            //GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(DataKey, this);
        }
    }





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
    //protected UnityAction DieEvent;
    // protected int UnitDamage =1;

    

    public void InitTurnCount() { TurnCount = 0; }

    public virtual void TakeDamage(int damage)
    {
        

        if (damage < 0)
        {
            Debug.Log("TakeDamge함수에 0보다 작은 수치가 들어옴");
            return;
        }

        UnitData.CurrentHp -= damage;

        if (UnitData.CurrentHp <= 0)
        {
            UnitData.CurrentHp = 0;
            Die();
        }

       
    }

    public virtual void TakeDamage(AttackData data)
    {
        if (UnitData.CurrentHp <= 0)
        {
            Die();
        }
        
      
    }

    protected virtual void Die() 
    {
        Debug.Log("Die() 활성화");
        //DieEvent?.Invoke();
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

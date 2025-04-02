using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] protected int UnitMaxHp = 10;
    [SerializeField] protected int UnitCurrentHp = 10;
    [SerializeField] protected bool IsTurn = false;
    [SerializeField] protected List<Buff> CurrentBuff;

    protected int TurnCount = 0;

   [SerializeField] protected UnityAction StartTurnEvent;
    protected UnityAction EndTurnEvent;
    //protected UnityAction DieEvent;
    // protected int UnitDamage =1;

    public int GetMaxHp() { return UnitMaxHp; }
    public int GetUnitCurrentHp() { return UnitCurrentHp; }

    public void InitTurnCount() { TurnCount = 0; }

    public virtual void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.Log("TakeDamge함수에 0보다 작은 수치가 들어옴");
            return;
        }

        UnitCurrentHp -= damage;

        if (UnitCurrentHp <= 0)
        {
            Die();
        }
    }

    public virtual void TakeDamage(AttackData data)
    {
        if (data.Damage < 0)
        {
            Debug.Log("TakeDamge함수에 0보다 작은 수치가 들어옴");
            return;
        }

        UnitCurrentHp -= data.Damage;

        if (data.Buff)
        {
            CurrentBuff.Add(data.Buff);
        }


        if (UnitCurrentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die() 
    {
        Debug.Log("Die() 활성화");
       // DieEvent?.Invoke();
    }

    //Unit의 턴이 시작했을 때 호출
    public void StartTurn() 
    {

        IsTurn = true;
        BuffExecution(BuffType.Start);

        Debug.Log(gameObject.name + "attack");
       
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

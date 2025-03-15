using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] protected int UnitMaxHp = 10;
    [SerializeField] protected int UnitCurrentHp = 10;
    [SerializeField] protected bool IsTurn = false;

    protected UnityAction StartTurnEvent;
    protected UnityAction EndTurnEvent;
    // protected int UnitDamage =1;

    public int GetMaxHp() { return UnitMaxHp; }
    public int GetUnitCurrentHp() { return UnitCurrentHp; }

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

    protected virtual void Die() 
    {
        Debug.Log("Die() 활성화");
    }

    public void StartTurn()
    {

        IsTurn = true;
        StartTurnEvent?.Invoke();
    }

    public void EndTurn()
    {
        IsTurn = false;
        EndTurnEvent?.Invoke();
    }
}

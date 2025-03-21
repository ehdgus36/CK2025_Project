using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
   // [SerializeField] int Damage = 1;
    public bool isHold = false;
    [SerializeField] public AttackData Data;
   
    [SerializeField] int ManaCost = 1;

   
    public int GetDamage() { return Data.Damage; }
    public int GetManCost() { return ManaCost; }

    public Buff GetBuff() { return Data.Buff; }

    public AttackData GetAttackData() { return Data; }
    public void SetAttackData(AttackData data) { Data = data; }

    void SetHold(bool hold) { isHold = hold; }
   
}

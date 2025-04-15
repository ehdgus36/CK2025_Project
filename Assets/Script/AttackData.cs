using UnityEngine;


[System.Serializable]
public enum AttackType
{ 
Single, All , None
}

public enum AttackOrderType
{ 
First , Last
}

[System.Serializable]
public struct AttackData
{

    [SerializeField] public AttackType Type;
    [SerializeField] public AttackOrderType Order;
    [SerializeField] public int Damage;
    [SerializeField] public Buff Buff;
    [SerializeField] public Unit FromUnit;
    [SerializeField] public Skill Skill;
   
}
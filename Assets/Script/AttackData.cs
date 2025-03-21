using UnityEngine;


[System.Serializable]
public enum AttackType
{ 
Single, All , None
}


[System.Serializable]
public struct AttackData
{

    [SerializeField] public AttackType Type;
    [SerializeField] public int Damage;
    [SerializeField] public Buff Buff;
    [SerializeField] public Unit FromUnit;
    [SerializeField] public Skill Skill;
    [SerializeField] public EffectSystem EffectSystem;
}
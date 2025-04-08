using UnityEngine;

public class NomalCard : Card
{
    [SerializeField] AttackType AttackType = AttackType.Single;
    [SerializeField] AttackOrderType AttackOrder = AttackOrderType.First;
    [SerializeField] int AttackDamage;
    [SerializeField] int ShildCount;




    public override int GetDamage() { return AttackDamage; }
    public AttackOrderType GetAttackOrder() { return AttackOrder; }
}

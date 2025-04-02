using UnityEngine;

public class NomalCard : Card
{
    [SerializeField] int AttackDamage;
    [SerializeField] int ShildCount;


    public override int GetDamage() { return AttackDamage; }
}

using UnityEngine;

public class FireBuff : Buff
{
    int Damage = 2;
  
    public FireBuff(BuffType type, int buffDurationTurn , int damage) : base(type, buffDurationTurn)
    {
        Damage = damage;
    }

    public override void StartBuff(Unit unit)
    {
        if (CurrentBuffTurn == BuffDurationTurn) return;

        unit.TakeDamage(Damage);
        CurrentBuffTurn++;
    }
}

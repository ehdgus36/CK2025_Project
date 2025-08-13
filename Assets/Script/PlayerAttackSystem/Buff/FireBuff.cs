using UnityEngine;

public class FireBuff : Buff
{
    int Damage = 2;
  
    public FireBuff(BuffType type, int buffDurationTurn , int damage) : base(type, buffDurationTurn)
    {
        Damage = damage;
    }

    public override void BuffEvent(Unit unit)
    {
        
        unit.GetComponent<Enemy>()?.TakeDamage(Damage,null);
        
    }
}

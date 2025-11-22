using UnityEngine;

public class PoisonBuff : Buff
{
    protected int Damage = 0;
  
    public PoisonBuff(BuffType type, int buffDurationTurn , int damage) : base(type, buffDurationTurn)
    {
        
    }

    public override void BuffEndEvent(Unit unit){ }

    public override void BuffEvent(Unit unit)
    {
        Damage = GetBuffDurationTurn();

        if (unit.GetComponent<Enemy>() == true)
        {           
            unit.GetComponent<Enemy>()?.TakeDamage(unit, Damage, null);
            unit.GetComponent<Enemy>()?.GetEffectSystem.PlayEffect("Fire_Effect", unit.transform.position);   
        }

        if (unit.GetComponent<Player>() == true)
        {
            unit.GetComponent<Player>()?.TakeDamage(unit, Damage);
            unit.GetComponent<Player>()?.PlayerEffectSystem.PlayEffect("Fire_Effect", unit.transform.position); 
        }
    }

    public override void PreviewBuffEffect<T>(T value, out T outobject)
    {
        outobject = value;
    }

    public override Buff Clone()
    {
        return new PoisonBuff(type, GetBuffDurationTurn(), Damage);
    }
}



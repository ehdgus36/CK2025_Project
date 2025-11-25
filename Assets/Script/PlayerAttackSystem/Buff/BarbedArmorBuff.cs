using UnityEngine;

public class BarbedArmorBuff : Buff
{
    Enemy enemy;

    public BarbedArmorBuff(BuffType type, int buffDurationTurn) : base(type, buffDurationTurn + 1)
    {
    
    }
    public override void BuffEndEvent(Unit unit) {}

    public override void BuffEvent(Unit unit)
    {
        if (BuffDurationTurn <= 1)
        {
            if (unit.GetType() == typeof(Enemy))
            {
                enemy = unit as Enemy;
                (unit as Enemy).isBarbedArmor = false;
            }
        }
        else
        {
            (unit as Enemy).isBarbedArmor = true;
        }
    }

    public override void ClearBuff()
    {
        base.ClearBuff();
        if (enemy != null) enemy.isBarbedArmor = false;

    }
    public override void PreviewBuffEffect<T>(T value, out T outobject)
    {
        outobject = value;
    }
}

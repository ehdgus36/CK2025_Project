using UnityEngine;

public class BarbedArmorBuff : Buff
{
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
                (unit as Enemy).isBarbedArmor = false;
            }
        }
        else
        {
            (unit as Enemy).isBarbedArmor = true;
        }
    }

    public override void PreviewBuffEffect<T>(T value, out T outobject)
    {
        outobject = value;
    }
}

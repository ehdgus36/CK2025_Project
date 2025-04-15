using UnityEngine;

public class StunBuff : Buff
{
    public override Buff DeepCopy()
    {
        return null;
    }

    public override void StartBuff(Unit unit)
    {
        if (CurrentBuffTurn == BuffDurationTurn) return;

        unit.IsTurn = false;

        CurrentBuffTurn++;
    }

    
}

using UnityEngine;

public class VolumeUPBuff : Buff
{
    public VolumeUPBuff(BuffType type, int buffDurationTurn) : base(type, buffDurationTurn)
    {
    }

    public override void BuffEndEvent(Unit unit)
    {
        
    }

    public override void BuffEvent(Unit unit)
    {
        BuffDurationTurn = 0;
    }

    public override void PreviewBuffEffect<T>(T value, out T outobject)
    {
        outobject = value;
    }

    
}

using System;
using UnityEngine;

public class VolumeUPBuff : Buff
{
    public VolumeUPBuff(BuffType type, int buffDurationTurn) : base(type, buffDurationTurn)
    {
        this.type = type;
       
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
        int damage = Convert.ToInt32(value);

       
        damage += BuffDurationTurn;

        value = (T)Convert.ChangeType(damage, typeof(T));

        outobject = value;
    }

    
}

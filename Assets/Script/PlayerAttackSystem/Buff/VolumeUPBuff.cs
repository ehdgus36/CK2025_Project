using UnityEngine;

public class VolumeUPBuff : Buff
{
    public VolumeUPBuff(BuffType type, int buffDurationTurn) : base(type, buffDurationTurn)
    {
        this.type = type;
        if (GameManager.instance.ItemDataLoader.stringData.Buff_Type == "Buff_Strength")
        {
            BuffDurationTurn = buffDurationTurn + GameManager.instance.ItemDataLoader.stringData.Buff_Value_Gain;
        }
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

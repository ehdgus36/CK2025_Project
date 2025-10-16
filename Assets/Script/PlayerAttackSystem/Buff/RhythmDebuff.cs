using UnityEngine;

public class RhythmDebuff : Buff
{
    public RhythmDebuff(BuffType type, int buffDurationTurn) : base(type, buffDurationTurn)
    {
    }

    public override void BuffEndEvent(Unit unit)
    {
        
    }

    public override void BuffEvent(Unit unit)
    {
        if (unit.GetComponent<Player>() == true)
        {
            //리듬게임 노트증가


        }
    }

    public override void PreviewBuffEffect<T>(T value, out T outobject)
    {
        outobject = value;
    }
}

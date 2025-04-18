using UnityEngine;

public class ElecBuff : Buff
{
    int Down = 1;

    public ElecBuff(BuffType type, int buffDurationTurn, int down) : base(type, buffDurationTurn)
    {
        Down = down;
    }


    public override void StartBuff(Unit unit)
    {


        if (CurrentBuffTurn == BuffDurationTurn) return;
        if (unit.GetComponent<Enemy>())
        {
            unit.GetComponent<Enemy>().CurrentDefense -= Down;
        }

        CurrentBuffTurn++;
    }
}

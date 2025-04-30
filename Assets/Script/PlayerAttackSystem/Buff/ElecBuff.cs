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


        if (BuffDurationTurn == 0) return;
        if (unit.GetComponent<Enemy>())
        {
            unit.GetComponent<Enemy>().EnemyData.CurrentDefense -= Down;
        }

        BuffDurationTurn--;
    }
}

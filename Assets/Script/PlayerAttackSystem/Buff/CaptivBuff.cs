using UnityEngine;

public class CaptivBuff : Buff
{

    //È®·ü 50;

    public CaptivBuff(BuffType type, int buffDurationTurn, int Percent) : base(type, buffDurationTurn)
    { 
    
    }

    public override void StartBuff(Unit unit)
    {

        if (BuffDurationTurn == 0) return;
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            unit.IsTurn = false;
        }

        BuffDurationTurn--;

    }
}

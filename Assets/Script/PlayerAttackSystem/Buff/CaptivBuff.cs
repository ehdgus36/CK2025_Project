using UnityEngine;

public class CaptivBuff : Buff
{

    //È®·ü 50;

    public CaptivBuff(BuffType type, int buffDurationTurn, int Percent) : base(type, buffDurationTurn)
    { 
    
    }

    public override void BuffEvent(Unit unit)
    {

        
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            unit.IsTurn = false;
        }

      

    }
}

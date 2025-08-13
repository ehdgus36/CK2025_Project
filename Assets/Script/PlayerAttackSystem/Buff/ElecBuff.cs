using UnityEngine;

public class DefenseDebuff : Buff
{
    int Down = 1;
    float DefenseDebuffPercent = 20f;
    public DefenseDebuff(BuffType type, int buffDurationTurn, int down) : base(type, buffDurationTurn)
    {
        Down = down;
    }


    public override void BuffEvent(Unit unit)
    {
        if (unit.GetComponent<Enemy>())
        {         
            unit.GetComponent<Enemy>().EnemyData.VulnerabilityPercent = DefenseDebuffPercent; //취약 퍼센트 연산
        }
    }
}

using System.Runtime.Serialization;
using UnityEngine;

public class CurseBuff : Buff
{
    int Down_Attack = 0;

    public CurseBuff(BuffType type, int buffDurationTurn, int down_attack) : base(type, buffDurationTurn)
    {
        Down_Attack = down_attack;
    }
    public override void StartBuff(Unit unit)
    {

        if (BuffDurationTurn == 0) return;
        if (unit.GetComponent<Enemy>())
        {
            unit.GetComponent<Enemy>().EnemyData.CurrentDamage -= Down_Attack;
        }

        BuffDurationTurn--;
    }
}

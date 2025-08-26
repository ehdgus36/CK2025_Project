using System.Runtime.Serialization;
using UnityEngine;

public class AttackDamageDownBuff : Buff
{
    int Down_Attack = 0;
    float DownPercent = 20f;
    public AttackDamageDownBuff(BuffType type, int buffDurationTurn, int down_attack) : base(type, buffDurationTurn)
    {
         
    }
    public override void BuffEvent(Unit unit)
    {
        DownPercent = 20f + GameManager.instance.ItemDataLoader.EnDf_Down;


        if (unit.GetComponent<Enemy>())
        {
            Down_Attack = (int)((float)unit.GetComponent<Enemy>().EnemyData.CurrentDamage * (DownPercent/100f));
            unit.GetComponent<Enemy>().EnemyData.CurrentDamage -= Down_Attack;
            Debug.Log("CuserBuffExcut :" + unit.GetComponent<Enemy>().EnemyData.CurrentDamage.ToString());
        }
    }
}

using System;
using System.Runtime.Serialization;
using UnityEngine;

public class AttackDamageDownBuff : Buff
{
    int Down_Attack = 0;
    float DownPercent = 20f;
    public AttackDamageDownBuff(BuffType type, int buffDurationTurn, int down_attack) : base(type, buffDurationTurn)
    {
         
    }

    public override void BuffEndEvent(Unit unit)
    {
        if (unit.GetComponent<Enemy>())
        {
           
        }

        if (unit.GetComponent<Player>())
        {
            //플레이어 카드 데이터 테이블 초기화 
        }


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

        if (unit.GetComponent<Player>())
        {
          //노트 처리하는 기능
        }
    }

    public override void PreviewBuffEffect<T>(T value, out T outobject)
    {
        int damage = Convert.ToInt32(value);

        DownPercent = 20f + GameManager.instance.ItemDataLoader.EnDf_Down;
        Down_Attack = (int)((float)damage * (DownPercent / 100f));

        damage -= Down_Attack;

        value = (T)Convert.ChangeType(damage, typeof(T));

        outobject = value;
    }
}

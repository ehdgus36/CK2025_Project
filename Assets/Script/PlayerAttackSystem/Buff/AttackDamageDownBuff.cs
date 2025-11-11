using System;
using System.Runtime.Serialization;
using UnityEngine;

public class AttackDamageDownBuff : Buff
{
    int Down_Attack = 0;
    float DownPercent = 0;
    public AttackDamageDownBuff(BuffType type, int buffDurationTurn, float down_attack) : base(type, buffDurationTurn)
    {
         DownPercent = down_attack;
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


        int ItemDownDamage = 0;
        if (GameManager.instance.ItemDataLoader.stringData.Buff_Type == "Buff_Weak")
        {
            ItemDownDamage = GameManager.instance.ItemDataLoader.stringData.Buff_Value_Gain;
        }

        float downPercent = DownPercent + (float)ItemDownDamage;

        if (unit.GetComponent<Enemy>())
        {
            Down_Attack = (int)((float)unit.GetComponent<Enemy>().EnemyData.MaxDamage * (downPercent / 100f));
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

        float downPercent = DownPercent + GameManager.instance.ItemDataLoader.EnDf_Down;

        Down_Attack = (int)((float)damage * (downPercent / 100f));

        damage -= Down_Attack;

        value = (T)Convert.ChangeType(damage, typeof(T));

        outobject = value;
    }
}


public class AttackDamageDownBuff_Mute : AttackDamageDownBuff
{
    public AttackDamageDownBuff_Mute(BuffType type, int buffDurationTurn, int down_attack) : base(type, buffDurationTurn, down_attack)
    {
    }
}
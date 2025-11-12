using GameDataSystem;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class AttackDamageDownBuff : Buff
{
    int Down_Attack = 0;
    float DownPercent = 0;


    static int getValue = 0;

    public static int GetBuffValue
    {
        get
        {
            int plusValue = 0;
            if (GameManager.instance.ItemDataLoader.stringData.Buff_Type == "Buff_Weak")
            {
                plusValue = GameManager.instance.ItemDataLoader.stringData.Buff_Value_Gain;
            }
            return getValue + plusValue;
        }
    }

    public AttackDamageDownBuff(BuffType type, int buffDurationTurn, float down_attack) : base(type, buffDurationTurn)
    {
         DownPercent = down_attack;
         getValue = (int)DownPercent;
    }

    public override void BuffEndEvent(Unit unit)
    {   
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
            StaticGameDataSchema.CARD_DATA_BASE.PlayerBuzz( DownPercent/100f, new List<Card>());
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


public class AttackDamageDownBuff_Mute : Buff
{
    int Down_Attack = 0;
    float DownPercent = 0;


    static int getValue = 0;

    public static int GetBuffValue
    {
        get
        {
            int plusValue = 0;
            if (GameManager.instance.ItemDataLoader.stringData.Buff_Type == "Buff_Weak")
            {
                plusValue = GameManager.instance.ItemDataLoader.stringData.Buff_Value_Gain;
            }
            return getValue + plusValue;
        }
    }

    public AttackDamageDownBuff_Mute(BuffType type, int buffDurationTurn, float down_attack) : base(type, buffDurationTurn)
    {
        DownPercent = down_attack;
        getValue = (int)DownPercent;
    }

    public override void BuffEndEvent(Unit unit)
    {
    }

    public override void BuffEvent(Unit unit)
    {


        int ItemDownDamage = 0;
       
        float downPercent = DownPercent + (float)ItemDownDamage;

        if (unit.GetComponent<Enemy>())
        {
            Down_Attack = (int)((float)unit.GetComponent<Enemy>().EnemyData.MaxDamage * (downPercent / 100f));
            unit.GetComponent<Enemy>().EnemyData.CurrentDamage -= Down_Attack;
            Debug.Log("CuserBuffExcut :" + unit.GetComponent<Enemy>().EnemyData.CurrentDamage.ToString());
        }

        if (unit.GetComponent<Player>())
        {
            StaticGameDataSchema.CARD_DATA_BASE.PlayerBuzz(1f, new List<Card>());
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
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FireBuff : Buff
{
   

    protected int Damage = 0;

    static int getValue = 0;

    float percent = .05f;

    public static int GetBuffValue
    {
        get
        {
            int plusValue = 0;

           

            if (GameManager.instance != null)
            {
                if (GameManager.instance.ItemDataLoader.stringData.Buff_Type == "Buff_Burn")
                {
                    plusValue = GameManager.instance.ItemDataLoader.stringData.Buff_Value_Gain;
                }
            }
            return getValue + plusValue;
        }
    }

    public FireBuff(BuffType type, int buffDurationTurn , int damage) : base(type, buffDurationTurn)
    {
        Damage = damage;
        getValue = Damage;
        getValue = (int)(percent * 10f);
    }

    public override void AddBuffTurnCount(int addCount, Unit buffuseUnit)
    {
        base.AddBuffTurnCount(addCount , buffuseUnit);

        if (GetBuffDurationTurn() >= 5)
        {
            this.BuffDurationTurn -= 5;
            buffuseUnit.AddBuff(new FireBuffBrunOut(BuffType.Start, 1, 12));
        }
    }

    public override void BuffEndEvent(Unit unit){ }

    public override void BuffEvent(Unit unit)
    {
       
        int ItemAddDamage = 0;
        if (GameManager.instance.ItemDataLoader.stringData.Buff_Type == "Buff_Burn")
        {
            ItemAddDamage = GameManager.instance.ItemDataLoader.stringData.Buff_Value_Gain;
        }

        if (unit.GetComponent<Enemy>() == true)
        {

            Damage = Mathf.FloorToInt((float)unit.GetComponent<Enemy>().EnemyData.EnemyUnitData.CurrentHp * percent);
            unit.GetComponent<Enemy>()?.TakeDamage(unit, Damage + ItemAddDamage, null);
            unit.GetComponent<Enemy>()?.GetEffectSystem.PlayEffect("Fire_Effect", unit.transform.position);
            
        }

        if (unit.GetComponent<Player>() == true)
        {
            Damage = Mathf.FloorToInt((float)unit.GetComponent<Player>().PlayerUnitData.CurrentHp * percent);
            unit.GetComponent<Player>()?.TakeDamage(unit, Damage);
            unit.GetComponent<Player>()?.PlayerEffectSystem.PlayEffect("Fire_Effect", unit.transform.position);
        }

      

    }

    public override void PreviewBuffEffect<T>(T value, out T outobject)
    {
        outobject = value;
    }

    public override Buff Clone()
    {
        return new FireBuff(type, GetBuffDurationTurn(), Damage);
    }
}


public class FireBuffBrunOut : Buff
{
    protected int Damage = 0;


    float percent = .3f;
    public FireBuffBrunOut(BuffType type, int buffDurationTurn, int damage) : base(type, buffDurationTurn)
    {
        Damage = damage;
    }
    public override void BuffEndEvent(Unit unit) { }

    public override void BuffEvent(Unit unit)
    {
        if (unit.GetComponent<Enemy>() == true)
        {

            Damage = Mathf.FloorToInt((float)unit.GetComponent<Enemy>().EnemyData.EnemyUnitData.CurrentHp * percent);
            unit.GetComponent<Enemy>()?.TakeDamage(unit, Damage , null);
            unit.GetComponent<Enemy>()?.GetEffectSystem.PlayEffect("Fire_Effect", unit.transform.position);

        }

        if (unit.GetComponent<Player>() == true)
        {
            Damage = Mathf.FloorToInt((float)unit.GetComponent<Player>().PlayerUnitData.CurrentHp * percent);
            unit.GetComponent<Player>()?.TakeDamage(unit, Damage);
            unit.GetComponent<Player>()?.PlayerEffectSystem.PlayEffect("Fire_Effect", unit.transform.position);
        }



    }

    public override void PreviewBuffEffect<T>(T value, out T outobject)
    {
        outobject = value;
    }

    public override Buff Clone()
    {
        return new FireBuffBrunOut(type, GetBuffDurationTurn(), Damage);
    }
}


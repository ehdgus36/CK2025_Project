using UnityEngine;

public class FireBuff : Buff
{
    protected int Damage = 0;
  
    public FireBuff(BuffType type, int buffDurationTurn , int damage) : base(type, buffDurationTurn)
    {
        Damage = damage;
    }

    public override void BuffEndEvent(Unit unit){ }

    public override void BuffEvent(Unit unit)
    {
        //float DmPercent = 2; //  (10f + (float)GameManager.instance.ItemDataLoader.FireDm_UP)/100f;

        if (unit.GetComponent<Enemy>() == true)
        {
            //Damage =  (int)((float)unit.GetComponent<Enemy>().EnemyData.EnemyUnitData.MaxHp * DmPercent);
            unit.GetComponent<Enemy>()?.TakeDamage(unit, Damage, null);
            unit.GetComponent<Enemy>()?.GetEffectSystem.PlayEffect("Fire_Effect", unit.transform.position);
            
        }

        if (unit.GetComponent<Player>() == true)
        {
            //Damage = (int)((float)unit.GetComponent<Player>().PlayerUnitData.MaxHp * DmPercent);
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

    public FireBuffBrunOut(BuffType type, int buffDurationTurn, int damage) : base(type, buffDurationTurn)
    {
        Damage = damage;
    }
    public override void BuffEndEvent(Unit unit) { }

    public override void BuffEvent(Unit unit)
    {
        //float DmPercent = 2; //  (10f + (float)GameManager.instance.ItemDataLoader.FireDm_UP)/100f;

        if (unit.GetComponent<Enemy>() == true)
        {
            //Damage =  (int)((float)unit.GetComponent<Enemy>().EnemyData.EnemyUnitData.MaxHp * DmPercent);
            unit.GetComponent<Enemy>()?.TakeDamage(unit, Damage, null);
            unit.GetComponent<Enemy>()?.GetEffectSystem.PlayEffect("Fire_Effect", unit.transform.position);

        }

        if (unit.GetComponent<Player>() == true)
        {
            //Damage = (int)((float)unit.GetComponent<Player>().PlayerUnitData.MaxHp * DmPercent);
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


using UnityEngine;

public class FireBuff : Buff
{
    int Damage = 2;
  
    public FireBuff(BuffType type, int buffDurationTurn , int damage) : base(type, buffDurationTurn)
    {
        Damage = damage;
    }

    public override void BuffEvent(Unit unit)
    {
        float DmPercent = (10f + (float)GameManager.instance.ItemDataLoader.FireDm_UP)/100f;

        if (unit.GetComponent<Enemy>() == true)
        {
            Damage = (int)((float)unit.GetComponent<Enemy>().EnemyData.EnemyUnitData.MaxHp * DmPercent);
            unit.GetComponent<Enemy>()?.TakeDamage(Damage, null);
            unit.GetComponent<Enemy>()?.GetEffectSystem.PlayEffect("Fire_Effect", unit.transform.position);
        }

        if (unit.GetComponent<Player>() == true)
        {
            Damage = (int)((float)unit.GetComponent<Player>().PlayerUnitData.MaxHp * DmPercent);
            unit.GetComponent<Player>()?.TakeDamage(Damage);
            unit.GetComponent<Player>()?.PlayerEffectSystem.PlayEffect("Fire_Effect", unit.transform.position);
        }

    }
}

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

        Damage =(int)((float) unit.GetComponent<Enemy>().EnemyData.EnemyUnitData.MaxHp * DmPercent);
        unit.GetComponent<Enemy>()?.TakeDamage(Damage,null);
        unit.GetComponent<Enemy>()?.GetEffectSystem.PlayEffect("Fire_Effect", unit.transform.position);
        
    }
}

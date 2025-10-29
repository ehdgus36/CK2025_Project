using UnityEngine;

public class BossStatus : EnemyStatus
{
    [SerializeField] HP_Bar BossHP_Bar;

    [SerializeField] Buff_Icon_UI BossBuff_UI;

    [SerializeField] EnemySkillPoint _BossEnemySkillPoint;


    public override void Initialize(Enemy enemy)
    {
        base.Initialize(enemy);

        BossHP_Bar.UpdateUI(enemyData.EnemyUnitData.MaxHp, enemyData.EnemyUnitData.CurrentHp);

        _BossEnemySkillPoint.UpdateUI(enemyData.CurrentSkillPoint, enemyData.MaxSkillPoint);

        BossBuff_UI.UpdateBuffIcon(enemyData.EnemyUnitData.buffs);
    }


    public override void UpdateStatus()
    {
        base.UpdateStatus();


        BossHP_Bar.UpdateUI(enemyData.EnemyUnitData.MaxHp, enemyData.EnemyUnitData.CurrentHp);

        _BossEnemySkillPoint.UpdateUI(enemyData.CurrentSkillPoint, enemyData.MaxSkillPoint);

        BossBuff_UI.UpdateBuffIcon(enemyData.EnemyUnitData.buffs);
    }
}

using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemyAttackState : BaseAIState
{
    Vector3 StargPos;
    [SerializeField] Vector3 AttackOffset;

    BaseAIState EnemySkill;

    public EnemyAttackState(BaseAIState ChangeSkillState) { EnemySkill = ChangeSkillState;}

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }
   
    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        Enemy enemy = (Enemy)unit;

        if (enemy.EnemyData.CurrentSkillPoint >= enemy.EnemyData.MaxSkillPoint)
        {
            aIBehavior.ChangeState(EnemySkill, unit, aIBehavior);
            yield break;
        }

        // 위치 이동
        StargPos = enemy.transform.position;
        enemy.transform.position = GameManager.instance.Player.transform.position + AttackOffset;
        yield return new WaitForSeconds(.0f);

        //애니메이션 재생및 공격
        enemy.UnitAnimationSystem.PlayAnimation("attack", false, (entry, e) => { GameManager.instance.Player.TakeDamage(enemy,enemy.EnemyData.CurrentDamage); }, null);

        yield return new WaitForSeconds(1.0f);

        //완료 이벤트
        enemy.isAttackEnd = true; // 공격함
        enemy.transform.position = StargPos;
        yield return null;

        yield break;

    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) { }
  

}

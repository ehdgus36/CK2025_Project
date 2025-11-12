using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

// enemy가 취할수 있는 액션을 정리한 클래스(예 / 플레이어까지 이동 , 공격 , 마무리)
public class EnemyStateAction
{
    Vector3 StartPos;
    public void MoveEnemy(GameObject moveObject, Vector3 formPos, Vector3 attackOffset)
    {
        StartPos = moveObject.transform.position;
        moveObject.transform.position = formPos + attackOffset;
    }

    public IEnumerator AttackEnemy(int damage, int attackCount, Enemy attackEnemy, Unit targetUnit , Buff buff = null)
    {
        for (int i = 0; i < attackCount; i++)
        {
            attackEnemy.UnitAnimationSystem.PlayAnimation("attack", false, (entry, e) => { GameManager.instance.Player.TakeDamage(attackEnemy, damage , buff); }, null);
            yield return new WaitForSeconds(.8f);
        }
    }
}

[System.Serializable]
public class EnemyAttackState : BaseAIState
{
    [SerializeReference] BaseAIState EnemySkill;
    [SerializeReference] BaseAIState EnemyDefultAttack;

    public EnemyAttackState(BaseAIState defultAttackState ,BaseAIState ChangeSkillState) { EnemyDefultAttack = defultAttackState; EnemySkill = ChangeSkillState;}
    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }
    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {

        Debug.Log("몬스터 AI 실행");


        Enemy enemy = (Enemy)unit;

        Debug.Log("화상 버프 존재" + enemy.EnemyData.EnemyUnitData.buffs.Exists(c => c is FireBuff));
        if (enemy.EnemyData.EnemyUnitData.buffs.Exists(c => c is FireBuff))
        {
            Debug.Log("화상 버프 존재");
            yield return new WaitForSeconds(1.0f);
        }
        if (enemy.EnemyData.EnemyUnitData.buffs.Exists(c => c is FireBuffBrunOut))
        {
            Debug.Log("화상 버프 존재");
            yield return new WaitForSeconds(1.0f);
        }
       


        if (enemy.EnemyData.CurrentSkillPoint >= enemy.EnemyData.MaxSkillPoint)
        {

            Debug.Log("몬스터 AI 스킬 실행");
            enemy.EnemyData.CurrentSkillPoint = 0;
            aIBehavior.ChangeState(EnemySkill, unit, aIBehavior);
            yield break;
        }
        else
        {
            enemy.EnemyData.CurrentSkillPoint++;
            Debug.Log("몬스터 평타");
            aIBehavior.ChangeState(EnemyDefultAttack, unit, aIBehavior);
           
            yield break;
        }
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) {}
  

}

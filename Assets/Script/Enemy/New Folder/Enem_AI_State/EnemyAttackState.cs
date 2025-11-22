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

    public IEnumerator AttackEnemy(int damage, int attackCount, Enemy attackEnemy, Unit targetUnit , Buff buff = null , string animeCode = "attack")
    {
        for (int i = 0; i < attackCount; i++)
        {
            attackEnemy.UnitAnimationSystem.PlayAnimation(animeCode, false, (entry, e) => { if (e.Data.Name == "heal") return; GameManager.instance.Player.TakeDamage(attackEnemy, damage , buff); }, null);
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

       

        Enemy enemy = (Enemy)unit;

      
        if (enemy.EnemyData.EnemyUnitData.buffs.Exists(c => c is FireBuff))
        {
         
            yield return new WaitForSeconds(1.0f);
        }
        if (enemy.EnemyData.EnemyUnitData.buffs.Exists(c => c is FireBuffBrunOut))
        {
          
            yield return new WaitForSeconds(1.0f);
        }
       


        if (enemy.EnemyData.CurrentSkillPoint >= enemy.EnemyData.MaxSkillPoint)
        {

            
            enemy.EnemyData.CurrentSkillPoint = 0;
            aIBehavior.ChangeState(EnemySkill, unit, aIBehavior);
            yield break;
        }
        else
        {
            enemy.EnemyData.CurrentSkillPoint++;
           
            aIBehavior.ChangeState(EnemyDefultAttack, unit, aIBehavior);
           
            yield break;
        }
    }

    public override void Exit(Unit unit, UnitAIBehavior aIBehavior) {}
  

}

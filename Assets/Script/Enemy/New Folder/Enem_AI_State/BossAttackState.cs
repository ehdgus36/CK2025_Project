using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

// enemy가 취할수 있는 액션을 정리한 클래스(예 / 플레이어까지 이동 , 공격 , 마무리)


[System.Serializable]
public class BossAttackState : BaseAIState
{  
    [SerializeReference] BaseAIState EnemySkill;
    [SerializeReference] BaseAIState EnemySkill2;
    [SerializeReference] BaseAIState EnemyDefultAttack;
    
    public BossAttackState(BaseAIState defultAttackState ,BaseAIState ChangeSkillState , BaseAIState ChangeSkill2State) 
    { EnemyDefultAttack = defultAttackState; EnemySkill = ChangeSkillState; EnemySkill2 = ChangeSkill2State; }

    public override void Enter(Unit unit, UnitAIBehavior aIBehavior) { }
   
    public override IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior)
    {
        
        Enemy enemy = (Enemy)unit;

        if (enemy.EnemyData.EnemyUnitData.buffs.Exists(c => c is FireBuff) ||
           enemy.EnemyData.EnemyUnitData.buffs.Exists(c => c is FireBuffBrunOut))

        {
            Debug.Log("화상 버프 존재");
            yield return new WaitForSeconds(1.0f);
        }

        if (enemy.EnemyData.CurrentSkillPoint >= enemy.EnemyData.MaxSkillPoint)
        {
            enemy.EnemyData.CurrentSkillPoint = 0;
            aIBehavior.ChangeState(EnemySkill, unit, aIBehavior);

            if (aIBehavior.GetType()== typeof( EnemyAI_HIPPOP_Behavior))
            {
                (aIBehavior as EnemyAI_HIPPOP_Behavior).SwapSkill();
            }
                    
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

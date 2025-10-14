using UnityEngine;
using System.Collections;



[System.Serializable]
public class EnemyAIBehavior : UnitAIBehavior 
{

    [SerializeReference] protected BaseAIState EnemyStartState;
    [SerializeReference] protected BaseAIState EnemySkillState;

    protected override BaseAIState StartState => EnemyStartState;

    protected EnemyAIBehavior()
    {
        EnemySkillState = new EnemySkill_MultiAttack_State();
        EnemyStartState = new EnemyAttackState(EnemySkillState);
    }
}


public class EnemyAI_HipRat_Behavior : EnemyAIBehavior
{

    protected override BaseAIState StartState => EnemyStartState;

    EnemyAI_HipRat_Behavior()
    {
        EnemySkillState = new EnemySkill_MultiAttack_State();
        EnemyStartState = new EnemyAttackState(EnemySkillState);
    }
}

public class EnemyAI_Spray_Behavior : EnemyAIBehavior
{

    protected override BaseAIState StartState => EnemyStartState;

    EnemyAI_Spray_Behavior()
    {
        EnemySkillState = new EnemySkill_MultiAttack_State();
        EnemyStartState = new EnemyAttackState(EnemySkillState);
    }
}

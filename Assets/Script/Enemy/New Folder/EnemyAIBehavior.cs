using UnityEngine;
using System.Collections;



[System.Serializable]
public abstract class EnemyAIBehavior : UnitAIBehavior 
{

    protected BaseAIState EnemyStartState;
    [SerializeReference] protected BaseAIState EnemyDefaultAttackState;
    [SerializeReference] protected BaseAIState EnemySkillState;

    public BaseAIState GetEnemyDefaultAttackState { get { return EnemyDefaultAttackState; } }
    public BaseAIState GetEnemySkillState { get { return EnemySkillState; } }

    public EnemyAIBehavior()
    {

        InitializeState();
        Initialize();
    }

    public override void Initialize()
    {
        InitializeState();
        if (EnemyDefaultAttackState == null || EnemySkillState == null) return;
        EnemyStartState = new EnemyAttackState(EnemyDefaultAttackState, EnemySkillState);

        StartState = EnemyStartState;
    }


    protected abstract void InitializeState();
}


public class EnemyAI_HipRat_Behavior : EnemyAIBehavior
{

    int attackCount = 1;
    protected override void InitializeState()
    {
        EnemySkillState = new EnemySkill_MultiAttack_State(2);
        EnemyDefaultAttackState = new EnemySkill_MultiAttack_State(attackCount);
    }
}


public class EnemyAI_Spray_Behavior : EnemyAIBehavior
{
    int attackCount = 1;
    protected override void InitializeState()
    {
       // EnemySkillState = new EnemySkill_AttackRecoverHP_State();
        EnemySkillState = new EnemySkill_DackAttack_State();

        EnemyDefaultAttackState = new EnemySkill_MultiAttack_State(attackCount);
    }
}

public class EnemyAI_WasteBasket_Behavior : EnemyAIBehavior
{
    int attackCount = 1;
    protected override void InitializeState()
    {
        EnemySkillState = new EnemySkill_AllEnemyRecoverHP_State();
        EnemyDefaultAttackState = new EnemySkill_MultiAttack_State(attackCount);
    }
}

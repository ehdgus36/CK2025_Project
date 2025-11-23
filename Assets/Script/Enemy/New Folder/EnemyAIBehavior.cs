using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;



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

    public  override void Initialize()
    {
        if (EnemyDefaultAttackState == null || EnemySkillState == null) return;
        EnemyStartState = new EnemyAttackState(EnemyDefaultAttackState, EnemySkillState);

        StartState = EnemyStartState;
    }


    protected abstract void InitializeState(); // 생성할때 단한번
}

public class EnemyAI_Custom_Behavior: EnemyAIBehavior
{

    int attackCount = 1;
    public EnemyAI_Custom_Behavior(BaseAIState Skill)
    {
        EnemySkillState = Skill;
        Initialize();
    }


    protected override void InitializeState()
    {
        EnemySkillState = new EnemySkill_MultiAttack_State(2);
        EnemyDefaultAttackState = new EnemySkill_MultiAttack_State(attackCount);
    }
}

public class EnemyAI_CustomSkill2_Behavior : EnemyAI_HIPPOP_Behavior
{

    
    public EnemyAI_CustomSkill2_Behavior(BaseAIState Skill, BaseAIState Skill2)
    {
        EnemySkillState = Skill;
        EnemySkillState2 = Skill2;
        Initialize();
    }
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
        EnemySkillState = new EnemySkill_AttackRecoverHP_State(1, .1f);
        //EnemySkillState = new EnemySkill_DackAttack_State();

        EnemyDefaultAttackState = new EnemySkill_MultiAttack_State(attackCount);
    }
}

public class EnemyAI_WasteBasket_Behavior : EnemyAIBehavior
{
    int attackCount = 1;
    protected override void InitializeState()
    {
        EnemySkillState = new EnemySkill_AllEnemyRecoverHP_State(1 , .1f);
        EnemyDefaultAttackState = new EnemySkill_MultiAttack_State(attackCount);
    }
}

public class EnemyAI_Test_Behavior : EnemyAIBehavior
{
    int attackCount = 1;
    protected override void InitializeState()
    {
        EnemySkillState = new EnemySkill_RhythmReverse_State();
        EnemyDefaultAttackState = new EnemySkill_MultiAttack_State(attackCount);
    }
}


public class EnemyAI_HIPPOP_Behavior : EnemyAIBehavior
{
    [SerializeReference] protected BaseAIState EnemySkillState2;
    int attackCount = 1;
    protected override void InitializeState()
    {
        EnemySkillState = new EnemySkill_BarbedArmor_State(1);
        EnemySkillState2 = new EnemySkill_MultiAttack_State(2);
        EnemyDefaultAttackState = new EnemySkill_MultiAttack_State(attackCount);
    }

    public override void Initialize()
    {           
        if (EnemyDefaultAttackState == null || EnemySkillState == null) return;
        EnemyStartState = new BossAttackState(EnemyDefaultAttackState, EnemySkillState , EnemySkillState2);

        StartState = EnemyStartState;
    }

    public void SwapSkill()
    {
        (EnemySkillState, EnemySkillState2) = (EnemySkillState2, EnemySkillState);
    }
}

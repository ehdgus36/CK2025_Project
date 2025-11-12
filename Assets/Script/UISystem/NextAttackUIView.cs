using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NextAttackUIView : MonoBehaviour
{
    public enum AttackIconEnum
    {
        Attack, RecverHP, MultiRecverHP ,DackCountAttack , RhythmRevers,BarbeArmor ,
        All_Volumeup, Barrier_DMG, 
        Buff_Burnout, Buff_Burnup, Confuse_DMG, Confuse_Heal, Confuse_PC, DMG_Gold, Double_Damage, Heal_All, Heal_HP,
        Heal_Lowest, HP_Volumeup, Poison_ATK, Self_Spike_Barrier, Self_Volumeup, Spike_Enemy

    }

    [System.Serializable]
    struct AttackIconData
    {
        public AttackIconEnum Key;
        public Sprite icon;
    }

    [SerializeField] Image AttackIcon;
    [SerializeField] TextMeshProUGUI NextStateText;
    [SerializeField] TextMeshProUGUI DescText;

    [SerializeField] AttackIconData[] IconDatas;
    public void UpdateUI(EnemyData enemyData, EnemyAIBehavior enemyAIBehavior)
    {
        BaseAIState EnemyAction = null; //enemy가 어떤상태일지 비교를 위한 변수
        AttackIconEnum iconEnum = AttackIconEnum.Attack;

        int viewDamage = enemyData.CurrentDamage;

        int viewAttackCount = 1;

        string attackDesc = "NULL";

        if (enemyData.CurrentSkillPoint >= enemyData.MaxSkillPoint)
        {
            EnemyAction = enemyAIBehavior.GetEnemySkillState;
        }
        else
        {
            EnemyAction = enemyAIBehavior.GetEnemyDefaultAttackState;
        }

        switch (EnemyAction)
        {
            case EnemySkill_MultiAttack_State:
                break;
        }


        for (int i = 0; i < enemyData.EnemyUnitData.buffs.Count; i++)
        {
            switch (enemyData.EnemyUnitData.buffs[i])
            {
                case FireBuff buff:                    
                    break;

                case AttackDamageDownBuff buff:
                    buff.PreviewBuffEffect(viewDamage, out viewDamage);
                    break;
            }
        }


        if (viewAttackCount > 1)
            NextStateText.text = string.Format("{0}X{1}", viewDamage.ToString(), viewAttackCount.ToString());
        else if (viewAttackCount == 1)
            NextStateText.text = string.Format("{0}", viewDamage.ToString());

        DescText.text = attackDesc;


        for (int i = 0; i < IconDatas?.Length; i++)
        {
            if (IconDatas[i].Key == iconEnum)
            {
                AttackIcon.sprite = IconDatas[i].icon;
            }
        }


    }


}



//switch (EnemyAction)
//{
//    case EnemySkill_AttackRecoverHP_State state:
//        iconEnum = AttackIconEnum.RecverHP;

//        viewDamage = enemyData.CurrentDamage;
//        viewAttackCount = state.AttackCount;

//        if(viewAttackCount ==1)
//            attackDesc = "상대가 입은 데미지만큼 체력 회복";
//        if (viewAttackCount == 2)
//            attackDesc = "상대가 입은 데미지의 2배만큼 체력 회복";
//        break;
//    case EnemySkill_AllEnemyRecoverHP_State state:
//        iconEnum = AttackIconEnum.MultiRecverHP;

//        viewDamage = enemyData.CurrentDamage;
//        attackDesc = "상대가 입은 데미지만큼 아군과 자신의 체력 회복";
//        break;

//    case EnemySkill_BarbedArmor_State state:
//        iconEnum = AttackIconEnum.BarbeArmor;

//        viewDamage = enemyData.CurrentDamage;

//        attackDesc = "공격받으면 상대에게 50%의 데미지 피격";
//        break;

//    case EnemySkill_MultiAttack_State state:
//        iconEnum = AttackIconEnum.Attack;

//        viewDamage = enemyData.CurrentDamage;
//        viewAttackCount = state.AttackCount;
//        if (viewAttackCount == 1)
//        {
//            attackDesc = "1회 공격";
//        }
//        if (viewAttackCount == 2)
//            attackDesc = "연속으로 2회 공격";
//        if (viewAttackCount == 3)
//            attackDesc = "연속으로 3회 공격";
//        break;

//    case EnemySkill_DackAttack_State state:
//        iconEnum = AttackIconEnum.DackCountAttack;
//        viewDamage = state.dackCount;

//        attackDesc = "플레이어의 덱 개수만큼의 데미지로 공격";


//        break;
//    case EnemySkill_RhythmReverse_State state:
//        iconEnum = AttackIconEnum.RhythmRevers;
//        viewDamage = enemyData.CurrentDamage;
//        attackDesc = "리듬 게임의 화살표가 반대로 표시";
//        break;
//}

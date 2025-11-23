using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NextAttackUIView : MonoBehaviour
{
    public enum AttackIconEnum
    {
        Attack, RecverHP, MultiRecverHP ,DackCountAttack , RhythmRevers,BarbeArmor
    }

    [System.Serializable]
    struct AttackIconData
    {
        public AttackIconEnum Key;
        public Sprite icon;
    }

    [SerializeField] Image AttackIcon;
    [SerializeField] TextMeshProUGUI NextStateText;

    [SerializeField] AttackIconData[] IconDatas;
    public void UpdateUI(EnemyData enemyData, EnemyAIBehavior enemyAIBehavior)
    {
        BaseAIState EnemyAction = null; //enemy가 어떤상태일지 비교를 위한 변수
        AttackIconEnum iconEnum = AttackIconEnum.Attack;

        int viewDamage = enemyData.CurrentDamage;

        int viewAttackCount = 1;

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
            case EnemySkill_AttackRecoverHP_State state:
                iconEnum = AttackIconEnum.RecverHP;

                viewDamage = enemyData.CurrentDamage;
                viewAttackCount = state.AttackCount;
                break;
            case EnemySkill_AllEnemyRecoverHP_State state:
                iconEnum = AttackIconEnum.MultiRecverHP;

                viewDamage = enemyData.CurrentDamage;
                break;

            case EnemySkill_BarbedArmor_State state:
                iconEnum = AttackIconEnum.BarbeArmor;

                viewDamage = enemyData.CurrentDamage;
                break;

            case EnemySkill_MultiAttack_State state:
                iconEnum = AttackIconEnum.Attack;

                viewDamage = enemyData.CurrentDamage;
                viewAttackCount = state.AttackCount;
                break;
            case EnemySkill_DackAttack_State state:
                iconEnum = AttackIconEnum.DackCountAttack;
                viewDamage = state.dackCount;

                break;
            case EnemySkill_RhythmReverse_State state:
                iconEnum = AttackIconEnum.RhythmRevers;
                viewDamage = enemyData.CurrentDamage;

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


        for (int i = 0; i < IconDatas?.Length; i++)
        {
            if (IconDatas[i].Key == iconEnum)
            {
                AttackIcon.sprite = IconDatas[i].icon;
            }
        }


    }


}

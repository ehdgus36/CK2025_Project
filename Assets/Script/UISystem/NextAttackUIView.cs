using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NextAttackUIView : MonoBehaviour
{
    public enum AttackIconEnum
    {  
        Attack,RecverHP,Attack_Two
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
            case EnemySkill_MultiAttack_State state:
                iconEnum = AttackIconEnum.Attack;

                if (state.AttackCount > 1)
                    NextStateText.text = string.Format("{0}X{1}", enemyData.CurrentDamage.ToString(), state.AttackCount.ToString());
                else if(state.AttackCount ==1)
                    NextStateText.text = string.Format("{0}", enemyData.CurrentDamage.ToString());
                                                             
                break;


            case EnemySkill_AttackRecoverHP_State state:
                iconEnum = AttackIconEnum.RecverHP;
                
                if (state.AttackCount > 1)
                    NextStateText.text = string.Format("{0}X{1}", enemyData.CurrentDamage.ToString(), state.AttackCount.ToString());
                else if (state.AttackCount == 1)
                    NextStateText.text = string.Format("{0}", enemyData.CurrentDamage.ToString());
                break;

            case EnemySkill_AllEnemyRecoverHP_State state:
                iconEnum = AttackIconEnum.RecverHP;
                
                NextStateText.text = string.Format("{0}", state.RecoverValue.ToString());                                          
                break;


            case EnemySkill_DackAttack_State state:
                iconEnum = AttackIconEnum.RecverHP;

                NextStateText.text = string.Format("{0}", state.dackCount.ToString());
                break;
        }


        for (int i = 0; i < IconDatas?.Length; i++)
        {
            if (IconDatas[i].Key == iconEnum)
            {
                AttackIcon.sprite = IconDatas[i].icon;
            }
        }

        
    }

    
}

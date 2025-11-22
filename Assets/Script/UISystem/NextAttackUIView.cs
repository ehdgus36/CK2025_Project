using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextAttackUIView : MonoBehaviour
{
    public enum AttackIconEnum
    {
        Attack, RecverHP, MultiRecverHP, DackCountAttack, RhythmRevers, BarbeArmor,
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
    [SerializeField] Image SkillIcon;
    [SerializeField] TextMeshProUGUI NextSstateText;
    [SerializeField] TextMeshProUGUI SkillStateText;
    [SerializeField] TextMeshProUGUI DescText;

    [SerializeField] Image Base_Image;
    [SerializeField] Image Skill_Image;

    [SerializeField] AttackIconData[] IconDatas;

    [SerializeField] Image Attackimage;
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

            case EnemySkill_DMG_Gold_State st:

                viewDamage = st.Damage;
                iconEnum = AttackIconEnum.DMG_Gold;

                if (viewDamage == 8)
                    attackDesc = "돈을 던져 <color=#ff2e50>약한</color> 피해를 준다.";

                if (viewDamage == 12)
                    attackDesc = "돈다발을 던져 <color=#ff2e50>강력한</color> 피해를 준다.";

                break;

            case EnemySkill_AttackRecoverHP_State st:
                iconEnum = AttackIconEnum.Heal_HP;
                attackDesc = "자신의 체력을 <color=#0ab52b>조금</color> 회복한다.";

                break;

            case EnemySkill_Buff_Burnup_State st:
                iconEnum = AttackIconEnum.Buff_Burnup;
                attackDesc = "<color=#d98f1c>번업</color>을 2 부여한다.";

                break;

            case EnemySkill_Buff_Burnout_State st:
                iconEnum = AttackIconEnum.Buff_Burnout;
                attackDesc = "<color=#d98f1c>번아웃</color>을 2 부여한다.";

                break;

            case EnemySkill_HP_Volumeup_State st:
                iconEnum = AttackIconEnum.HP_Volumeup;
                attackDesc = "자신의 체력을 <color=#ff2e50>조금</color> 깎고,\n<color=#d98f1c>볼륨업</color>을 4 부여한다.";

                break;

            case EnemySkill_Heal_Lowest_State st:
                iconEnum = AttackIconEnum.Heal_Lowest;
                attackDesc = "체력이 가장 낮은 아군을 <color=#0ab52b>적당히</color> 회복한다.";

                break;

            case EnemySkill_PoisonAttack_State st:
                iconEnum = AttackIconEnum.Poison_ATK;
                attackDesc = string.Format("<color=#d98f1c>중독</color>을 {0} 부여한다.", st.PoisonBuffTurn);
                break;

            case EnemySkill_Self_Volumeup_State st:
                iconEnum = AttackIconEnum.Self_Volumeup;
                attackDesc = "자신에게 <color=#d98f1c>볼륨업</color>을 10 부여한다.";
                break;

            case EnemySkill_BarrierAttack_State st:
                iconEnum = AttackIconEnum.Barrier_DMG;

                
                viewDamage = st.GetAttack;
                attackDesc = "자신에게 베리어를 <color=#0ab52b>20</color>주고,\r\n<color=#ff2e50>약한</color> 피해를 5번 준다.";
                break;

            case EnemySkill_JAZZBOSS_ALL_VolumeUp_State st:
                
                iconEnum = AttackIconEnum.All_Volumeup;
                attackDesc = "노이즈와 자신에게 <color=#d98f1c>볼륨업</color>을 7 부여한다.";
                break;

            case EnemySkill_AllEnemyRecoverHP_State st:
                
                iconEnum = AttackIconEnum.Heal_All;
                attackDesc = "모든 아군의 체력을 <color=#0ab52b>적당히</color> 회복한다.";
                break;

            case EnemySkill_AllBarbedArmor_State st:
                iconEnum = AttackIconEnum.Spike_Enemy;
                attackDesc = "모든 아군에게 <color=#d98f1c>가시</color>를 2 부여한다.";
                break;

            case EnemySkill_HpRecover_ReversRhythm_State st:
                iconEnum = AttackIconEnum.Confuse_Heal;

                attackDesc = "<color=#d98f1c>혼란</color>을 1 부여하고,\n자신의 체력을<color=#0ab52b>많이</color> 회복한다.";

                break;

            case EnemySkill_RhythmReverse_State st: // 데미지
                iconEnum = AttackIconEnum.Confuse_DMG;
                iconEnum = AttackIconEnum.Confuse_PC;

                viewDamage = st.CustomDamage;

                if (st.CustomDamage == 0)
                {
                    attackDesc = "<color=#d98f1c>혼란</color>을 1 부여한다.";
                    viewDamage = enemyData.CurrentDamage;
                }
                if (st.CustomDamage == 36)
                    attackDesc = "<color=#d98f1c>혼란</color>을 2 부여하고,\n<color=#ff2e50>치명적인</color> 피해를 준다.";


                break;

            case EnemySkill_BarbedArmor_Barrier_PlayerVolumeUp_State st:
                iconEnum = AttackIconEnum.Self_Spike_Barrier;

                attackDesc = "노이즈에게 <color=#d98f1c>볼륨업</color>을 20 부여하고,\n자신에게 <color=#d98f1c>가시</color> 2와 베리어 <color=#0ab52b>30</color>을 준다.";
                break;

            case EnemySkill_MultiAttack_State st:

                if (st.AttackCount == 1)
                {
                    iconEnum = AttackIconEnum.Attack;
                    attackDesc = "<color=#ff2e50>수치만큼</color> 피해를 준다.";

                    viewDamage = enemyData.CurrentDamage;
                }

                if (st.AttackCount == 2)
                {
                    iconEnum = AttackIconEnum.Double_Damage;
                    attackDesc = "<color=#ff2e50>약한</color> 피해를 2번 준다.";
                }
                break;
        }


        if (iconEnum == AttackIconEnum.Attack)
        {
            Base_Image.gameObject.SetActive(true);
            Skill_Image.gameObject.SetActive(false);
        }
        else
        {
            Base_Image.gameObject.SetActive(false);
            Skill_Image.gameObject.SetActive(true);
        }





        for (int i = 0; i < enemyData.EnemyUnitData.buffs.Count; i++)
        {
            if (enemyData.EnemyUnitData.buffs[i].GetBuffDurationTurn() <= 0) continue;

            switch (enemyData.EnemyUnitData.buffs[i])
            {             
                case VolumeUPBuff buff:
                    buff.PreviewBuffEffect(viewDamage, out viewDamage);
                    break;
            }
        }



        for (int i = 0; i < enemyData.EnemyUnitData.buffs.Count; i++)
        {

            if (enemyData.EnemyUnitData.buffs[i].GetBuffDurationTurn() <= 0) continue;

            switch (enemyData.EnemyUnitData.buffs[i])
            {
                case FireBuff buff:
                    break;

                case AttackDamageDownBuff_Mute buff:
                    buff.PreviewBuffEffect(viewDamage, out viewDamage);
                    break;

                case AttackDamageDownBuff buff:
                    buff.PreviewBuffEffect(viewDamage, out viewDamage);
                    break;
               
             
            }
        }




        NextSstateText.text = string.Format("{0}", viewDamage.ToString());
        SkillStateText.text = string.Format("{0}", viewDamage.ToString());
        DescText.text = attackDesc;


        for (int i = 0; i < IconDatas?.Length; i++)
        {
            if (IconDatas[i].Key == iconEnum)
            {
                AttackIcon.sprite = IconDatas[i].icon;
                SkillIcon.sprite = IconDatas[i].icon;
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

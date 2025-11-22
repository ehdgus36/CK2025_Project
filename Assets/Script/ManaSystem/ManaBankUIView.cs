using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManaBankUIView : DynamicUIObject
{
    [SerializeField] GameObject SkillButton;
    [SerializeField] Image ManaBankFill;
    [SerializeField] TextMeshProUGUI SkillPointText;
    [SerializeField] int manas;
    [SerializeField] GameObject ActiveSkill;
    Material Skill_Bar;
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA;

    bool onecSound = false;

    private void OnEnable()
    {
        Skill_Bar = ManaBankFill.material;
        GameObject.Find("Skill_Cut").gameObject?.SetActive(false);
        SkillButton.gameObject.GetComponent<SelectExcutCard>().enabled = false;
        //내부 프로퍼티만 초기화

    }

    public override void UpdateUIData(object update_ui_data)
    {
        manas = (int)update_ui_data;

        int MaxSkillPoint = GameManager.instance.ItemDataLoader.stickerData.CardCount;

        if (MaxSkillPoint == 0) gameObject.SetActive(false);

        int mana = Mathf.Clamp((int)update_ui_data, 0, MaxSkillPoint);

        if ((int)update_ui_data > MaxSkillPoint)
        {
            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, mana);
        }
        

        if (mana >= MaxSkillPoint)
        {
            SkillButton.gameObject.GetComponent<SelectExcutCard>().enabled = true;
            Skill_Bar?.SetFloat("_Health", 1);
            ActiveSkill.SetActive(true);
            
            // 준비 사운드 사운드는 1회성
            if (onecSound == false)
            {
                onecSound = true;
                GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/UI/Skill_Ready");
            }

        }
        else 
        {
            onecSound = false;
            SkillButton.gameObject.GetComponent<SelectExcutCard>().enabled = false;

            Skill_Bar?.SetFloat("_Health", 0);
            ActiveSkill.SetActive(false);
        }
        //Cut Angle Size

        if (Skill_Bar != null)
        {
            if ((float)mana / (float)MaxSkillPoint == 1.0f)
            {
                Skill_Bar?.SetFloat("Cut Angle Size", 0);

            }
            else
            {
                Skill_Bar?.SetFloat("Cut Angle Size", 0.065f);
            }
            
        }

        ManaBankFill.fillAmount = (float)mana / (float)MaxSkillPoint;

        SkillPointText.text = mana.ToString() + "/" + MaxSkillPoint.ToString();
    }

}

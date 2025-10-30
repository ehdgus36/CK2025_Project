using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManaBankUIView : DynamicUIObject
{
    [SerializeField] GameObject SkillButton;
    [SerializeField] Image ManaBankFill;
    [SerializeField] TextMeshProUGUI SkillPointText;
    [SerializeField] int manas;
    Material Skill_Bar;
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA;

    private void OnEnable()
    {
        Skill_Bar = ManaBankFill.material;
        GameObject.Find("Skill_Cut").gameObject.SetActive(false);
        //내부 프로퍼티만 초기화

    }

    public override void UpdateUIData(object update_ui_data)
    {
        manas = (int)update_ui_data;

        int mana = Mathf.Clamp((int)update_ui_data, 0, ManaBankSystem.MAX_BANK_MANA); 
        if (mana >= 10)
        {
            SkillButton.gameObject.SetActive(true);
            Skill_Bar?.SetFloat("_Health", 1);

        }
        else 
        {
            //SkillButton?.gameObject.SetActive(false);
            Skill_Bar?.SetFloat("_Health", 0);
        }
        //Cut Angle Size

        if (Skill_Bar != null)
        {
            if ((float)mana / (float)ManaBankSystem.MAX_BANK_MANA == 1.0f)
            {
                Skill_Bar?.SetFloat("Cut Angle Size", 0);

            }
            else
            {
                Skill_Bar?.SetFloat("Cut Angle Size", 0.065f);
            }
            
        }

        ManaBankFill.fillAmount = (float)mana / (float)ManaBankSystem.MAX_BANK_MANA;

        SkillPointText.text = mana.ToString()+ "/"  + ManaBankSystem.MAX_BANK_MANA.ToString();
    }

}

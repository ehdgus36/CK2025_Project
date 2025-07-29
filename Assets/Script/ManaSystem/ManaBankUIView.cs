using UnityEngine;

using UnityEngine.UI;

public class ManaBankUIView : DynamicUIObject
{
    [SerializeField] Button SkillButton;
    [SerializeField] Image ManaBankFill;
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA;

    private void OnEnable()
    {
        //내부 프로퍼티만 초기화
        
    }

    public override void UpdateUIData(object update_ui_data)
    {
        
        int mana = (int)update_ui_data;
        if (mana >= 20)
        {
            SkillButton?.gameObject.SetActive(true);
            
        }
        else 
        {
            SkillButton?.gameObject.SetActive(false);
        }


        ManaBankFill.fillAmount = (float)mana / (float)ManaBankSystem.MAX_BANK_MANA;
        
    }

}

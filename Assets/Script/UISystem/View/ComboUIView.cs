using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
public class ComboUIView : DynamicUIObject
{
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.COMBO_DATA;

    [SerializeField] TextMeshProUGUI ComboText;
    [SerializeField] GameObject MaxButtonUI;
    int comboNum = 0;

    public override void UpdateUIData(object update_ui_data)
    {
        if ((int)update_ui_data >= 99999)
        {
            ComboText.gameObject.SetActive(false);
            MaxButtonUI.gameObject.SetActive(true);

            return;
        }

        comboNum = (int)update_ui_data;
        ComboText.text = "x" + ((int)comboNum).ToString("D5");
    }

    
}

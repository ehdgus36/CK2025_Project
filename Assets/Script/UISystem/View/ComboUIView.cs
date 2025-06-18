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
        
        if ((int)update_ui_data >= 999999)
        {
            ComboText.gameObject.SetActive(false);
            MaxButtonUI.gameObject.SetActive(true);

            return;
        }


        ComboText.gameObject.SetActive(true);
        comboNum = (int)update_ui_data;
        ComboText.text = "x" + ((int)comboNum).ToString("D6");
    }

    public void DisableButton()
    {
        MaxButtonUI.GetComponent<Button>().interactable = false;
    }
    public void EnableButton()
    {
        MaxButtonUI.GetComponent<Button>().interactable = true;
    }

}

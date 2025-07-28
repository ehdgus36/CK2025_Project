using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
public class ComboUIView : DynamicUIObject
{
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA;

    [SerializeField] TextMeshProUGUI ComboText;
    [SerializeField] GameObject MaxButtonUI;
    [SerializeField] Sprite MaxBGSprite;

    [SerializeField] Sprite ComboSprite;
    [SerializeField] Image BGImage;
    
    int comboNum = 0;

    public override void UpdateUIData(object update_ui_data)
    {
        
        if ((int)update_ui_data >= 999999)
        {
            ComboText.gameObject.SetActive(false);
            MaxButtonUI.gameObject.SetActive(true);
            BGImage.sprite = MaxBGSprite;
            return;
        }


        ComboText.gameObject.SetActive(true);
        BGImage.sprite = ComboSprite;
        comboNum = (int)update_ui_data;
        ComboText.text = "x" + ((int)comboNum).ToString("D6");
    }

    public void DisableButton()
    {
        MaxButtonUI.GetComponent<Button>().interactable = false;
        BGImage.color = new Color(1, 1, 1, 0);
    }
    public void EnableButton()
    {
        MaxButtonUI.GetComponent<Button>().interactable = true;
        BGImage.color = new Color(1, 1, 1, 1);
    }

}

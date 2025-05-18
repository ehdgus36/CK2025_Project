using UnityEngine;
using TMPro;

public class StageUIView : DynamicUIObject
{
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.STAGE_DATA;
    [SerializeField] TextMeshProUGUI StageText;
    public override void UpdateUIData(object update_ui_data)
    {
        StageText.text = update_ui_data.ToString();
    }

}

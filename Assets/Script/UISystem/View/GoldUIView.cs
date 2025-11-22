using UnityEngine;
using TMPro;

public class GoldUIView : DynamicUIObject
{
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA;
    [SerializeField] TextMeshProUGUI GoldText;

    public override void UpdateUIData(object update_ui_data)
    {
        // GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA key값의 데이터 타입은 int
        GoldText.text = update_ui_data.ToString(); 
    }


   
}

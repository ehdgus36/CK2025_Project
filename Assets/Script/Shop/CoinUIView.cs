using UnityEngine;
using TMPro;

public class CoinUIView : DynamicUIObject
{
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA;

    [SerializeField] TextMeshProUGUI GoldText;

    public override void UpdateUIData(object update_ui_data)
    {
        int coin = (int)update_ui_data;
        GoldText.text = coin.ToString();
    }

   
}

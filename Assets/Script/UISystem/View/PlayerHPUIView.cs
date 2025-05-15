using UnityEngine;
using TMPro;
using System;

public class PlayerHPUIView : DynamicUIObject
{
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA;
    [SerializeField] TextMeshProUGUI HP_Text;
    public override void UpdateUIData(object update_ui_data)
    {
        UnitData PlayerData = (UnitData)update_ui_data;
        HP_Text.text = string.Format("{0}/{1}", PlayerData.CurrentHp, PlayerData.MaxHp);

    }
}

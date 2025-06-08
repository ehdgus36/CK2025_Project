using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : DynamicUIObject
{
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA;
    [SerializeField] HP_Bar HP_bar;
  
    void UpdataStatus(UnitData data)
    {
        HP_bar.UpdateUI(data.MaxHp, data.CurrentHp);
    }

    public override void UpdateUIData(object update_ui_data)
    {
        UnitData playerData = (UnitData)update_ui_data;

        if (playerData != null)
        { 
            UpdataStatus(playerData);
        }
    }
}

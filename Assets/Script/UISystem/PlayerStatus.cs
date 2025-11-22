using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : DynamicUIObject
{
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA;
    [SerializeField] HP_Bar HP_bar;
    [SerializeField] Barrier_ViewUI Barrier_viewUI;
    [SerializeField] Buff_Icon_UI Buff_Icon_UI;

    [SerializeField] bool isManageble = true;
    private void Awake()
    {
        if (isManageble == false)
        {
            GameDataSystem.DynamicGameDataSchema.AddDynamicUIDataBase(DynamicDataKey, this);
        }
    }


    void UpdataStatus(UnitData data)
    {
        HP_bar?.UpdateUI(data.MaxHp, data.CurrentHp);
        Barrier_viewUI?.UpdateUI(data.CurrentBarrier);
        Buff_Icon_UI?.UpdateBuffIcon(data.buffs);
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

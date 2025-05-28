using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : DynamicUIObject
{
    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA;
    [SerializeField] Image Hpfill;
    [SerializeField] TextMeshProUGUI Hptext;

    int MaxHP;
    int CurrentHP;


   
    void UpdataStatus(int maxHp , int currentHp)
    {
        (MaxHP, CurrentHP) = (maxHp, currentHp);


        Hpfill.fillAmount = (float)CurrentHP / (float)MaxHP;


        Hptext.text = maxHp.ToString() + "/" + CurrentHP.ToString();
    }

    public override void UpdateUIData(object update_ui_data)
    {
        UnitData playerData = (UnitData)update_ui_data;

        if (playerData != null)
        {

            UpdataStatus(playerData.MaxHp, playerData.CurrentHp);
        }
    }
}

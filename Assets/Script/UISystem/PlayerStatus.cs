using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : DynamicUIObject
{
    [SerializeField] Image Hpfill;
    [SerializeField] TextMeshProUGUI Hptext;

    int MaxHP;
    int CurrentHP;

    public void UpdataStatus(int maxHp , int currentHp)
    {
        (MaxHP, CurrentHP) = (maxHp, currentHp);


        Hpfill.fillAmount = (float)CurrentHP / (float)MaxHP;


        Hptext.text = CurrentHP.ToString();
    }

    public override void UpdateUIData(object update_ui_data)
    {
        UnitData playerData = (UnitData)update_ui_data;

        UpdataStatus(playerData.MaxHp, playerData.CurrentHp);
    }
}

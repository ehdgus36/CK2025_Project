using UnityEngine;
using UnityEngine.UI;

public class UpGradeBarUIView : DynamicUIObject
{

    [SerializeField] Image Fill;
    [SerializeField] GameObject FillBarSprite;
    float MaxPoint = 100;
    [SerializeField] int currentPoint;

    public override string DynamicDataKey => GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA;

   


  
    public override void UpdateUIData(object update_ui_data)
    {
        if ((int)update_ui_data < (int)MaxPoint)
        { 
            FillBarSprite.SetActive(false);
        }

        if ((int)update_ui_data >= (int)MaxPoint)
        {
            FillBarSprite.SetActive(true);
        }


        currentPoint = (int)update_ui_data;

        Fill.fillAmount = ((float)((int)update_ui_data) / MaxPoint);


    }
}

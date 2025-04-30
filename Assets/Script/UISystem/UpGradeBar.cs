using UnityEngine;
using UnityEngine.UI;

public class UpGradeBar : MonoBehaviour
{
    [SerializeField] Sprite[] UpGradeSprite;
    [SerializeField] Image UpGradebar;
    int MaxPoint = 5;
    int CurrentPoint = 0;

    public int GetCurrentPoint(){ return CurrentPoint; }


    public void SetPoint(int point)
    {
        if (CurrentPoint == MaxPoint)
        {

            //강화 카드 추가;
            CurrentPoint = 0;
            UpGradebar.sprite = UpGradeSprite[CurrentPoint];
            return;
        } 
        
        
        CurrentPoint += point;
        

        UpGradebar.sprite = UpGradeSprite[CurrentPoint];


    }
    
}

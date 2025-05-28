using UnityEngine;

public class FoodEvent : ExecutEvent
{
    [SerializeField] int FailDamage; // 실패시 데이지
    [SerializeField] int Recovery; // 회복량

    public override void Execute()
    {
        int randData = Random.Range(0, 2);
        if (randData == 0)
        { 
            // 성공
        }

        if (randData == 1)
        {
            // 실패
        }

    }

    
}

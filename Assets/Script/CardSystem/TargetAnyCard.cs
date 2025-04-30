using UnityEngine;

public class TargetAnyCard : TargetCard
{

   
  
    [SerializeField]int minIndex = 0;
    [SerializeField]int maxIndex = 2;

    public override int GetTargetIndex() 
    {
        maxIndex = GameManager.instance.EnemysGroup.Enemys.Count;
        
        return Random.Range(minIndex, maxIndex); 
    }
}

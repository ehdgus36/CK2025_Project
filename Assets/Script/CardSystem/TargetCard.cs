using UnityEngine;

public class TargetCard : Card
{
    [SerializeField]int TargetIndex = 0;

    public int GetTargetIndex() { return TargetIndex; }

    public void Initialized(int enemycount)
    {
        
    }
}

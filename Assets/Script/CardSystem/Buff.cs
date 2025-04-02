using UnityEngine;


public enum BuffType
{ 
Start, End
}

public class Buff : MonoBehaviour
{
    BuffType type;

    public BuffType GetBuffType() { return type; }
    public void StartBuff(Unit unit)
    {
         
    }
}

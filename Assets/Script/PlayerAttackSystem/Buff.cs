using UnityEngine;


public enum BuffType
{ 
Start, End
}

public class Buff : MonoBehaviour
{
    BuffType type;

    public BuffType GetBuffType() { return type; }
    public virtual void StartBuff(Unit unit)
    {
        unit.TakeDamage(1); //юс╫ц
    }
}

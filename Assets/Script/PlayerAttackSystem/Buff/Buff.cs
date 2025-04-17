using UnityEngine;


public enum BuffType
{ 
Start, End
}

public abstract class Buff
{
    [SerializeField] BuffType type;
    [SerializeField] protected int BuffDurationTurn = 1;

    
    public Buff(BuffType type, int buffDurationTurn)
    {
        this.type = type;
        BuffDurationTurn = buffDurationTurn;
        Initialize();
    }

    protected int CurrentBuffTurn;

    public virtual void Initialize()
    {
        CurrentBuffTurn = 0;
    }

    public BuffType GetBuffType() { return type; }
    public abstract void StartBuff(Unit unit);

    //public void SetBuffDuationTurn(int value) { BuffDurationTurn = value; Initialize();   }

}




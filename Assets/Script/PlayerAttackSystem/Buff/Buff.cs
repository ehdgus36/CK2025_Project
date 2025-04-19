using UnityEngine;


public enum BuffType
{ 
Start, End
}

public abstract class Buff
{
    [SerializeField] BuffType type;
    [SerializeField] protected int BuffDurationTurn = 1;
    protected int CurrentBuffTurn;

    public Buff(BuffType type, int buffDurationTurn)
    {
        this.type = type;
        BuffDurationTurn = buffDurationTurn;
        Initialize();
    }

   

    public virtual void Initialize()
    {
        CurrentBuffTurn = 0;
    }

    public int GetBuffDurationTurn() { return BuffDurationTurn; }

    public BuffType GetBuffType() { return type; }
    public abstract void StartBuff(Unit unit);

    public void AddBuffTurnCount(int addCount) { BuffDurationTurn += addCount; }

    //public void SetBuffDuationTurn(int value) { BuffDurationTurn = value; Initialize();   }

}




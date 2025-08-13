using UnityEngine;


public enum BuffType
{ 
Start, End
}

public enum BuffState
{
    Enable, Disable
}
public abstract class Buff
{
    [SerializeField] BuffType type;
    [SerializeField] private int BuffDurationTurn = 1;
    [SerializeField] protected BuffState State = BuffState.Disable;

    public BuffState GetState { get { return State; } }
  

    public Buff(BuffType type, int buffDurationTurn)
    {
        this.type = type;
        BuffDurationTurn = buffDurationTurn;
        Initialize();
    }

   

    public virtual void Initialize()
    {
       
    }

    public int GetBuffDurationTurn() { return BuffDurationTurn; }

    public BuffType GetBuffType() { return type; }
    public void StartBuff(Unit unit)
    {
        if (BuffDurationTurn <= 0)
        {
            State = BuffState.Disable;
            return;
        }
        State = BuffState.Enable;


        BuffEvent(unit);
        BuffDurationTurn--;
    }

    public abstract void BuffEvent(Unit unit);

    public void AddBuffTurnCount(int addCount) { BuffDurationTurn += addCount; }

    //public void SetBuffDuationTurn(int value) { BuffDurationTurn = value; Initialize();   }

}




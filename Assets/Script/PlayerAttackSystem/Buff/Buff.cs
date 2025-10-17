using UnityEngine;


public enum BuffType
{ 
Start, End
}

public enum BuffState
{
    Enable, Disable
}

[System.Serializable]
public abstract class Buff
{
    [SerializeField] BuffType type = BuffType.Start;
    [SerializeField] protected int BuffDurationTurn { get; private set; }
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
            unit.RemoveBuff(this);

            if (State == BuffState.Enable)
            {
                BuffEndEvent(unit);
            }

            State = BuffState.Disable;
           
            return;
        }
        State = BuffState.Enable;


        BuffEvent(unit);
        BuffDurationTurn--;

        if (BuffDurationTurn <= 0)
            unit.RemoveBuff(this);
    }

    public abstract void BuffEvent(Unit unit); // 버프가 작용할 때 이벤트

    public abstract void BuffEndEvent(Unit unit); // 버프가 끝날때 이벤트

    public abstract void PreviewBuffEffect<T>(T value , out T outobject);


    public void AddBuffTurnCount(int addCount) {
        State = BuffState.Enable;
        BuffDurationTurn += addCount; 
    }

    //public void SetBuffDuationTurn(int value) { BuffDurationTurn = value; Initialize();   }

}




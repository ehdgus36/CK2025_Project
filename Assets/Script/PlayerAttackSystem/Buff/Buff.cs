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
    [SerializeField] protected BuffType type = BuffType.Start;



    [SerializeField] protected int BuffDurationTurn { get; set; }
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
            BuffDurationTurn--;
            return;
        }
        State = BuffState.Enable;


        BuffEvent(unit);
        BuffDurationTurn--;

       
    }

    public abstract void BuffEvent(Unit unit); // 버프가 작용할 때 이벤트

    public abstract void BuffEndEvent(Unit unit); // 버프가 끝날때 이벤트

    public abstract void PreviewBuffEffect<T>(T value , out T outobject);


    public virtual void AddBuffTurnCount(int addCount, Unit buffUseUnit) {
        State = BuffState.Enable;
        BuffDurationTurn += addCount;
    }

    
    public virtual Buff Clone()
    {
        return (Buff)this.MemberwiseClone();
    }
}




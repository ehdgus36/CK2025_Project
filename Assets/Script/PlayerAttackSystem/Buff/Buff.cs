using UnityEngine;


public enum BuffType
{ 
Start, End
}

public abstract class Buff : MonoBehaviour
{
    [SerializeField] BuffType type;
    [SerializeField] protected int BuffDurationTurn = 1;

    BuffType _type;
    protected int _BuffDurationTurn = 1;
    protected int CurrentBuffTurn;

    public virtual void Initialize()
    {
        CurrentBuffTurn = 0;

        _type = type;
        _BuffDurationTurn = BuffDurationTurn;
    }

    public BuffType GetBuffType() { return type; }
    public abstract void StartBuff(Unit unit);

    public void SetBuffDuationTurn(int value) { BuffDurationTurn = value; Initialize();   }
    public abstract Buff DeepCopy();
}

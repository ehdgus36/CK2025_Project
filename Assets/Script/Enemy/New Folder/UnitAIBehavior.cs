using UnityEngine;
using System.Collections;



[System.Serializable]
public class UnitAIBehavior 
{
    protected virtual BaseAIState StartState { get; }

    BaseAIState CurrentState;
    UnitAIMachine UnitAIMachine;

    public void Excut(Unit unit, UnitAIMachine aIMachine)
    {
        UnitAIMachine = aIMachine;
        ChangeState(StartState, unit , this);    
    }

    public void ChangeState(BaseAIState state, Unit unit, UnitAIBehavior aIBehavior)
    {

        UnitAIMachine.StopCorutinExcut();
        CurrentState?.Exit(unit, aIBehavior);

        CurrentState = state;

        CurrentState?.Enter(unit, aIBehavior);
        UnitAIMachine.StartCorutinExcut(CurrentState.Excut(unit, this));
    }
}

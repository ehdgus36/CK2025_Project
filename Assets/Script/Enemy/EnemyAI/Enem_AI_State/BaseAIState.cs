using UnityEngine;
using System.Collections;


[System.Serializable]
public abstract class BaseAIState 
{
    public abstract void Enter(Unit unit, UnitAIBehavior aIBehavior);
    

    public abstract IEnumerator Excut(Unit unit, UnitAIBehavior aIBehavior); //ÄÚ·çÆ¾
    
    
    public abstract void Exit(Unit unit, UnitAIBehavior aIBehavior);
    
}

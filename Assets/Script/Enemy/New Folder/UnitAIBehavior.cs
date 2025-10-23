using UnityEngine;
using System.Collections;


using UnityEditor;


[System.Serializable]
public abstract class UnitAIBehavior 
{
    protected BaseAIState StartState;

    BaseAIState CurrentState;
    UnitAIMachine UnitAIMachine;

    public virtual void Initialize() { }
 
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


public class ReadOnlyAttribute : PropertyAttribute { }

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;  // 필드 비활성화
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;   // 이후 GUI 활성화
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}

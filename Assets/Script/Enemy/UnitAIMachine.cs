using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json;

[System.Serializable]
public class UnitAIMachine : MonoBehaviour
{
    [SerializeReference] public UnitAIBehavior aIBehavior;
    Coroutine CurrentCoroutine;

    void OnDisable()
    {
        StopCorutinExcut();
    }


    public void StartAI(Unit controll_unit)
    {
        aIBehavior.Initialize();
        
        aIBehavior.Excut(controll_unit, this);
    }

    public void StartCorutinExcut(IEnumerator coroutineExcut)
    {
        CurrentCoroutine = StartCoroutine(coroutineExcut);
    }

    public void StopCorutinExcut() 
    {
        if (CurrentCoroutine != null)
        {
            StopCoroutine(CurrentCoroutine);
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(UnitAIMachine))]
public class UnitAIMachineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 멀티 오브젝트 선택 감지
        if (targets.Length > 1)
        {
            EditorGUILayout.HelpBox(
                "Multi-object editing not supported for AIBehavior.\nPlease select a single UnitAIMachine.",
                MessageType.Warning
            );
            return;
        }

        serializedObject.Update();

        SerializedProperty valueProp = serializedObject.FindProperty("aIBehavior");

        // 현재 타입
        Type currentType = valueProp.managedReferenceValue?.GetType();
        string typeName = currentType != null ? currentType.Name : "None";

        // 버튼 UI
        if (GUILayout.Button($"Value Type: {typeName}"))
        {
            GenericMenu menu = new GenericMenu();

            // None 옵션
            menu.AddItem(new GUIContent("None"), currentType == null, () =>
            {
                valueProp.managedReferenceValue = null;
                serializedObject.ApplyModifiedProperties();
            });

            // UnitAIBehavior를 상속한 모든 타입 찾기
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(UnitAIBehavior).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var t in types)
            {
                menu.AddItem(new GUIContent(t.Name), t == currentType, () =>
                {
                    valueProp.managedReferenceValue = Activator.CreateInstance(t);
                    serializedObject.ApplyModifiedProperties();
                });
            }

            menu.ShowAsContext();
        }

        // 실제 필드 표시
        if (valueProp.managedReferenceValue != null)
        {
            EditorGUILayout.PropertyField(valueProp, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
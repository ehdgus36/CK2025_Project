
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(EnemysGroup))]
public class EnemyGroupCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // damage 제외한 나머지 필드 그리기
        SerializedProperty prop = serializedObject.GetIterator();
        bool enterChildren = true;
        while (prop.NextVisible(enterChildren))
        {
            enterChildren = false;
            if (prop.name == "UnitData") continue; // 숨기기
            if (prop.name == "HitEffect") continue;
            if (prop.name == "NoteControl") continue;
            EditorGUILayout.PropertyField(prop, true);


        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
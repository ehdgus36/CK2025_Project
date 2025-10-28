using UnityEngine;


[System.Serializable]
public struct EffectListData
{
    [SerializeField] private string _EffectCode;
    [SerializeField] private GameObject _EffectObject;
    [SerializeField] private Vector3 _EffectOffSet;

    public string EffectCode { get { return _EffectCode; } }
    public GameObject EffectObject { get { return _EffectObject; } }
    public Vector3 EffectOffSet { get { return _EffectOffSet; } }
}

[CreateAssetMenu(fileName = "EffectData" , menuName ="Scriptable/EffectData", order = int.MaxValue)]
public class EffectData : ScriptableObject
{
    [SerializeField] private EffectListData[] _effectDatas;

    public EffectListData[] EffectDatas { get { return _effectDatas; } }
}

using UnityEngine;



[System.Serializable]
struct EffectListData
{
    [SerializeField] private string _EffectCode;
    [SerializeField] private ParticleSystem _EffectObject;
    [SerializeField] private Vector3 _EffectOffSet;

    public string EffectCode { get { return _EffectCode; } }
    public ParticleSystem EffectObject { get { return _EffectObject; } }
    public Vector3 EffectOffSet { get { return _EffectOffSet; } }
}

[CreateAssetMenu(fileName = "EffectData" , menuName ="Scriptable/EffectData", order = int.MaxValue)]
public class EffectData : ScriptableObject
{
    [SerializeField] private EffectListData[] _effectDatas;

    public EffectListData[] EffectListDatas { get { return _effectDatas; } }
}

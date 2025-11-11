using UnityEngine;
using Spine.Unity;


[System.Serializable]
public struct AnimationListData
{
    [SerializeField] private string _AnimationCode;
    [SerializeField] private AnimationReferenceAsset _SpineAnimationData;

    public string AnimationCode { get { return _AnimationCode; } }
    public AnimationReferenceAsset SpineAnimationData { get { return _SpineAnimationData; } }
   
}

[CreateAssetMenu(fileName = "UnitAnimationGroupData", menuName = "Scriptable/UnitAnimationGroupData", order = int.MaxValue)]
public class UnitAnimationGroupData : ScriptableObject
{
    [SerializeField] private AnimationListData[] _AnimeDatas;

    public AnimationListData[] AnimeDatas { get { return _AnimeDatas; } }
}

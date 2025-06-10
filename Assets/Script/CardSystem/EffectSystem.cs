using UnityEngine;


[System.Serializable]
struct EffectData
{
    [SerializeField] public string EffectCode;
    [SerializeField] public ParticleSystem EffectObject;
    [SerializeField] public Vector3 EffectOffSet;
}
public class EffectSystem : MonoBehaviour
{
    [SerializeField] EffectData[] EffectDatas;
    public void PlayEffect(string effectCode, Vector3 TargetPos)
    {
        for (int i = 0; i < EffectDatas.Length; i++)
        {
            if (EffectDatas[i].EffectCode == effectCode)
            {
                EffectDatas[i].EffectObject.gameObject.transform.position = TargetPos + EffectDatas[i].EffectOffSet;
                EffectDatas[i].EffectObject.Play();
            }
        }
    }
}

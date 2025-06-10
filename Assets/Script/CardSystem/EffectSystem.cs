using UnityEngine;


struct EffectData
{
    [SerializeField] public string EffectCode;
    [SerializeField] public GameObject EffectObject;
}
public class EffectSystem : MonoBehaviour
{
    [SerializeField] EffectData[] EffectDatas;
    public void PlayEffect(string effectCode)
    { 
    
    }
}

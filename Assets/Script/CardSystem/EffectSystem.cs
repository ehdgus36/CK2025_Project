using UnityEngine;



public class EffectSystem : MonoBehaviour
{
    [SerializeField] EffectData EffectDatas;
    public void PlayEffect(string effectCode, Vector3 TargetPos)
    {
        for (int i = 0; i < EffectDatas.EffectDatas.Length; i++)
        {
            if (EffectDatas.EffectDatas[i].EffectCode == effectCode)
            {
                EffectDatas[i].EffectObject.gameObject.transform.position = TargetPos + EffectDatas[i].EffectOffSet;
                EffectDatas[i].EffectObject.Play();

                return;
            }
        }

        /// 만약 Effect가 없으면
        EffectDatas[0].EffectObject.gameObject.transform.position = TargetPos + EffectDatas[0].EffectOffSet;
        EffectDatas[0].EffectObject.Play();
    }

    public void PlayEffectAllTarget(string effectCode, Vector3 TargetPos)
    { 
    
    }
}

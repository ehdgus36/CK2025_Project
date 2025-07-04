using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectSystem : MonoBehaviour
{
    [SerializeField] EffectData EffectData;

    Dictionary<string, ParticleSystem> EffectSystemInstanceData = new Dictionary<string, ParticleSystem>();

    public void PlayEffect(string effectCode, Vector3 TargetPos) // 수정 필요 스크립터블 오브젝트에서 데이터 받아서 이펙트 생성하고 사용 딕셔너리로 관리
    {

        if (EffectSystemInstanceData.ContainsKey(effectCode) == false) // 이펙트가 생성되지 않았다면 생성후 딕셔너리에 저장
        {
            for (int i = 0; i < EffectData.EffectDatas.Length; i++)
            {
                if (EffectData.EffectDatas[i].EffectCode == effectCode)
                {
                    GameObject EffectParticleSystem = Instantiate(EffectData.EffectDatas[i].EffectObject);
                    EffectParticleSystem.transform.SetParent(this.transform);
                    EffectSystemInstanceData.Add(effectCode, EffectParticleSystem.GetComponent<ParticleSystem>());

                    break;
                }
            }
        }

        
        Vector3 offset = Vector3.zero; // 오프셋값 저장

        for(int i = 0; i < EffectData.EffectDatas.Length; i++) //스크립터블 오브젝트의 데이터에서 오프셋 값을 받아옴
        {
            if (EffectData.EffectDatas[i].EffectCode == effectCode)
            {
                offset = EffectData.EffectDatas[i].EffectOffSet;
                break;
            }
        }


        //생성된 이펙트 실행
        EffectSystemInstanceData[effectCode].transform.position = TargetPos + offset;
        EffectSystemInstanceData[effectCode].Play();
       

       
    }

    public void PlayEffectAllTarget(string effectCode, Vector3 TargetPos)
    { 
    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;






public class PostProcessingSystem : MonoBehaviour
{
    [System.Serializable]
    struct VolumeData
    {
        public string key;
        public Volume data;
    }


    [SerializeField] Volume DeffultVolume;
    [SerializeField] VolumeData[] ChangeVolumeData;

    Dictionary<string, Volume> ChangeVolumeDataDic = new Dictionary<string, Volume>();

    Coroutine PlayingStartCoroutine = null;

    public void Initialized()
    {
        for (int i = 0; i < ChangeVolumeData.Length; i++)
        {
            ChangeVolumeDataDic.TryAdd(ChangeVolumeData[i].key, ChangeVolumeData[i].data);
        }
    }


    public void ChangeVolume(string volumeName ,bool isOnce = true , float changeTime = 0.2f , float startBlendTime = 0.0f, float endBlendTime = 0.0f)
    {
        if (PlayingStartCoroutine != null)
        { 
             StopCoroutine(PlayingStartCoroutine);
        }

        if (ChangeVolumeDataDic.ContainsKey(volumeName))
        {
            PlayingStartCoroutine = StartCoroutine(PostProcessingChager(volumeName , isOnce , changeTime , startBlendTime , endBlendTime));
        }
    }

    IEnumerator PostProcessingChager(string volumeName, bool isOnce = true, float changeTime = 0.5f, float startBlendTime = 0.0f, float endBlendTime = 0.0f)
    {
        try
        {
            if (changeTime < startBlendTime) startBlendTime = changeTime;

            for (int i = 0; i < 10; i++)
            {
                ChangeVolumeDataDic[volumeName].weight += 0.1f;
                yield return new WaitForSeconds(startBlendTime / 10.0f);
            }

            yield return new WaitForSeconds(changeTime - startBlendTime);

            for (int i = 0; i < 10; i++)
            {
                ChangeVolumeDataDic[volumeName].weight -= 0.1f;
                yield return new WaitForSeconds(endBlendTime / 10.0f);
            }



            yield return null;
        }
        finally
        {
           ChangeVolumeDataDic[volumeName].weight = 0.0f;
           PlayingStartCoroutine = null;       
        }


       
    }
}

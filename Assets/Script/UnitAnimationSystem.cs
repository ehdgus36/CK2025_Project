using UnityEngine;
using System.Collections;
using Spine.Unity;
using System.Collections.Generic;

public class UnitAnimationSystem : MonoBehaviour
{
    const string IDLE = "Idle";
    const string ATTACK = "Attack";
    const string HIT = "Hit";

    const int AttackLayer = 1;

    [System.Serializable]
    struct AnimeData
    {
        [SerializeField] public string key;
        [SerializeField] public AnimationReferenceAsset animeData;
    }

    [SerializeField] SkeletonAnimation UnitAnimation;

    [SerializeField] AnimeData[] AddAnimation;

    [SerializeField] bool isattack = false;

    Dictionary<string, AnimationReferenceAsset> AnimationDatas = new Dictionary<string, AnimationReferenceAsset>();

    //private void Start()
    //{
    //    Initialize();
    //}

    public void Initialize()
    {
        if (UnitAnimation == null) Debug.LogError("스파인 스켈레톤 애니메이션이 없음");

        for (int i = 0; i < AddAnimation.Length; i++)
        {
            AnimationDatas.Add(AddAnimation[i].key, AddAnimation[i].animeData);
        }

        if (AnimationDatas.ContainsKey(IDLE)) UnitAnimation.AnimationState.SetAnimation(0, AnimationDatas[IDLE], true);           
    }

    public void PlayAnimation(string animeKey , bool loop = false)
    {
        if (loop)
        {
            if (AnimationDatas.ContainsKey(animeKey)) 
                UnitAnimation.AnimationState.SetAnimation(AttackLayer, AnimationDatas[animeKey], loop);
        }

        if (!loop)
        {
            if (AnimationDatas.ContainsKey(animeKey)) 
                UnitAnimation.AnimationState.SetAnimation(AttackLayer, AnimationDatas[animeKey], loop).Complete += clear => 
                                                                                                      { UnitAnimation.AnimationState.SetEmptyAnimation(AttackLayer, .1f); };
        }
    }

    //private void Update()
    //{
    //    if (isattack)
    //    { 
    //        isattack = false;
    //        PlayAnimation(ATTACK);
    //    }
    //}
}

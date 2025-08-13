using UnityEngine;
using System.Collections;
using Spine.Unity;
using System.Collections.Generic;
using System.Linq;
using Spine;
using static Spine.AnimationState;

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


    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (UnitAnimation == null) Debug.LogError("스파인 스켈레톤 애니메이션이 없음");

        for (int i = 0; i < AddAnimation.Length; i++)
        {
            AnimationDatas.Add(AddAnimation[i].key, AddAnimation[i].animeData);
        }
        

        if (AnimationDatas.Count > 0 ) 
            UnitAnimation.AnimationState.SetAnimation(0, AnimationDatas[AnimationDatas.FirstOrDefault().Key], true);           

    }

    public void PlayAnimation(string animeKey , bool loop = false ,
                              TrackEntryEventDelegate eventDelegate = null , TrackEntryDelegate CompleteDelegate = null, bool notEmpty = false)// notEmpty idle 로 돌아가지 않음
    {
        if (loop)
        {
            if (AnimationDatas.ContainsKey(animeKey))
            {
                UnitAnimation.AnimationState.SetAnimation(AttackLayer, AnimationDatas[animeKey], loop);
            }
            else
            {
                UnitAnimation.AnimationState.SetAnimation(AttackLayer, AnimationDatas["break_Ani"], loop);
            }
        }

        if (!loop)
        {
            if (AnimationDatas.ContainsKey(animeKey))
            {
                TrackEntry track = UnitAnimation.AnimationState.SetAnimation(AttackLayer, AnimationDatas[animeKey], loop);

                if (notEmpty == false)
                    track.Complete += clear => { UnitAnimation.AnimationState.SetEmptyAnimation(AttackLayer, 0.1f); };

                track.Complete += CompleteDelegate;
                track.Event += eventDelegate;
            }
            else 
            {
                TrackEntry track = UnitAnimation.AnimationState.SetAnimation(AttackLayer, AnimationDatas["break_Ani"], loop);

                if (notEmpty == false)
                    track.Complete += clear => { UnitAnimation.AnimationState.SetEmptyAnimation(AttackLayer, 0.1f); };

                track.Complete += CompleteDelegate;
                track.Event += eventDelegate;
            }
        }
    }

 
}

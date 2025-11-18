using UnityEngine;
using System.Collections;
using Spine.Unity;
using System.Collections.Generic;
using System.Linq;
using Spine;
using static Spine.AnimationState;

public class UnitAnimationSystem : MonoBehaviour
{

    Dictionary<string, AnimationReferenceAsset> AnimationDatas = new Dictionary<string, AnimationReferenceAsset>();

    //공격 레이어
    const int AttackLayer = 1;


    [SerializeField] SkeletonAnimation UnitAnimation;

    [SerializeField] UnitAnimationGroupData AddAnimation;

    [SerializeField] bool isattack = false;

    


    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (UnitAnimation == null) Debug.LogError("스파인 스켈레톤 애니메이션이 없음");

        for (int i = 0; i < AddAnimation.AnimeDatas.Length; i++)
        {
            AnimationDatas.Add(AddAnimation.AnimeDatas[i].AnimationCode, AddAnimation.AnimeDatas[i].SpineAnimationData);
        }

        // 애니메이션이 존재한다면 
        if (AnimationDatas.Count > 0)
        {
            //0번째 애니메이션을 기본 애니메이션으로 실행
            UnitAnimation.AnimationState.SetAnimation(0, AnimationDatas[AnimationDatas.FirstOrDefault().Key], true);
        }
    }

    public void MainLayerPlayAnimation(string animeKey, bool loop = false,
                           TrackEntryEventDelegate eventDelegate = null,
                           TrackEntryDelegate CompleteDelegate = null, bool notEmpty = true, float TimeScale = 1.0f)
    {
        if (!loop)
        {
            if (AnimationDatas.ContainsKey(animeKey))
            {

                UnitAnimation.AnimationState.SetEmptyAnimation(0, 0f);

                TrackEntry track = UnitAnimation.AnimationState.SetAnimation(0, AnimationDatas[animeKey].Animation, loop);
                track.TimeScale = TimeScale;


                if (notEmpty == false) // 애니메이션이 종료하면 자동적으로 idle 애니메이션으로 돌아감
                    track.Complete += clear => { UnitAnimation.AnimationState.SetEmptyAnimation(0, 0f); };

                track.Complete += CompleteDelegate;
                track.Interrupt += CompleteDelegate;
                track.Event += eventDelegate;

            }
            else //없으면 아무 애니메이션 재생
            {
                List<string> key = AnimationDatas.Keys.ToList();
                TrackEntry track = UnitAnimation.AnimationState.SetAnimation(0, AnimationDatas[key[0]].Animation, loop);
                track.TimeScale = TimeScale;

                if (notEmpty == false)
                    track.Complete += clear => { UnitAnimation.AnimationState.SetEmptyAnimation(0, 0f); };

                track.Complete += CompleteDelegate;
                track.Interrupt += CompleteDelegate;
                track.Event += eventDelegate;
            }
        }
    }


    /// <summary> 플레이어 애니메이션을 실행하는 함수 </summary>
    /// <remarks>필수적으로 매개변수 animekey에 값을 줘야함</remarks>
    ///  <param name="animeKey">재생할 애니메잇션 key값 </param> <param name="loop">애니메이션을 loop시킬지 여부</param>
    public void PlayAnimation(string animeKey , bool loop = false ,
                              TrackEntryEventDelegate eventDelegate = null , 
                              TrackEntryDelegate CompleteDelegate = null, bool notEmpty = false , float TimeScale = 1.0f)
    {



        

        if (loop)
        {
            if (AnimationDatas.ContainsKey(animeKey))
            {
                TrackEntry track = UnitAnimation.AnimationState.SetAnimation(AttackLayer, AnimationDatas[animeKey].Animation, loop);
                track.HoldPrevious = true;
            }
            else
            {
                UnitAnimation.AnimationState.SetAnimation(AttackLayer, AnimationDatas["Break_Ani"].Animation, loop);
            }
        }

        if (!loop)
        {
            if (AnimationDatas.ContainsKey(animeKey))
            {
                
                UnitAnimation.AnimationState.SetEmptyAnimation(AttackLayer, 0f);
                
                TrackEntry track = UnitAnimation.AnimationState.SetAnimation(AttackLayer, AnimationDatas[animeKey].Animation, loop);
                track.TimeScale = TimeScale;


                if (notEmpty == false) // 애니메이션이 종료하면 자동적으로 idle 애니메이션으로 돌아감
                    track.Complete += clear => { UnitAnimation.AnimationState.SetEmptyAnimation(AttackLayer, 0f); };

                
                track.Complete += CompleteDelegate;
                track.Interrupt += CompleteDelegate;
                track.Event += eventDelegate;
                
            }
            else //없으면 아무 애니메이션 재생
            {
                List<string> key = AnimationDatas.Keys.ToList();
                TrackEntry track = UnitAnimation.AnimationState.SetAnimation(AttackLayer, AnimationDatas[key[0]].Animation, loop);
                track.TimeScale = TimeScale;

                if (notEmpty == false)
                    track.Complete += clear => { UnitAnimation.AnimationState.SetEmptyAnimation(AttackLayer, 0f); };

                
                track.Complete += CompleteDelegate;
                track.Interrupt += CompleteDelegate;
                track.Event += eventDelegate;
            }
        }
    }
}

using System.Collections.Generic;

using Spine.Unity;
using UnityEngine;

public class GuitarSkill : MonoBehaviour
{
    [SerializeField] Transform[] Pos;
    [SerializeField] GameObject[] NotePrefab;
    [SerializeField] SpriteRenderer HitBox;

    [SerializeField] float BPM;
    [SerializeField] List<Note> Notes;
    [SerializeField] SkeletonAnimation SkillAnime;
    [SerializeField] EffectSystem EffectSystem;

    
    double CurrentTime;

    int TotalScore = 0;
    public int Score { get; private set; }

    int MaxNoteCount = 10;
    int CurrentNoteCount = 10;
    public bool Success { get; private set; }
    bool isPlay = false;


    public void OnEnable()
    {
        PlaySkill();
    }
  
    public void SkillAttack()
    {
        GameManager.instance.Player.PlayerAnimator.PlayAnimation("ultimate");
    }


    public void PlaySkill()
    {
        Success = false;
        isPlay = true;
        CurrentNoteCount = MaxNoteCount;
        CurrentTime = 0;
        Score = 0;
        TotalScore = 0;

        SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni", true);
    }



    public void Update()
    {
        if (isPlay == false) return;

        if (MaxNoteCount == TotalScore)
        {
            isPlay = false;
            Success = true;
            return;
        } 


        CurrentTime += Time.deltaTime;

        if (CurrentNoteCount != 0)
        {
            if (CurrentTime >= 60d / BPM)
            {

                Note note_s = Instantiate(NotePrefab[Random.Range(0, NotePrefab.Length)], Pos[Random.Range(0, Pos.Length)].transform.position, transform.rotation).GetComponent<Note>();
                CurrentTime -= 60d / BPM;
                Notes.Add(note_s);
                CurrentNoteCount--;

            }
        }




        if (Notes.Count != 0)
        {
            if (Notes[0].transform.position.x > HitBox.bounds.max.x) // 채보 실패
            {
                GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Effect/Strength_Attack/Click_Strength_Button_Fa");
                EffectSystem.PlayEffect("Miss_Effect", HitBox.transform.position);
                Destroy(Notes[0].gameObject);
                Notes.RemoveAt(0);
               
                return;
            }

            if (Input.GetKeyDown(Notes[0].key))
            {
                if (Notes[0].transform.position.x >= HitBox.bounds.min.x && Notes[0].transform.position.x <= HitBox.bounds.max.x) // 채보 처리
                {
                   SkillAnime.AnimationState.ClearTrack(0);
                   SkillAnime.Skeleton.SetSlotsToSetupPose();
                  // SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni", false);

                    GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Effect/Strength_Attack/Click_Strength_Button_Su");
                    GameManager.instance.PostProcessingSystem.ChangeVolume("Skill");
                    EffectSystem.PlayEffect("Perfect_Effect", HitBox.transform.position);
                    switch (Notes[0].key)
                    {
                        case KeyCode.LeftArrow:
                            SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni-left", false);
                            SkillAnime.AnimationState.AddAnimation(0, "ultimate-hamoni", true, 1f);
                            break;

                        case KeyCode.RightArrow:
                            SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni-right", false);
                            SkillAnime.AnimationState.AddAnimation(0, "ultimate-hamoni", true, 1f);
                            break;

                        case KeyCode.UpArrow:
                            SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni-up", false);
                            SkillAnime.AnimationState.AddAnimation(0, "ultimate-hamoni", true, 1f);
                            break;

                        case KeyCode.DownArrow:
                            SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni-down", false);
                            SkillAnime.AnimationState.AddAnimation(0, "ultimate-hamoni", true, 1f);
                            break;

                    }

                    Destroy(Notes[0].gameObject);
                    Notes.RemoveAt(0);

                  

                    Score++;
                    TotalScore++;
                }
                else if (Notes[0].transform.position.x < HitBox.bounds.min.x && Notes[0].transform.position.x <= HitBox.bounds.max.x) // 채보 미스
                {
                    GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Effect/Strength_Attack/Click_Strength_Button_Fa");
                    EffectSystem.PlayEffect("Miss_Effect", HitBox.transform.position);
                    Destroy( Notes[0].gameObject);
                    Notes.RemoveAt(0);
                    TotalScore++;

                }

                
               
            }
        }
       
    }
}

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
    [SerializeField] AudioSource aaa;

    
    double CurrentTime;

    int TotalScore = 0;
    public int Score { get; private set; }

    int MaxNoteCount = 10;
    int CurrentNoteCount = 10;
    public bool Success { get; private set; }
    bool isPlay = false;


    private void Start()
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
            if (Notes[0].transform.position.x > HitBox.bounds.max.x)
            {
                Destroy(Notes[0].gameObject);
                Notes.RemoveAt(0);
               
                return;
            }

            if (Input.GetKeyDown(Notes[0].key))
            {
                if (Notes[0].transform.position.x >= HitBox.bounds.min.x && Notes[0].transform.position.x <= HitBox.bounds.max.x)
                {
                    SkillAnime.AnimationState.ClearTrack(0);
                    SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni", false);
                    switch (Notes[0].key)
                    {
                        case KeyCode.LeftArrow:
                            SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni-left", false).Complete += clear => { SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni", false); }; 
                            break;

                        case KeyCode.RightArrow:
                            SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni-right", false).Complete += clear => { SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni", false); };
                            break;

                        case KeyCode.UpArrow:
                            SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni-up", false).Complete += clear => { SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni", false); };
                            break;

                        case KeyCode.DownArrow:
                            SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni-down", false).Complete += clear => { SkillAnime.AnimationState.SetAnimation(0, "ultimate-hamoni", false); };
                            break;

                    }

                    Destroy(Notes[0].gameObject);
                    Notes.RemoveAt(0);

                  

                    Score++;
                    TotalScore++;
                }
                else if (Notes[0].transform.position.x < HitBox.bounds.min.x && Notes[0].transform.position.x <= HitBox.bounds.max.x)
                {
                    Destroy( Notes[0].gameObject);
                    Notes.RemoveAt(0);
                    TotalScore++;

                }

                
                aaa.Play();
            }
        }
       
    }
}

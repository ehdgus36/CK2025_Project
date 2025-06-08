using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class NoteSystem : MonoBehaviour
{
    
    [SerializeField]Transform Good;
    [SerializeField]Transform Normal;
    [SerializeField]Transform Bad;
    [SerializeField]Transform Center;
    [SerializeField]Transform NoteCircle;


    [SerializeField] float speed;


    [SerializeField] string Verdict;


    [SerializeField] GameObject PERFECT_EFFECT;
    [SerializeField] GameObject GOOD_EFFECT;
    [SerializeField] GameObject MISS_EFFECT;
    [SerializeField] GameObject HIT_EFFECT;

    Vector3 StartNoteScale = Vector3.zero;
    public bool isTrigger = false;
    public event Action<string> NoteEvent;
    public bool isEnd = false;

    public string GetVerdict { get { return Verdict; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize()
    { 
        StartNoteScale = NoteCircle.transform.localScale;
      
    }


    //// 삭제 필요
    //private void Start()
    //{
    //    Time.timeScale = 0.5f;
    //    Initialize();
    //    PlayNote();
    //}

    public void PlayNote()
    {
        this.gameObject.SetActive(true);
        MISS_EFFECT.SetActive(false);
        PERFECT_EFFECT.SetActive(false);
        GOOD_EFFECT.SetActive(false);
        MISS_EFFECT.SetActive(false);
        HIT_EFFECT.SetActive(false);

        StartCoroutine("PlayNoteSystem");
    }
    IEnumerator PlayNoteSystem()
    {
        NoteCircle.transform.localScale = StartNoteScale;
       isEnd = false;

        while (true) {

            if (isTrigger)
            {
                isTrigger = false;
                StartCoroutine(UnActive());

                HIT_EFFECT.SetActive(true);
                break;
            }

            if (NoteCircle.localScale.x == new Vector3(2.1f, 2.1f, 2.1f).x) 
            {
                isEnd= true;
                isTrigger = true;
                HIT_EFFECT.SetActive(true);
            }

            NoteCircle.localScale = Vector3.MoveTowards(NoteCircle.localScale, new Vector3(.9f,0.9f,0.9f), speed * 0.0166666666666667f);
            yield return new WaitForSeconds(0.0166666666666667f); // 60프레임
        }
        //반복문 나와서


        //Good판정
        if (Center.localScale.x <= NoteCircle.localScale.x && Good.localScale.x >= NoteCircle.localScale.x)
        {
            Verdict = "Good";
            NoteEvent?.Invoke(Verdict);

            PERFECT_EFFECT.SetActive(true);
            int GradePoint = 0;
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, out GradePoint);

            GradePoint = GradePoint + 10;
            GameManager.instance.Player.PlayerAnimator.PlayAnimation("gard1");

            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, GradePoint);
            yield break;
        }
        //Normal판정
        if (Good.localScale.x < NoteCircle.localScale.x && Normal.localScale.x >= NoteCircle.localScale.x)
        {
            Verdict = "Normal";
            NoteEvent?.Invoke(Verdict);

            GOOD_EFFECT.SetActive(true);
            int GradePoint = 0;

            GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, out GradePoint);

            GradePoint = GradePoint + 5;
            GameManager.instance.Player.PlayerAnimator.PlayAnimation("gard1");
            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, GradePoint);
           yield break;
        }
        //Bad판정
        if (Normal.localScale.x < NoteCircle.localScale.x && Bad.localScale.x >= NoteCircle.localScale.x)
        {
            Verdict = "Bad";
            NoteEvent?.Invoke(Verdict);

            GOOD_EFFECT.SetActive(true);
            int GradePoint = 0;
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, out GradePoint);

            GradePoint = GradePoint + 5;
            GameManager.instance.Player.PlayerAnimator.PlayAnimation("gard1");
            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, GradePoint);

            yield break;
        }

        //Miss판정
        if (Bad.localScale.x < NoteCircle.localScale.x || Center.localScale.x > NoteCircle.localScale.x)
        {

            MISS_EFFECT.SetActive(true);
            Verdict = "miss";
            NoteEvent?.Invoke(Verdict);     
          
            yield return null;
        }

        yield break;

    }

    IEnumerator UnActive()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }

}

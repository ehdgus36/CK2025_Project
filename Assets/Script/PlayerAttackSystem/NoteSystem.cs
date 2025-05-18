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

    Vector3 StartNoteScale = Vector3.zero;
    public bool isTrigger = false;
    public event Action<string> NoteEvent;
    public bool isEnd = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize()
    { 
        StartNoteScale = NoteCircle.transform.localScale;
      
    }

    public void PlayNote()
    {
        this.gameObject.SetActive(true);
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
                this.gameObject.SetActive(false);
                break;
            }

            if (NoteCircle.localScale.x == Vector3.zero.x) 
            {
                isEnd= true;
                isTrigger = true;
                
            }

            NoteCircle.localScale = Vector3.MoveTowards(NoteCircle.localScale, Vector3.zero, speed * 0.0166666666666667f);
            yield return new WaitForSeconds(0.0166666666666667f); // 60프레임
        }
        //반복문 나와서


        //Good판정
        if (Center.localScale.x <= NoteCircle.localScale.x && Good.localScale.x >= NoteCircle.localScale.x)
        {
            Verdict = "Good";
            NoteEvent?.Invoke(Verdict);

            int GradePoint = 0;
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, out GradePoint);

            GradePoint = GradePoint + 10;


            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, GradePoint);
            yield break;
        }
        //Normal판정
        if (Good.localScale.x < NoteCircle.localScale.x && Normal.localScale.x >= NoteCircle.localScale.x)
        {
            Verdict = "Normal";
            NoteEvent?.Invoke(Verdict);

            int GradePoint = 0;
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, out GradePoint);

            GradePoint = GradePoint + 5;

            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, GradePoint);
           yield break;
        }
        //Bad판정
        if (Normal.localScale.x < NoteCircle.localScale.x && Bad.localScale.x >= NoteCircle.localScale.x)
        {
            Verdict = "Bad";
            NoteEvent?.Invoke(Verdict);

            int GradePoint = 0;
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, out GradePoint);

            GradePoint = GradePoint + 5;

            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, GradePoint);

            yield break;
        }

        //Miss판정
        if (Bad.localScale.x < NoteCircle.localScale.x || Center.localScale.x > NoteCircle.localScale.x)
        {
            Verdict = "miss";
            NoteEvent?.Invoke(Verdict);
            int GradePoint = 0;
          
            yield return null;
        }

        yield break;

    }
    
}

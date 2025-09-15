using UnityEngine;
using System.Collections;

public class RhythmSystem : MonoBehaviour
{
    [SerializeField] RhythmView rhythmView;
    [SerializeField] RhythmInput rhythmInput;

    [SerializeField] string NoteData;

    MetronomeSystem metronome;

   


    public bool IsEndGame { get; private set; }



    public void StartEvent()
    {
       
        if (metronome == null)
        {
            metronome = GameManager.instance.Metronome;
        }
       

        rhythmView.NoteData = NoteData;
        rhythmInput.NoteData = NoteData.Substring(1);


        rhythmView.mt = metronome;
        rhythmInput.mt = metronome;

        ResetEvent();

        StartCoroutine(gameStart());
    }

    IEnumerator gameStart()
    {
        //애니 연출 재생
        GameManager.instance.ControlleCam.Play("EnemyTurnCamAnime");



        //애니메이션 재생이후 활성화
        yield return new WaitForSeconds(.2f);     
        rhythmView.gameObject.SetActive(true);
        yield return new WaitForSeconds(.2f);


        //메트로놈 박자에 넣기
        metronome.AddOnceMetronomX4Event(rhythmView.StarNote);

        yield return new WaitUntil(() => rhythmView.IsEnd == true);
        yield return new WaitForSeconds(.1f);


        rhythmView.gameObject.SetActive(false);
       
        GameManager.instance.ControlleCam.Play("PlayerZoomCamAnime");

        //애니메이션 재생이후 활성화
        yield return new WaitForSeconds(.2f);
        rhythmInput.gameObject.SetActive(true);
        yield return new WaitForSeconds(.2f);


        //메트로놈 박자에 넣기
        metronome.AddOnceMetronomX4Event(rhythmInput.StarNote);


        yield return new WaitUntil(() => rhythmInput.IsEnd == true);
        yield return new WaitForSeconds(.1f);

        
        rhythmView.gameObject.SetActive(false);
        rhythmInput.gameObject.SetActive(false);
        GameManager.instance.ControlleCam.Play("EnemyTurnCamReturn");
       
        IsEndGame = true;
    }

   



    public void ResetEvent()
    {
        rhythmView.Reset();
        rhythmInput.Reset();
        IsEndGame = false;
    }
}

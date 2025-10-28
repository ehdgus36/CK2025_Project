using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RhythmSystem : MonoBehaviour
{
    [SerializeField] RhythmView  rhythmView;
    [SerializeField] RhythmInput rhythmInput;

    [SerializeField] string NoteData;
    [SerializeField] string NoteGroupID = "RG101";

    [SerializeField] Sprite RImage;
    [SerializeField] Sprite LImage;

    [SerializeField] Sprite ReverseRImage;
    [SerializeField] Sprite ReverseLImage;

    Sprite StartRImage;
    Sprite StartLImage;

    MetronomeSystem metronome;

    public RhythmView GetRhythmView { get => rhythmView; }
    public RhythmInput GetRhythmInput { get => rhythmInput; }

    public Sprite GetRImage { get { return RImage; } }
    public Sprite GetLImage { get { return LImage; } }
    private void Awake()
    {
        (StartRImage, StartLImage) = (RImage, LImage);
    }

    public bool IsEndGame { get; private set; }

    public void StartEvent()
    {
       
        if (metronome == null)
        {
            metronome = GameManager.instance.Metronome;
        }

        NoteData = GetNotData(NoteGroupID);

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
        yield return new WaitForSeconds(1.55f);     
        rhythmView.gameObject.SetActive(true);
        rhythmView.GetComponent<RectTransform>().anchoredPosition = new Vector3(-219, 294, 0);
        rhythmView.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 2);
        yield return new WaitForSeconds(.2f);


        //메트로놈 박자에 넣기
        metronome.AddOnceMetronomX4Event(rhythmView.StarNote);

        yield return new WaitUntil(() => rhythmView.IsEnd == true);
        yield return new WaitForSeconds(.1f);


        //rhythmView.gameObject.SetActive(false);

        GameManager.instance.ControlleCam.Play("EnemyTurnCamReturn");
        rhythmView.GetComponent<RectTransform>().anchoredPosition = new Vector3(260, 230, 0);
        rhythmView.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        //애니메이션 재생이후 활성화
        yield return new WaitForSeconds(.8f);
        rhythmView.gameObject.SetActive(true);
       

        rhythmInput.gameObject.SetActive(true);
        yield return new WaitForSeconds(.2f);


        //메트로놈 박자에 넣기
        metronome.AddOnceMetronomX4Event(rhythmInput.StarNote);


        yield return new WaitUntil(() => rhythmInput.IsEnd == true);
        yield return new WaitForSeconds(.4f);

        //이펙트 끄기
        GameManager.instance.Player.PlayerEffectSystem.StopEffect("SoftEcho_Buff_Effect");
        GameManager.instance.Player.PlayerEffectSystem.StopEffect("BuildUpBuff_Effect"); 
        rhythmView.gameObject.SetActive(false);
        rhythmInput.gameObject.SetActive(false);

        yield return new WaitForSeconds(.2f);

        rhythmInput.SuccessNoteEvent = null;
        IsEndGame = true;
       
    }

    string GetNotData(string noteGroupID)
    {
        object loadData; 

        GameDataSystem.StaticGameDataSchema.NOTE_DATA_BASE.SearchData(noteGroupID, out loadData);

        List<string> NoteIDs = new List<string>();
        NoteIDs.Add(((NoteGroupData)loadData).Note_ID1);
        NoteIDs.Add(((NoteGroupData)loadData).Note_ID2);
        NoteIDs.Add(((NoteGroupData)loadData).Note_ID3);


        GameDataSystem.StaticGameDataSchema.NOTE_DATA_BASE.SearchData(NoteIDs[Random.Range(0, NoteIDs.Count)], out loadData);


        return "0"+((NoteData)loadData).NoteCode.Substring(1); ;
    }

    public void ResetEvent()
    {
        rhythmView.Reset();
        rhythmInput.Reset();
        IsEndGame = false;
    }

    public void ReverseNote(bool reverse)
    {
        if(reverse == true)
            (RImage, LImage) = (ReverseLImage, ReverseRImage);
        if(reverse == false)
            (RImage, LImage) = (StartRImage, StartLImage);
    }
   
}

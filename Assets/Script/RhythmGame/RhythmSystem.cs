using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RhythmSystem : MonoBehaviour
{
    [SerializeField] RhythmView rhythmView;
    [SerializeField] RhythmInput rhythmInput;

    [SerializeField] string NoteData;
    [SerializeField] string NoteGroupID = "RG101";

    MetronomeSystem metronome;

   


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

        
        rhythmView.gameObject.SetActive(false);
        rhythmInput.gameObject.SetActive(false);



        //플레이어 베리어guitarwall_Ani

        if (rhythmInput.score != 0)
        {

            //GameManager.instance.Player.PlayerAnimator.PlayAnimation("guitarwall_Ani", false, (Entry, e) =>
            //{
            //    Debug.Log("점수 :" + rhythmInput.score.ToString() + "방패 가동");
            //    GameManager.instance.Player.PlayerEffectSystem.PlayEffect("guitarwall_Effect",
            //                                                             GameManager.instance.Player.transform.position);
            //    UnitData playerData;
            //    GameDataSystem.DynamicGameDataSchema.LoadDynamicData<UnitData>(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA,
            //                                                                   out playerData);
            //    playerData.CurrentBarrier += rhythmInput.score;

            //    GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, playerData);
            //});

        }

        yield return new WaitForSeconds(2f);
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


        return ((NoteData)loadData).NoteCode;
    }




    public void ResetEvent()
    {
        rhythmView.Reset();
        rhythmInput.Reset();
        IsEndGame = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RhythmGameSystem : MonoBehaviour
{

    [SerializeField] List<RhythmGameTrack> RhythmGameTracks;


    public bool isEndGame = false;

    public void StartGame()
    { 
        isEndGame = false;
        for (int i = 0; i < RhythmGameTracks.Count; i++)
        {
            RhythmGameTracks[i].Setup();
        }

        //데이터 베이스에서 데이터 가져오기 
        StartCoroutine(StartRhythmGame());

    }

    IEnumerator StartRhythmGame()
    { 
        yield return new WaitForSeconds(1.0f);
        GameManager.instance.Metronome.AddRecurringMetronomEvent(RhythmGameTracksNoteSpawn);

        for (int i = 0; i < RhythmGameTracks.Count; i++)
        {
            yield return new WaitUntil(() => RhythmGameTracks[i].isEndTrack == true);
        }

        GameManager.instance.Metronome.RecurringMetronomEventClear();
        GameManager.instance.Metronome.OnceMetronomEventClear();

        yield return new WaitForSeconds(0.5f);

        isEndGame = true;

        // 리듬게임 완료후 꺼주기
        for (int i = 0; i < RhythmGameTracks.Count; i++)
        {
            RhythmGameTracks[i].MainRhythmRhythemObj.SetActive(false);
        }
    }


    void RhythmGameTracksNoteSpawn()
    {
        for (int i = 0; i < RhythmGameTracks.Count; i++)
        {
            RhythmGameTracks[i].SpawnNote();
        }
    }


    public void RhythmGameTracksRemove(int index )
    {
        RhythmGameTracks[index].Setup();
        RhythmGameTracks.RemoveAt(index);
    }


}

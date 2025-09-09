using FMODUnity;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewRhythmSystem : MonoBehaviour
{
    [SerializeField] RhythmView rhythmView;
    [SerializeField] RhythmInput rhythmInput;

    [SerializeField] MetronomeSystem metronome;
    [SerializeField] TMP_InputField INputString;

    private void Start()
    {
        StartEvent();
    }
    IEnumerator gameStart()
    {
        yield return new WaitForSeconds(2);
        metronome.AddRecurringMetronomEvent(rhythmView.SpawnNote);

        yield return new WaitForSeconds(5);
        metronome.RemoveRecurringMetronomEvent(rhythmView.SpawnNote);
        metronome.AddRecurringMetronomEvent(rhythmInput.SpawnNote);
    }

    public void StartEvent()
    {
        rhythmView.NoteData = INputString.text;
        rhythmInput.NoteData = INputString.text.Substring(1);
        metronome.OnceMetronomEventClear();
        metronome.OnceMetronomEventClear();

        StartCoroutine(gameStart());
    }

    public void ResetEvent()
    {
        SceneManager.LoadScene("RhythmGamTest");
    }

}

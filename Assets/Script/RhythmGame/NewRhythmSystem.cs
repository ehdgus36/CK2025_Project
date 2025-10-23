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

    [SerializeField] bool IsReset = false;

    private void Start()
    {    
        StartEvent();
    }
    IEnumerator gameStart()
    {
        yield return new WaitForSeconds(1.1f);
        metronome.AddOnceMetronomX4Event(rhythmView.StarNote);

        yield return new WaitForSeconds(4.8f);
        metronome.AddOnceMetronomX4Event(rhythmInput.StarNote);
    }

    public void StartEvent()
    {
        rhythmView.NoteData = INputString.text;
        rhythmInput.NoteData = INputString.text.Substring(1);
        

        rhythmView.mt = metronome;
        rhythmInput.mt = metronome;

        StartCoroutine(gameStart());
    }


    public void ResetEvent()
    {
        rhythmView.Reset();
        rhythmInput.Reset();

        if (IsReset == true)
        {
            SceneManager.LoadScene("RhythmGamTest");
        }
    }

}

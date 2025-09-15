using FMODUnity;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class RhythmView : MonoBehaviour
{
    [SerializeField] public string NoteData;
    [SerializeField] Image[] ViewNote;
    [SerializeField] Sprite RImage;
    [SerializeField] Sprite LImage;

    [SerializeField] GameObject startObject;

    int noteIndex = 0;
    int currentBeat = -1;

    public MetronomeSystem mt;
    public bool IsEnd;


    public void StarNote()
    {
        mt.AddRecurringMetronomEvent(SpawnNote);
        IsEnd = false;
    }

    public void Reset()
    {
        for (int i = 0; i < ViewNote.Length; i++)
        {
            ViewNote[i].gameObject.SetActive(false);

        }

        noteIndex = 0;
        currentBeat = -1;

        IsEnd = false;
        mt.RemoveRecurringMetronomEvent(SpawnNote);
    }
    public void SpawnNote()
    {
        if (currentBeat == NoteData.Length)
        {

            IsEnd = true;
            return;
        }


        if (currentBeat == -1)
        {
            startObject.SetActive(true);
            currentBeat++;
            RuntimeManager.PlayOneShot("event:/UI/Turn_End");
            return;
        }

       


        if (NoteData[currentBeat] == '1') // ¿ÞÂÊ
        {
            ViewNote[noteIndex].gameObject.SetActive(true);
            ViewNote[noteIndex].sprite = LImage;
            RuntimeManager.PlayOneShot("event:/Character/Player_CH/Player_Attack");
            noteIndex++;
        }


        if (NoteData[currentBeat] == '2')// ¿À¸¥ÂÊ
        {
            ViewNote[noteIndex].gameObject.SetActive(true);
            ViewNote[noteIndex].sprite = RImage;
            RuntimeManager.PlayOneShot("event:/Effect/Defense/Defense_Success");
            noteIndex++;
        }
        currentBeat++;
    }
}

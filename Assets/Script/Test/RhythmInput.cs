using FMODUnity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class RhythmInput : MonoBehaviour
{
    [SerializeField] public string NoteData;
    [SerializeField] List<Inputnote> InputNote;
    [SerializeField] List<Inputnote> inputInstanceNote = new List<Inputnote>();
    [SerializeField] Sprite RImage;
    [SerializeField] Sprite LImage;

    [SerializeField] GameObject startObject;
    [SerializeField]

    public MetronomeSystem mt;

    int noteIndex = 0;
    int currentBeat = -1;

    public int score=0;

    public bool IsEnd;

    public void StarNote()
    {
        mt.AddRecurringMetronomEvent(SpawnNote);
        IsEnd = false;
    }

    public void Reset()
    {
        for (int i = 0; i < InputNote.Count; i++)
        {
            InputNote[noteIndex].gameObject.SetActive(false);
           
        }

        noteIndex = 0;
        currentBeat = -1;
        inputInstanceNote.Clear();
        mt.RemoveRecurringMetronomEvent(SpawnNote);
        IsEnd = false;
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



        if (NoteData[currentBeat] == '1')
        {
            InputNote[noteIndex].gameObject.SetActive(true);
            InputNote[noteIndex].StartNote(NoteData[currentBeat]);

            inputInstanceNote.Add(InputNote[noteIndex]);
            noteIndex++;
        }


        if (NoteData[currentBeat] == '2')
        {
            InputNote[noteIndex].gameObject.SetActive(true);
            InputNote[noteIndex].StartNote(NoteData[currentBeat]);

            inputInstanceNote.Add(InputNote[noteIndex]);
            noteIndex++;
        }
       

        currentBeat++;
    }



    private void Update()
    {

        if (inputInstanceNote.Count == 0) return;
        else
        {

            if (inputInstanceNote[0].miss)
            {
                inputInstanceNote.Remove(inputInstanceNote[0]);

                if (inputInstanceNote.Count == 0) return;
            }


            if (Input.GetMouseButtonDown(inputInstanceNote[0].mouseInput) && inputInstanceNote.Count != 0)
            {
                if (inputInstanceNote[0].good)
                {
                    if (inputInstanceNote[0].mouseInput == 0) RuntimeManager.PlayOneShot("event:/Character/Player_CH/Player_Attack");

                    if (inputInstanceNote[0].mouseInput == 1) RuntimeManager.PlayOneShot("event:/Effect/Defense/Defense_Success");

                    inputInstanceNote[0].GetComponent<UnityEngine.UI.Image>().color = Color.white;

                    inputInstanceNote.Remove(inputInstanceNote[0]);
                    score++;


                }
            }
        }
       
    }
}

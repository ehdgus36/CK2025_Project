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
    [SerializeField] public string NoteData { get; set; }
    [SerializeField] Image[] ViewNote;
    
   

    [SerializeField] GameObject startObject;

    int noteIndex = 0;
    int currentBeat = -1;

    public MetronomeSystem mt;
    public bool IsEnd;

    public bool IsReverse = false;

    private void OnEnable()
    {
        Reset();
    }

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
        startObject.SetActive(false);

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
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Rythm_Game/Monster_Go");
            return;
        }

        if (NoteData[currentBeat] == '1') // ¿ÞÂÊ
        {
            ViewNote[noteIndex].gameObject.SetActive(true);
            ViewNote[noteIndex].sprite = GameManager.instance.EnemysGroup.GetRhythmSystem.GetLImage; 

            for (int i = 0; i < GameManager.instance.EnemysGroup.Enemys.Count; i++)
            {
                GameManager.instance.EnemysGroup.Enemys[i].UnitAnimationSystem.PlayAnimation("Rhytem_Ani", false, null, null, false, 1.5f);

            }
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Rythm_Game/Monster_Left");
            noteIndex++;
        }

        if (NoteData[currentBeat] == '2')// ¿À¸¥ÂÊ
        {
            ViewNote[noteIndex].gameObject.SetActive(true);
            ViewNote[noteIndex].sprite = GameManager.instance.EnemysGroup.GetRhythmSystem.GetRImage;
            for (int i = 0; i < GameManager.instance.EnemysGroup.Enemys.Count; i++)
            {
                GameManager.instance.EnemysGroup.Enemys[i].UnitAnimationSystem.PlayAnimation("Rhytem2_Ani", false, null, null, false, 1.5f);
            }
            GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Rythm_Game/Monster_Right");
            noteIndex++;
        }
        currentBeat++;
    }


  
}

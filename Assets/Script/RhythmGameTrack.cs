using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RhythmGameTrack : MonoBehaviour
{

   

     int currentBeat = 0;
     int DelectNoteCount  =0;
    

    [SerializeField] string NoteData;
    public bool isEndTrack { get; private set; }

    [SerializeField] List<GameObject> Notes;
    List<GameObject> SpawnNotes = new List<GameObject>();

    [SerializeField] Transform StartNotePos;
    [SerializeField] KeyCode NoteKey;
    [SerializeField] SpriteRenderer SelectZone;
    [SerializeField] GameObject _MainRhythemObj;
    [SerializeField] Enemy TargetEnemy;

    [SerializeField] TextMeshProUGUI _EnemyDamageText;

    public GameObject MainRhythmRhythemObj { get { return _MainRhythemObj; } }
    public GameObject EnemyDamageText { get { return _EnemyDamageText.gameObject; } }

    public void Setup()
    {
        SpawnNotes.Clear();
        _MainRhythemObj.SetActive(true);
        _EnemyDamageText.gameObject.SetActive(true);
        _EnemyDamageText.text = TargetEnemy.EnemyData.CurrentDamage.ToString();
       currentBeat = 0;
      
        isEndTrack = false;
        DelectNoteCount = 0;

        // 노트 위치 초기화
        for (int i = 0; i < Notes.Count; i++)
        {
            Notes[i].transform.position = StartNotePos.position;
            Notes[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (SpawnNotes.Count == 0) return;

        if (Input.GetKeyDown(NoteKey)) // 키 입력시
        {
            if (SpawnNotes[0].activeSelf == true)
            {
                if (SpawnNotes[0].transform.position.y < SelectZone.bounds.max.y
                    && SpawnNotes[0].transform.position.y > SelectZone.bounds.min.y)
                {
                    TargetEnemy.CurrentDamageDown(1); //Enemy 의 데미지를 감소 시킴

                    GameManager.instance.ComboUpdate(Random.Range(9024, 10025));
                    _EnemyDamageText.text = TargetEnemy.EnemyData.CurrentDamage.ToString();
                    DelectNote();      
                }
                else  // 키입력시 범위 내에 없을때
                {
                    DelectNote();
                }
            }

        }
        else // 키입력없음
        {
            if (SpawnNotes[0].transform.position.y < SelectZone.bounds.min.y) // 키입력 없고 판정공간 넘어 갔을때
            {
                DelectNote();

            }
        }
    }

    //노트가 처리 될때 공통적으로 해야하는 거 묶음
    void DelectNote() 
    {
        SpawnNotes[0].SetActive(false);
        SpawnNotes[0].transform.position = StartNotePos.position;

        SpawnNotes.RemoveAt(0);
        DelectNoteCount++;
    }


    public void SpawnNote()
    {
        if (currentBeat == NoteData.Length) // 생성 갯수만족하면 초과 생성막기
        {
            if (DelectNoteCount == Notes.Count) // 생성된 노트가 모두 처리 되었으면 게임 종료
            {
                isEndTrack = true;
               
                Debug.Log("끝 : " + isEndTrack);
            }

            return;
        }
        
        if (NoteData[currentBeat] == '1')
        {
            Notes[0].SetActive(true);
            SpawnNotes.Add(Notes[0]);

            GameObject temp = Notes[0];
            Notes.RemoveAt(0);
            Notes.Add(temp);

        }


        currentBeat++;
    }
}


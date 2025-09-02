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

    [SerializeField] EffectSystem EffectSystem;
    [SerializeField] UnitAnimationSystem UnitAnime;

    [SerializeField] Transform StartNotePos;
    [SerializeField] KeyCode NoteKey;
    [SerializeField] KeyCode NoteKey2;

    [SerializeField] SpriteRenderer SelectZone;
    [SerializeField] GameObject _MainRhythemObj;
    [SerializeField] Enemy TargetEnemy;

    [SerializeField] TextMeshProUGUI _EnemyDamageText;

    public GameObject MainRhythmRhythemObj { get { return _MainRhythemObj; } }
    public GameObject EnemyDamageText { get { return _EnemyDamageText.gameObject; } }

    public void Setup()
    {
        _EnemyDamageText = TargetEnemy.GetEnemyStatus.RhythmDamageText;
        SpawnNotes.Clear();
        _MainRhythemObj.SetActive(true);
        _EnemyDamageText.gameObject.SetActive(true);
        _EnemyDamageText.text = TargetEnemy.EnemyData.CurrentDamage.ToString();
        currentBeat = 0;
      
        isEndTrack = false;
        DelectNoteCount = 0;

        //불필요한 UI 끄기
        TargetEnemy.GetEnemyStatus.NextAttackUI.gameObject.SetActive(false);

        // 노트 위치 초기화
        for (int i = 0; i < Notes.Count; i++)
        {
            Notes[i].transform.position = StartNotePos.position;
            Notes[i].gameObject.SetActive(false);
        }

        if (NoteKey == KeyCode.DownArrow)
            NoteKey2 = KeyCode.S;

        if (NoteKey == KeyCode.UpArrow)
            NoteKey2 = KeyCode.W;

        if (NoteKey == KeyCode.LeftArrow)
            NoteKey2 = KeyCode.A;

        if (NoteKey == KeyCode.RightArrow)
            NoteKey2 = KeyCode.D;

    }

    private void Update()
    {
        if (SpawnNotes.Count == 0) return;

        if (Input.GetKeyDown(NoteKey) || Input.GetKeyDown(NoteKey2)) // 키 입력시
        {
            if (SpawnNotes[0].activeSelf == true)
            {
                if (SpawnNotes[0].transform.position.y < SelectZone.bounds.max.y
                    && SpawnNotes[0].transform.position.y > SelectZone.bounds.min.y)
                {
                    TargetEnemy.CurrentDamageDown(1); //Enemy 의 데미지를 감소 시킴

                    //플레이어 위치 애니매이션 실행
                   // GameManager.instance.Player.transform.position = TargetEnemy.transform.position - new Vector3(2, 0, 0);
                   // GameManager.instance.Player.PlayerAnimator.PlayAnimation("gard"+((int)Random.Range(1,3)).ToString());

                    //애니메이션 이펙트 실행
                    GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Effect/Defense/Defense_Success");
                    EffectSystem.PlayEffect("Rhythm_Effect", SelectZone.transform.position);
                    EffectSystem.PlayEffect("Rhythm_Square", TargetEnemy.transform.position);
                    EffectSystem.PlayEffect("Perfect_Effect", TargetEnemy.transform.position);
                   
                    UnitAnime.PlayAnimation("hit");
                    GameManager.instance.PostProcessingSystem.ChangeVolume("Rhythem_Game");

                    GameManager.instance.Shake.PlayShake();

                    //점수업데이트
                    GameManager.instance.ComboUpdate(Random.Range(9024, 10025));
                    _EnemyDamageText.text = TargetEnemy.EnemyData.CurrentDamage.ToString();

                    //노트 삭제
                    DelectNote();      
                }
                else  // 키입력시 범위 내에 없을때
                {
                    EffectSystem.PlayEffect("Miss_Effect", TargetEnemy.transform.position);
                    GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Effect/Defense/Defense_Fail");
                    DelectNote();
                }
            }

        }
        else // 키입력없음
        {
            if (SpawnNotes[0].transform.position.y < SelectZone.bounds.min.y) // 키입력 없고 판정공간 넘어 갔을때
            {
                GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Effect/Defense/Defense_Fail");
                EffectSystem.PlayEffect("Miss_Effect", TargetEnemy.transform.position);
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
        Debug.Log("처리한 노트 : "+DelectNoteCount.ToString() + "생성한노트 : "+NoteData.Length.ToString());
        if (currentBeat == NoteData.Length) // 생성 갯수만족하면 초과 생성막기
        {
            if (DelectNoteCount == Notes.Count) // 생성된 노트가 모두 처리 되었으면 게임 종료
            {
                isEndTrack = true;
               
                Debug.Log("끝 : " );
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


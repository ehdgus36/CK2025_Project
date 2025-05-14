using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

public class EnemyNoteSystemControl : MonoBehaviour // 작동하는거 확인하고 로직 수정
{
    [SerializeField] public List<NoteSystem> NoteSystems; // 일단 퍼블릭 Enemy에서 이벤트 등록하도록 
    [SerializeField] float Play_Interval;
    [SerializeField] int currentindex = 0;
    bool isKeyOn = false;
    public bool[] Success { get; private set; }


    public void Initialize()
    {
        for (int i = 0; i < NoteSystems.Count; i++)
        {     
            NoteSystems[i].Initialize();
        }
        Success = new bool[NoteSystems.Count];

        for (int i = 0; i < Success.Length; i++)
        {
            Success[i] = false;
        }
    }

    public void Update()
    {
        if (isKeyOn == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NoteSystems[currentindex].isTrigger = true;
                Success[currentindex] = true;
                currentindex++;
                return;
            }

            if (currentindex == NoteSystems.Count)
            {
                isKeyOn = false;
               // Success = true;
              
            }
        }
        
    }

    public void PlayManager() // EnemyGroup에서 호출할꺼임 게임 시작하면
    {
        StartCoroutine(StartSystem()); // 이렇게 시작
    }

    IEnumerator StartSystem()
    {
        currentindex = 0;
        isKeyOn = false;

        for (int i = 0; i < Success.Length; i++)
        {
            Success[i] = false;
        }


        yield return new WaitForSeconds(0.5f);
        isKeyOn = true;

        for (int i = 0; i < NoteSystems.Count; i++)
        {
           
            GameManager.instance.Metronome.AddOnceMetronomEvent(NoteSystems[i].PlayNote);
            yield return new WaitForSeconds(Play_Interval);
        }
    }

}

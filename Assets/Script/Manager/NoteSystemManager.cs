using UnityEngine;
using System.Collections;

public class NoteSystemManager : MonoBehaviour // 작동하는거 확인하고 로직 수정
{
    [SerializeField] public NoteSystem[] NoteSystems; // 일단 퍼블릭 Enemy에서 이벤트 등록하도록 
    [SerializeField] int currentindex = 0;


    public void Start()
    {
        PlayManager();
    }


    public void Initialize()
    { 
     //대충 초기화
    }

    public void PlayManager() // EnemyGroup에서 호출할꺼임 게임 시작하면
    {
        StartCoroutine(StartSystem()); // 이렇게 시작
    }

    IEnumerator StartSystem()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i<  NoteSystems.Length; i++)
        {
            NoteSystems[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }


        while (true) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NoteSystems[currentindex].isTrigger = true;
                currentindex++;
            }
            if (currentindex == NoteSystems.Length)
            {
                break;
            }

            yield return new WaitForSeconds(0.016f);
        }
        yield return null;
    }

}

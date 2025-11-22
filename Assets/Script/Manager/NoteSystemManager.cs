using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class NoteSystemManager : MonoBehaviour // 작동하는거 확인하고 로직 수정
{
    [SerializeField] public NoteSystem[] NoteSystems; // 일단 퍼블릭 Enemy에서 이벤트 등록하도록 
    [SerializeField] float Play_Interval;
    [SerializeField] int currentindex = 0;
    bool isKeyOn = false;
    bool Success = false;


    public void Initialize()
    {
        for (int i = 0; i < NoteSystems.Length; i++)
        {     
            NoteSystems[i].Initialize();
        }
    }

    public void Update()
    {
        if (isKeyOn == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NoteSystems[currentindex].isTrigger = true;
                currentindex++;
            }

            if (currentindex == NoteSystems.Length)
            {
                isKeyOn = false;
                Success = true;
                for (int i = 0; i < NoteSystems.Length; i++)
                {
                    NoteSystems[i].gameObject.SetActive(false);                
                }
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
        Success = false;


        yield return new WaitForSeconds(0.5f);
        isKeyOn = true;
        for (int i = 0; i < NoteSystems.Length; i++)
        {
            NoteSystems[i].gameObject.SetActive(true);
            NoteSystems[i].PlayNote();
            yield return new WaitForSeconds(Play_Interval);
        }
        

        yield return new WaitUntil(() => Success == true);

       
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutoriaManager : MonoBehaviour
{
    public TutoriaManager instance { get; private set; }
    public int TutorialStep = 1;
    [SerializeField] GameObject DimmingOverlayObject;
    [SerializeField] List<string> Dialog;
    bool isKey;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        DimmingOverlayObject.SetActive(false);
        StartCoroutine(TutorialStart());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isKey == true)
        { 
        
        
        }


    }

    IEnumerator TutorialStart()
    {

        // 노말 카드선택
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                break;
            }
            yield return null;
            yield return null;
        }
        //튜토리얼 1번째 실행
        yield return new WaitUntil(() => TutorialStep == 2); // TutorialStep이 2가 될때 까지 대기

        //속성카드 선택
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                break;
            }
            yield return null;
            yield return null;
        }
        //튜토리얼 2번째 실행
        yield return new WaitUntil(() => TutorialStep == 3); //조건이 만족할때 까지 이하 반복


        //타겟 카드 선택
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                break;
            }
            yield return null;
            yield return null;
        }
        //튜토리얼 2번째 실행
        yield return new WaitUntil(() => TutorialStep == 4); //조건이 만족할때 까지 이하 반복

        //공격

        yield return new WaitUntil(() => TutorialStep == 5); //조건이 만족할때 까지 이하 반복
    }
}

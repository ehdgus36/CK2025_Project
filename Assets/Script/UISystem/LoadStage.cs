
using FMODUnity;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum StageState
{
    LOCK, ClEAR, NULOCK
}


public class LoadStage : MonoBehaviour,IPointerEnterHandler, IPointerDownHandler
{

    [SerializeField] string LoadSceneName;
    [SerializeField] GameObject pick;
    [SerializeField] MapSystem mapSystem;
    [SerializeField] GameObject ClearMark;

    [SerializeField] StagePass Pass;
    [SerializeField] LoadStage NextStage;

    [SerializeField] public StageState state = StageState.LOCK;

    bool IsSelect = false;

    Coroutine active;

    Vector3 start;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.GetComponent<Button>().interactable == true)
        {
            RuntimeManager.PlayOneShot("event:/UI/WorldMap/WorldMap_Click");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.GetComponent<Button>().interactable == true)
        {
            RuntimeManager.PlayOneShot("event:/UI/WorldMap/WorldMap_Over");
        }
    }


    public void SetUP()
    {
        if (state == StageState.ClEAR)
        {
            if (ClearMark != null)
                ClearMark?.SetActive(true);
            this.GetComponent<Button>().interactable = false;
            if (Pass != null)
            {
                Pass?.AddPassButtonEvent();
            }
        }

        this.GetComponent<Button>().onClick.AddListener(IntoStage);

        if (state == StageState.NULOCK)
        {
            this.GetComponent<Button>().interactable = true;
            active = StartCoroutine(Active());
        }

        if (state == StageState.LOCK) this.GetComponent<Button>().interactable = false;


    }


    public void IntoStage()
    {
        if (IsSelect == true) return;

        StopCoroutine(active);
        this.transform.localScale = start;

        pick.transform.position = this.transform.position;
        pick.SetActive(false);
        pick.SetActive(true);
        IsSelect = true;
        StartCoroutine(DelayLoadScene());
       
    }

    IEnumerator DelayLoadScene()
    {
        yield return new WaitForSeconds(.5f);
        state = StageState.ClEAR;

        if (Pass != null)
        {
            Pass.UnLockStage();
        }

        if (NextStage != null)
        {
            NextStage.state = StageState.NULOCK;
            NextStage.GetComponent<Button>().interactable = true;

            this.GetComponent<Button>().interactable = false;
        }

        mapSystem.Save();

        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STAGE_DATA, LoadSceneName);
        FindFirstObjectByType<LoadingScreen>().LoadScene(LoadSceneName);
    }


    IEnumerator Active()
    {
        start = this.transform.localScale;

        while (true)
        {
            float T = 0;
            for (int i = 0; i < 11; i++)
            {
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(2f, 2f, 2f) ,T);
                T += 0.1f;
                yield return new WaitForSeconds(.02f);
            }

            T = 0;
            for (int i = 0; i < 11; i++)
            {
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, start, T);
                T += 0.1f;
                yield return new WaitForSeconds(.02f);
            }
        }
    }
}

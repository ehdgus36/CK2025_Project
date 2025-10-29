
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum StageState
{
    LOCK, ClEAR, NULOCK
}


public class LoadStage : MonoBehaviour
{

    [SerializeField] string LoadSceneName;
    [SerializeField] GameObject pick;
    [SerializeField] MapSystem mapSystem;
    [SerializeField] GameObject ClearMark;

    [SerializeField] StagePass Pass;
    [SerializeField] LoadStage NextStage;

    [SerializeField] public StageState state = StageState.LOCK;




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

        if (state == StageState.NULOCK) this.GetComponent<Button>().interactable = true;

        if (state == StageState.LOCK) this.GetComponent<Button>().interactable = false;


    }


    public void IntoStage()
    {
        pick.transform.position = this.transform.position;


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
}

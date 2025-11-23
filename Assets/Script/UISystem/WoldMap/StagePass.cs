using UnityEngine;
using UnityEngine.UI;

public class StagePass : MonoBehaviour
{
    [SerializeField] LoadStage Pass1;
    [SerializeField] LoadStage Pass2;
    [SerializeField] LoadStage Pass3;
    [SerializeField] MapSystem mapSystem;

    public void UnLockStage()
    {
        if (Pass1 != null)
        {
            Pass1.GetComponent<Button>().interactable = true;
            Pass1.state = StageState.NULOCK;
        }
        if (Pass2 != null)
        {
            Pass2.GetComponent<Button>().interactable = true;
            Pass2.state = StageState.NULOCK;
        }
        if (Pass3 != null)
        {
            Pass3.GetComponent<Button>().interactable = true;
            Pass3.state = StageState.NULOCK;
        }


    }

    public void AddPassButtonEvent()
    {
        if (Pass1 != null)
            Pass1.GetComponent<Button>().onClick.AddListener(() =>
            {
                Pass2.GetComponent<Button>().interactable = false; Pass2.state = StageState.LOCK;

                if (Pass3 != null)
                {
                    Pass3.GetComponent<Button>().interactable = false; Pass3.state = StageState.LOCK;
                }
                mapSystem.Save();
            });


        if (Pass2 != null)
            Pass2.GetComponent<Button>().onClick.AddListener(() =>
            {
                Pass1.GetComponent<Button>().interactable = false; Pass1.state = StageState.LOCK;

                if (Pass3 != null)
                { 
                    Pass3.GetComponent<Button>().interactable = false; Pass3.state = StageState.LOCK;
                }
                mapSystem.Save();
            });

        if (Pass3 != null)
            Pass3.GetComponent<Button>().onClick.AddListener(() =>
            {
                Pass1.GetComponent<Button>().interactable = false; Pass1.state = StageState.LOCK;
                Pass2.GetComponent<Button>().interactable = false; Pass2.state = StageState.LOCK;
                mapSystem.Save();
            });
    }
}

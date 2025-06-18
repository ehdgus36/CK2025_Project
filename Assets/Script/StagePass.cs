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
        Pass1.GetComponent<Button>().interactable = true;
        Pass2.GetComponent<Button>().interactable = true;
        Pass3.GetComponent<Button>().interactable = true;

        Pass1.state = StageState.NULOCK;
        Pass2.state = StageState.NULOCK;
        Pass3.state = StageState.NULOCK;

        
    }

    public void AddPassButtonEvent()
    {
        Pass1.GetComponent<Button>().onClick.AddListener(() => {
            Pass2.GetComponent<Button>().interactable = false; Pass2.state = StageState.LOCK;
            Pass3.GetComponent<Button>().interactable = false; Pass3.state = StageState.LOCK;
            mapSystem.Save();
        });


        Pass2.GetComponent<Button>().onClick.AddListener(() => {
            Pass1.GetComponent<Button>().interactable = false; Pass1.state = StageState.LOCK;
            Pass3.GetComponent<Button>().interactable = false; Pass3.state = StageState.LOCK;
            mapSystem.Save();
        });
        Pass3.GetComponent<Button>().onClick.AddListener(() => {
            Pass1.GetComponent<Button>().interactable = false; Pass1.state = StageState.LOCK;
            Pass2.GetComponent<Button>().interactable = false; Pass2.state = StageState.LOCK;
            mapSystem.Save();
        });
    }
}

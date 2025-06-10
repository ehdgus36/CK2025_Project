
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStage : MonoBehaviour
{

    [SerializeField] string LoadSceneName;
    [SerializeField] GameObject pick;
    [SerializeField] MapSystem mapSystem;

    public bool isInto = true;
    public void IntoStage()
    {
       // pick.transform.position = this.transform.position;
        FindFirstObjectByType<LoadingScreen>().LoadScene(LoadSceneName);
        //isInto = false;

        //mapSystem.UpdateData();
    }
}

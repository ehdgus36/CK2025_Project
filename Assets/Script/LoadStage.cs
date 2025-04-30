
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStage : MonoBehaviour
{

    [SerializeField] string LoadSceneName;
    [SerializeField] GameObject pick;
    public void IntoStage()
    {
        pick.transform.position = this.transform.position;
        FindFirstObjectByType<LoadingScreen>().LoadScene(LoadSceneName);
    }
}

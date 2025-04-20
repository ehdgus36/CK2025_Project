using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStage : MonoBehaviour
{

    [SerializeField] string LoadSceneName;
    public void IntoStage()
    {
        FindFirstObjectByType<LoadingScreen>().LoadScene(LoadSceneName);
    }
}

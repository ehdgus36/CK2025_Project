using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class videoPlayerEndLoadScene : MonoBehaviour
{
    [SerializeField] string SceneName = "LobbyScene";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<VideoPlayer>().loopPointReached += (vp) => {
            SceneManager.LoadScene(SceneName);
        };
    }

   
}

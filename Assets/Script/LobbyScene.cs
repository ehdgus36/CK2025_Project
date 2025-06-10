using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class LobbyScene : MonoBehaviour
{
    [SerializeField] string SceneName = "Title";
    [SerializeField] Button EXIT_Button;
    [SerializeField] Button SETTING_Button;

    [SerializeField] GameObject SettingView;
    [SerializeField] GameObject VideoPlayObject;

    [SerializeField] VideoPlayer CutScenePlayer;

    private void Awake()
    {
        EXIT_Button.onClick.AddListener(Exit); ;
        SETTING_Button.onClick.AddListener(SETTING);
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingView.activeSelf == false)
        {
            if (Input.GetMouseButtonDown(0) == false)
            {
                if (Input.anyKeyDown)
                {
                    LoadScene();
                   
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene(SceneName);
                }
            }
        }
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void SETTING()
    {
       SettingView.SetActive(true);
    }

    public void LoadScene()
    {
        CutScenePlayer.Play();
        VideoPlayObject.SetActive(true);
        CutScenePlayer.loopPointReached += (vp) => { SceneManager.LoadScene(SceneName); };
    }
   
}

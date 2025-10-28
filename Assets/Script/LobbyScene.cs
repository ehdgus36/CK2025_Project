using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class LobbyScene : MonoBehaviour
{
    [SerializeField] string SceneName = "GameMap";
    [SerializeField] Button EXIT_Button;
    [SerializeField] Button SETTING_Button;

    [SerializeField] GameObject SettingView;
    [SerializeField] GameObject VideoPlayObject;

    [SerializeField] GameObject selectGameAnime;

    [SerializeField] VideoPlayer CutScenePlayer;

    bool startCutScene = false;
    private void Awake()
    {
        EXIT_Button.onClick.AddListener(Exit); ;
        SETTING_Button.onClick.AddListener(SETTING);
        CutScenePlayer.time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingView.activeSelf == false)
        {
            if (Input.GetMouseButtonDown(0) == false)
            {
                if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.LeftAlt) && !Input.GetKeyDown(KeyCode.Tab))
                {
                    LoadScene();
                   
                }

                if (startCutScene == true && Input.GetKeyDown(KeyCode.Escape))
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
        if (VideoPlayObject.activeSelf == true) return;

        GameDataSystem.DynamicGameDataSchema.NewGameDataInit(); // 로비에서 다시시작시 게임 데이터 초기화
        CutScenePlayer.time = 0;
        CutScenePlayer.Play();

        selectGameAnime.SetActive(true);
        startCutScene = true;

        StartCoroutine(DelayScene());
    }

    IEnumerator DelayScene()
    {
        yield return new WaitForSeconds(1.3f);
        VideoPlayObject.SetActive(true);
        CutScenePlayer.loopPointReached += (vp) => { SceneManager.LoadScene(SceneName); };
    }
    
}

using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

enum FMODLabeled
{
    Player_Turn	= 0,	
    Monster_Turn  = 1
}

public class FMODManagerSystem : MonoBehaviour
{
    [SerializeField] string MainBgm;
    [SerializeField] string SubBgm;
    private EventInstance bgmInstance;
    private EventInstance bgmInstance2;
    [SerializeField] bool isStartBgm = false;

    public void Start()
    {
        if (isStartBgm == true) Initialize();
    }

    public void Initialize()
    {
        bgmInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        bgmInstance.release();

        bgmInstance2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        bgmInstance2.release();

        if (MainBgm != "") GameManager.instance.Metronome.AddOnceMetronomX4Event(()=> { PlayBGM(MainBgm); });

        if (SubBgm != "") GameManager.instance.Metronome.AddOnceMetronomX4Event(() => { PlayBGMSub(SubBgm); }); 

        SceneManager.sceneUnloaded += (Scene) => { OnEndSound(); };
    }

    public void FMODChangeNomal()
    {
        bgmInstance.setParameterByName("Change_Game", (float)FMODLabeled.Player_Turn);
    }


    public void FMODChangePlayer()
    {
        bgmInstance.setParameterByName("Change_Game", (float)FMODLabeled.Player_Turn);
    }

    public void FMODChangeMonsterTurn()
    {
        bgmInstance.setParameterByName("Change_Game", (float)FMODLabeled.Monster_Turn);
    }

    public void FMODChangeUpgrade()
    {
        bgmInstance.setParameterByName("Upgrade_Attack", (float)FMODLabeled.Monster_Turn);
    }

    public void FMODChangeNomal2()
    {
        bgmInstance.setParameterByName("Upgrade_Attack", (float)FMODLabeled.Player_Turn);
    }

    public void FMODChangeUpgrade2()
    {
        bgmInstance.setParameterByName("Upgrade_Attack", (float)FMODLabeled.Player_Turn);
    }

    void PlayBGM(string key)
    {
        bgmInstance = RuntimeManager.CreateInstance(key);
        bgmInstance.start();
        FMODChangeNomal();
    }

    void PlayBGMSub(string key)
    {
        if (string.IsNullOrEmpty(key)) return;

        bgmInstance2 = RuntimeManager.CreateInstance(key);
        bgmInstance2.start();
        FMODChangeNomal2();
    }

    public void PlayEffectSound(string key)
    {
        RuntimeManager.PlayOneShot(key);
    }

    private void OnDestroy()
    {
        bgmInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        bgmInstance.release();

        bgmInstance2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        bgmInstance2.release();
    }

    void OnEndSound()
    {
        // BGM 페이드아웃 후 정지 및 해제
        bgmInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        bgmInstance.release();

        bgmInstance2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        bgmInstance2.release();
    }
}

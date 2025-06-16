using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

enum FMODLabeled
{
    Nomal	= 0,	
    Upgrad  = 1
}

public class FMODManagerSystem : MonoBehaviour
{
    [SerializeField] string MainBgm;
    private EventInstance bgmInstance;

    public void Initialize()
    {
        PlayBGM(MainBgm);
    }

    public void FMODChangeNomal()
    {
        bgmInstance.setParameterByName("Upgrade_Attack", (float)FMODLabeled.Nomal);
    }

    public void FMODChangeUpgrade()
    {
        bgmInstance.setParameterByName("Upgrade_Attack", (float)FMODLabeled.Upgrad);
    }

    void PlayBGM(string key)
    {
        bgmInstance = RuntimeManager.CreateInstance(key);
        bgmInstance.start();
        FMODChangeNomal();
    }

    public void PlayEffectSound(string key)
    {
        RuntimeManager.PlayOneShot(key);
    }

    void OnDestroy()
    {
        // BGM 페이드아웃 후 정지 및 해제
        bgmInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        bgmInstance.release();
    }
}

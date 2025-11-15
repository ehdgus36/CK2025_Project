using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingSystem : MonoBehaviour
{
    [SerializeField] Button EXIT_Button;
    [SerializeField] Button Reset_Button;
    [SerializeField] Button GameExit_Button;

    [SerializeField] Button ResetYes_Button;
    [SerializeField] Button ResetNo_Button;


    [SerializeField] Button Title_Button;

    [SerializeField] GameObject ResetPopUP;

    [SerializeField] Slider MasterVolume;
    [SerializeField] Slider BackGroundVolume;
    [SerializeField] Slider EffectVolume;

    Bus Masterbus;
    Bus BGMbus;
    Bus FXbus;
    List<string> SoundData = new List<string>();

    private void Awake()
    {
        EXIT_Button.onClick.AddListener(ExitEvent);
        Reset_Button.onClick.AddListener(ResetEvent);
        ResetYes_Button.onClick.AddListener(ResetYes);
        ResetNo_Button.onClick?.AddListener(ResetNo);

        Title_Button.onClick?.AddListener(() =>
        {
            RuntimeManager.PlayOneShot("event:/UI/Setting/Set_Click");
            SceneManager.LoadScene("LobbyScene");
        });



        GameExit_Button.onClick.AddListener(() =>
        {
            RuntimeManager.PlayOneShot("event:/UI/Setting/Set_Click");
            Application.Quit();
        });


        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.SOUNDVIEW_DATA, out SoundData);

        Debug.Log("사운드 시스템" + string.Join(',', SoundData));

        MasterVolume.value = float.Parse( SoundData[0]);
        BackGroundVolume.value = float.Parse( SoundData[1]);
        EffectVolume.value = float.Parse(SoundData[2]);

        MasterVolume.onValueChanged.AddListener(MasterChangeValueEvent);
        BackGroundVolume.onValueChanged.AddListener(BGMChangeValueEvent);
        EffectVolume.onValueChanged.AddListener(FXChangeValueEvent);


        Masterbus.setVolume(MasterVolume.value);
        BGMbus.setVolume(BackGroundVolume.value);
        FXbus.setVolume(EffectVolume.value);


        float value = 0;
        BGMbus.getVolume(out value);

        Masterbus = RuntimeManager.GetBus("bus:/");
        BGMbus = RuntimeManager.GetBus("bus:/BGM");
        FXbus = RuntimeManager.GetBus("bus:/SFX");
    }

    void ExitEvent()// 나가기
    {
        this.gameObject.SetActive(false);
        RuntimeManager.PlayOneShot("event:/UI/Setting/Set_Back_Click");
    }

    void ResetEvent()
    {
        ResetPopUP.SetActive(true);
        //리셋 기능 만들기
    }

    void ResetYes()
    {
        
        ResetPopUP.SetActive(false); 
        //리셋 기능 만들기
    }

    void ResetNo()
    {
        ResetPopUP.SetActive(false);
    }

    void MasterChangeValueEvent(Single value)
    {
        Masterbus.setVolume((float)value);
        SoundData[0] = MasterVolume.value.ToString();
        
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SOUNDVIEW_DATA, SoundData);
    }
    void BGMChangeValueEvent(Single value)
    {     
        BGMbus.setVolume((float)value);
        SoundData[1] = BackGroundVolume.value.ToString();
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SOUNDVIEW_DATA, SoundData);
    }
    void FXChangeValueEvent(Single value)
    {
        FXbus.setVolume((float)value);
        SoundData[2] = EffectVolume.value.ToString();
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SOUNDVIEW_DATA, SoundData);
    }
}

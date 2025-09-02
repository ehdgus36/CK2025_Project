using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SettingSystem : MonoBehaviour
{
    [SerializeField] Button EXIT_Button;
    [SerializeField] Button Reset_Button;

    [SerializeField] Button ResetYes_Button;
    [SerializeField] Button ResetNo_Button;

    [SerializeField] Button Title_Button;

    [SerializeField] GameObject ResetPopUP;

    [SerializeField] TextMeshProUGUI MasterVolumeText;
    [SerializeField] TextMeshProUGUI BackGroundVolumeText;
    [SerializeField] TextMeshProUGUI EffectVolumeText;


    float MasterVolume = 50f;
    float BackGroundVolume = 50f;
    float EffectVolume = 50f;
    private void Awake()
    {
        EXIT_Button.onClick.AddListener(ExitEvent);
        Reset_Button.onClick.AddListener(ResetEvent);
        ResetYes_Button.onClick.AddListener(ResetYes);
        ResetNo_Button.onClick?.AddListener(ResetNo);

        Title_Button.onClick?.AddListener(() => { SceneManager.LoadScene("LobbyScene"); });
        MasterVolume = 50f;
        BackGroundVolume = 50f;
        EffectVolume = 50f;

        MasterVolumeText.text = MasterVolume.ToString()+"%";
        BackGroundVolumeText.text = BackGroundVolume.ToString() + "%";
        EffectVolumeText.text = EffectVolume.ToString() + "%";
    }

    void ExitEvent()
    {
        this.gameObject.SetActive(false);
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

    public void VolumeUP(string vType)
    {
        if (vType == "Master") { MasterVolume += 10f; Debug.Log("UP"); }
        if (vType == "BackGround") BackGroundVolume += 10f;
        if (vType == "Effect") EffectVolume += 10f;

       

        MasterVolume = Mathf.Clamp(MasterVolume, 0f, 100f);
        BackGroundVolume = Mathf.Clamp(BackGroundVolume, 0f, 100f);
        EffectVolume = Mathf.Clamp(EffectVolume, 0f, 100f);

        MasterVolumeText.text = MasterVolume.ToString() + "%";
        BackGroundVolumeText.text = BackGroundVolume.ToString() + "%";
        EffectVolumeText.text = EffectVolume.ToString() + "%";

    }

    public void VolumeDown(string vType)
    {

        if (vType == "Master") { MasterVolume -= 10f;  }
        if (vType == "BackGround") BackGroundVolume -= 10f;
        if (vType == "Effect") EffectVolume -= 10f;

        MasterVolume = Mathf.Clamp(MasterVolume, 0f, 100f);
        BackGroundVolume = Mathf.Clamp(BackGroundVolume, 0f, 100f);
        EffectVolume = Mathf.Clamp(EffectVolume, 0f, 100f);

        MasterVolumeText.text = MasterVolume.ToString() + "%";
        BackGroundVolumeText.text = BackGroundVolume.ToString() + "%";
        EffectVolumeText.text = EffectVolume.ToString() + "%";
    }
}

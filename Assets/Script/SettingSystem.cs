using UnityEngine;
using UnityEngine.UI;

public class SettingSystem : MonoBehaviour
{
    [SerializeField] Button EXIT_Button;

    private void Awake()
    {
        EXIT_Button.onClick.AddListener(ExitEvent);
    }

    void ExitEvent()
    {
        this.gameObject.SetActive(false);
    }
}

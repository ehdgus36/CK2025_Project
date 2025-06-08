using UnityEngine;

public class PlayerCastCardStatus : MonoBehaviour
{
    [SerializeField] GameObject[] PeakIcon;
    public void UpdateUI(int count)
    {
        if (count > PeakIcon.Length) return;

        for (int i = 0; i < count; i++)
        { 
            PeakIcon[i].SetActive(true);
        }
    }

    public void Reset()
    {
        for (int i = 0; i < PeakIcon.Length; i++)
        {
            PeakIcon[i].SetActive(false);
        }
    }
}

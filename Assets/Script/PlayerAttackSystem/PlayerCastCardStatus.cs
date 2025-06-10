using UnityEngine;
using UnityEngine.UI;

public class PlayerCastCardStatus : MonoBehaviour
{
    [SerializeField] GameObject[] PeakIcon;
    [SerializeField] Sprite[] PeakIconSprite;
    public void UpdateUI(int count, int cardType)
    {
        if (count > PeakIcon.Length) return;

        for (int i = 0; i < count; i++)
        { 
            PeakIcon[i].SetActive(true);
            PeakIcon[i].GetComponent<Image>().sprite = PeakIconSprite[cardType - 1];
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

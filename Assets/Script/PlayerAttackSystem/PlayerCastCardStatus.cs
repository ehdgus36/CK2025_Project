using UnityEngine;
using UnityEngine.UI;

public class PlayerCastCardStatus : MonoBehaviour
{
    [SerializeField] GameObject[] PeakIcon;
    [SerializeField] Sprite[] PeakIconSprite;

    int index = 0;
    public void UpdateUI(int count, int cardType)
    {
        if (count > PeakIcon.Length) return;



        PeakIcon[index].SetActive(true);
        PeakIcon[index].GetComponent<Image>().sprite = PeakIconSprite[cardType - 1];
        index++;

    }

    public void Reset()
    {
        for (int i = 0; i < PeakIcon.Length; i++)
        {
            PeakIcon[i].SetActive(false);
        }

        index = 0;
    }
}

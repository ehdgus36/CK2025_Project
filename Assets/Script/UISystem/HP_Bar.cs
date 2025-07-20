using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HP_Bar : MonoBehaviour
{
    [SerializeField] Slider hp_bar;
    [SerializeField] TextMeshProUGUI Hp_text;
    public void UpdateUI(int maxhp , int currenthp)
    {
        Debug.Log("최대 체력:" + maxhp.ToString());

        hp_bar.value = ((float)currenthp/ (float)maxhp);

        if (hp_bar.value == 0)
        {
            hp_bar.fillRect.gameObject.SetActive(false);
        }
        else
        {
            hp_bar.fillRect.gameObject.SetActive(true);
        }

        Hp_text.text = currenthp.ToString();
    }
}

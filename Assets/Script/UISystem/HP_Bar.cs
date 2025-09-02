using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HP_Bar : MonoBehaviour
{
    [SerializeField] Slider hp_bar;
    [SerializeField] Material healthBarMat;
    [SerializeField] TextMeshProUGUI Hp_text;
    public void UpdateUI(int maxhp , int currenthp)
    {
        Debug.Log("최대 체력:" + maxhp.ToString());

        float healthPercent = ((float)currenthp / (float)maxhp);

        if (hp_bar != null)
        {
            hp_bar.value = healthPercent;

            if (hp_bar.value == 0)
            {
                hp_bar.fillRect.gameObject.SetActive(false);
            }
            else
            {
                hp_bar.fillRect.gameObject.SetActive(true);
            }
        }

        if (healthBarMat != null)
        {
            healthBarMat?.SetFloat("_Health", healthPercent);
        }

        Hp_text.text = currenthp.ToString();
    }
}

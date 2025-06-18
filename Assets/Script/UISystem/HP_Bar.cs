using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HP_Bar : MonoBehaviour
{
    [SerializeField] Image hp_img;
    [SerializeField] TextMeshProUGUI Hp_text;
    public void UpdateUI(int maxhp , int currenthp)
    {
        Debug.Log("최대 체력:" + maxhp.ToString());

        hp_img.fillAmount = ((float)currenthp/ (float)maxhp);
        


        Hp_text.text = currenthp.ToString();
    }
}

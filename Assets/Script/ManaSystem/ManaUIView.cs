using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManaUIView : MonoBehaviour
{
    [SerializeField] Sprite[] ManaIcons;
    [SerializeField] TextMeshProUGUI ManaText;



    
    //ManaSystem에서 받아오는 "CurrentMana/MaxMana 형태의 데이터를 받아와 UI표시"
    public void UpdateUI(string data)
    {
        string[] Datas = data.Split('/');

        int currentMana = int.Parse(Datas[0]);

        if (ManaText != null)
        {
            ManaText.text = currentMana.ToString();
        }


        if (ManaIcons.Length != 0)
        {
            this.GetComponent<Image>().sprite = ManaIcons[currentMana];
        }


       
    }
}

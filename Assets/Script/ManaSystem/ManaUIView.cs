using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManaUIView : MonoBehaviour
{
    [SerializeField] GameObject[] ManaIcons;
    [SerializeField] TextMeshProUGUI ManaText;



    
    //ManaSystem에서 받아오는 "CurrentMana/MaxMana 형태의 데이터를 받아와 UI표시"
    public void UpdateUI(string data)
    {
        string[] Datas = data.Split('/');

        int MaxMana = int.Parse(Datas[1]);

        for (int i = 0; i < ManaIcons.Length; i++)
        {
            ManaIcons[i].SetActive(false);
        }


        for (int i = 0; i < MaxMana; i++)
        {
            ManaIcons[i].SetActive(true);
            ManaIcons[i].transform.GetChild(0).gameObject.SetActive(false);
        }



        int currentMana = int.Parse(Datas[0]);

        if (ManaText != null)
        {
            ManaText.text = currentMana.ToString();
        }


       


        if (ManaIcons.Length != 0)
        {
            for (int i = 0; i < currentMana ; i++)
            {
                ManaIcons[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }


       
    }
}

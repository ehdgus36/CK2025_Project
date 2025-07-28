using UnityEngine;
using TMPro; 

public class ManaUIView : MonoBehaviour
{
    [SerializeField] GameObject[] ManaIcons;

    
    //ManaSystem에서 받아오는 "CurrentMana/MaxMana 형태의 데이터를 받아와 UI표시"
    public void UpdateUI(string data)
    {
        string[] Datas = data.Split('/');

        int currentMana = int.Parse(Datas[1]);

        for (int i = 0; i < ManaIcons.Length; i++)
        {
            ManaIcons[i].SetActive(false);
        }

        for (int i = 0; i < currentMana; i++)
        {
            ManaIcons[i].SetActive(true);
        }
    }
}

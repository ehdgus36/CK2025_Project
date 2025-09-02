using UnityEngine;
using TMPro;

public class ManaUIView : MonoBehaviour
{
    [SerializeField] GameObject[] ManaIcons;
    [SerializeField] TextMeshProUGUI ManaText;
    
    //ManaSystem에서 받아오는 "CurrentMana/MaxMana 형태의 데이터를 받아와 UI표시"
    public void UpdateUI(string data)
    {
        string[] Datas = data.Split('/');

        int currentMana = int.Parse(Datas[0]);

        if (ManaText != null)
        {
            ManaText.text = Datas[0];
        }



       
    }
}

using UnityEngine;
using TMPro;

public class CardDescription : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI CardName;
    [SerializeField] TextMeshProUGUI Desc;
    [SerializeField] TextMeshProUGUI DescSub;
    [SerializeField] TextMeshProUGUI Grade_Point;


    public void UpdateDescription(string name , string desc , string descsub , int point)
    {
        CardName.text = name;
        Desc.text = desc;
        DescSub.text = descsub;
        Grade_Point.text = point.ToString();
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDescription : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CardName;
    [SerializeField] TextMeshProUGUI Desc;
    [SerializeField] TextMeshProUGUI DescSub;
    [SerializeField] TextMeshProUGUI Grade_Point;
    [SerializeField] Image Icon;
    [SerializeField] Image DescImage;


    public void UpdateDescription(string name , string desc , string descsub , int point , Sprite icon)
    {
        CardName.text = name;
        Desc.text = desc;
        DescSub.text = descsub;
        Grade_Point.text = point.ToString();
        Icon.sprite = icon;
    }

    public void UpdateDescription( Sprite img)
    {
        DescImage.sprite = img;
    }
}

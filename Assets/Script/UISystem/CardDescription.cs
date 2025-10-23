using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDescription : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CardName;
    [SerializeField] TextMeshProUGUI Desc;
    [SerializeField] TextMeshProUGUI DescSub;
    [SerializeField] TextMeshProUGUI Grade_Point;
    [SerializeField] Image DescImage;

    [SerializeField] Sprite Attack;
    [SerializeField] Sprite Debuff;
    [SerializeField] Sprite Buff;


   
    public void UpdateDescription(string name, string desc ,Vector3 pos )
    {
        CardName.text = name;
        Desc.text = desc;
        DescImage.transform.position = pos;

    
    }


}

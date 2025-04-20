using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{


  
    [SerializeField] Image Hpfill;

    [SerializeField] TextMeshProUGUI Hptext;



    int MaxHP;
    int CurrentHP;

    




    public void UpdataStatus(int maxHp , int currentHp)
    {
        (MaxHP, CurrentHP) = (maxHp, currentHp);


        Hpfill.fillAmount = (float)CurrentHP / (float)MaxHP;


        Hptext.text = CurrentHP.ToString();
    }

}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{


    [SerializeField] Player Player;
    [SerializeField] Image Hpfill;

    [SerializeField] TextMeshProUGUI Hptext;



    int MaxHP;
    int CurrentHP;

    



    private void Update()
    {
        UpdataHp();
    }

    public void UpdataHp()
    {
        (MaxHP, CurrentHP) = (Player.GetMaxHp(), Player.GetUnitCurrentHp());


        Hpfill.fillAmount = (float)CurrentHP / (float)MaxHP;


        Hptext.text = MaxHP.ToString() + "/" + CurrentHP.ToString();
    }

}

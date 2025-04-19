using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;

public class EnemyStatus : MonoBehaviour
{


    [SerializeField] GameObject SkillPoint;
    [SerializeField] Image Hpfill;

    [SerializeField] TextMeshProUGUI HpText;
    [SerializeField] TextMeshProUGUI DamageText;
    [SerializeField] TextMeshProUGUI indexText;
    [SerializeField] TextMeshProUGUI PassiveDescText;
    [SerializeField] GameObject PassiveDescription;


    int MaxHP;
    int CurrentHP;



    public void Initialize(int maxHp, int damage, int index)
    {
        (MaxHP) = maxHp;
        CurrentHP = MaxHP;



        HpText.text = CurrentHP.ToString();
        DamageText.text = "Damage:" + damage.ToString();
        indexText.text = index.ToString();


        PassiveDescription.SetActive(false);
    }

    public void UpdateStatus(int hp, int damage, int index)
    {
        CurrentHP = hp;
        Hpfill.fillAmount = (float)CurrentHP / (float)MaxHP;


        HpText.text = CurrentHP.ToString();
        DamageText.text = "Damage:" + damage.ToString();
        indexText.text = index.ToString();
    }

    public void OnPassiveDescription()
    {
        PassiveDescription.SetActive(true);
    }

}

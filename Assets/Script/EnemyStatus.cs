using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class EnemyStatus : MonoBehaviour
{


    [SerializeField] GameObject SkillPoint;
    [SerializeField] Image Hpfill;

    [SerializeField] TextMeshProUGUI HpText;
    [SerializeField] TextMeshProUGUI DamageText;
    [SerializeField] TextMeshProUGUI indexText;
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI PassiveDescText;
    [SerializeField] GameObject PassiveDescription;

    [SerializeField] TextMeshProUGUI[] BuffIcon;
    

    int MaxHP;
    int CurrentHP;



    public void Initialize(int maxHp, int damage, int index , string name)
    {
        (MaxHP) = maxHp;
        CurrentHP = MaxHP;

        string enemyname ="";
        for (int i = 0; i < name.Length -1;i++)
        {
            enemyname += name[i];
        }


        HpText.text = CurrentHP.ToString();
        NameText.text = (enemyname + "<color=#EA133D>") + name[name.Length-1] +"</color>";
        DamageText.text = "<b><color=#EA133D>A</color></b>TK <size=20>" + damage.ToString() + "</size>";
        indexText.text = index.ToString();


        PassiveDescription.SetActive(false);

        for (int i = 0; i < BuffIcon.Length; i++)
        {
            BuffIcon[i].gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    public void UpdateBuffIcon(List<Buff> buffs)
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].GetBuffDurationTurn() != 0)
            {

                switch (buffs[i])
                {
                    case FireBuff F: // »¡
                        BuffIcon[0].gameObject.transform.parent.gameObject.SetActive(true);
                        BuffIcon[0].text = buffs[i].GetBuffDurationTurn().ToString();
                        break;

                    case ElecBuff F:// ÆÄ
                        BuffIcon[1].gameObject.transform.parent.gameObject.SetActive(true);
                        BuffIcon[1].text = buffs[i].GetBuffDurationTurn().ToString();
                        break;


                    case CaptivBuff F:// º¸
                        BuffIcon[2].gameObject.transform.parent.gameObject.SetActive(true);
                        BuffIcon[2].text = buffs[i].GetBuffDurationTurn().ToString();
                        break;

                    case CurseBuff F: // ÃÊ
                        BuffIcon[3].gameObject.transform.parent.gameObject.SetActive(true);
                        BuffIcon[3].text = buffs[i].GetBuffDurationTurn().ToString();
                        break;




                }
            }
        }
    }

    public void UpdateStatus(int hp, int damage, int index  )
    {
        CurrentHP = hp;
        Hpfill.fillAmount = (float)CurrentHP / (float)MaxHP;


        HpText.text = CurrentHP.ToString();
        DamageText.text = "<b><color=#EA133D>A</color></b>TK <size=20>" + damage.ToString() + "</size>";
        indexText.text = index.ToString();

        for (int i = 0; i < BuffIcon.Length; i++)
        {
            BuffIcon[i].gameObject.transform.parent.gameObject.SetActive(false);
        }

        
    }

    public void OnPassiveDescription()
    {
        PassiveDescription.SetActive(true);
    }

}

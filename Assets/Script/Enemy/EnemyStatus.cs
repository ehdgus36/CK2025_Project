using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class EnemyStatus : MonoBehaviour
{
   
    //HP
    [SerializeField] Image Hpfill;
    [SerializeField] TextMeshProUGUI HpText;
    
    //Damage UI
    [SerializeField] TextMeshProUGUI DamageText;
   
  
 
    [SerializeField] GameObject PassiveDescription;

    [SerializeField] TextMeshProUGUI[] BuffIcon;
    [SerializeField] Image num;
    [SerializeField] Sprite[] numIndex;

 



    public void Initialize(EnemyData enemyData)
    {
        HpText.text = enemyData.EnemyUnitData.CurrentHp.ToString();
        DamageText.text = enemyData.CurrentDamage.ToString();
        

        for (int i = 0; i < BuffIcon.Length; i++)
        {
            BuffIcon[i].gameObject.transform.parent.gameObject.SetActive(false);
        }

        PassiveDescription.SetActive(false);
    }

    public void UpdateBuffIcon(List<Buff> buffs)
    {
        if (buffs == null) return;


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

    public void UpdateStatus(EnemyData enemyData)
    {

        Hpfill.fillAmount = (float)enemyData.EnemyUnitData.CurrentHp / (float)enemyData.EnemyUnitData.MaxHp;
        HpText.text = enemyData.EnemyUnitData.CurrentHp.ToString();



        DamageText.text = enemyData.CurrentDamage.ToString();

      

        for (int i = 0; i < BuffIcon.Length; i++)
        {
            BuffIcon[i].gameObject.transform.parent.gameObject.SetActive(false);
        }

        UpdateBuffIcon(enemyData.buffs);
    }

    public void OnPassiveDescription()
    {
        PassiveDescription.SetActive(true);
    }

    public void OffPassiveDescription()
    {
        PassiveDescription.SetActive(false);
    }

 
}

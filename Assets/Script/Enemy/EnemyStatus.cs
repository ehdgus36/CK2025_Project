using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class EnemyStatus : DynamicUIObject
{

    public override string DynamicDataKey => UI_keyData;
    [SerializeField] string UI_keyData;

    [SerializeField] GameObject SkillPoint;
    [SerializeField] Image Hpfill;

    [SerializeField] TextMeshProUGUI HpText;
    [SerializeField] TextMeshProUGUI DamageText;
    [SerializeField] TextMeshProUGUI indexText;
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI PassiveDescText;
    [SerializeField] GameObject PassiveDescription;

    [SerializeField] TextMeshProUGUI[] BuffIcon;
    [SerializeField] Image num;
    [SerializeField] Sprite[] numIndex;

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


        HpText.text = "50"+"/"+CurrentHP.ToString();
        NameText.text = (enemyname + "<color=#EA133D>") + name[name.Length-1] +"</color>";
        DamageText.text = damage.ToString();
        //indexText.text = index.ToString();


        PassiveDescription.SetActive(false);

        for (int i = 0; i < BuffIcon.Length; i++)
        {
            BuffIcon[i].gameObject.transform.parent.gameObject.SetActive(false);
        }
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
                    case FireBuff F: // 빨
                        BuffIcon[0].gameObject.transform.parent.gameObject.SetActive(true);
                        BuffIcon[0].text = buffs[i].GetBuffDurationTurn().ToString();
                        break;

                    case ElecBuff F:// 파
                        BuffIcon[1].gameObject.transform.parent.gameObject.SetActive(true);
                        BuffIcon[1].text = buffs[i].GetBuffDurationTurn().ToString();
                        break;


                    case CaptivBuff F:// 보
                        BuffIcon[2].gameObject.transform.parent.gameObject.SetActive(true);
                        BuffIcon[2].text = buffs[i].GetBuffDurationTurn().ToString();
                        break;

                    case CurseBuff F: // 초
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


        HpText.text = "50" + "/" + CurrentHP.ToString();
        DamageText.text =  damage.ToString();

        num.sprite = numIndex[index];
        ///indexText.text = index.ToString();

        for (int i = 0; i < BuffIcon.Length; i++)
        {
            BuffIcon[i].gameObject.transform.parent.gameObject.SetActive(false);
        }

        
    }

    public void OnPassiveDescription()
    {
        PassiveDescription.SetActive(true);
    }

    public void OffPassiveDescription()
    {
        PassiveDescription.SetActive(false);
    }

    public override void UpdateUIData(object update_ui_data)
    {
        EnemyData uiEnemyData = (EnemyData)update_ui_data;

        if (uiEnemyData != null)
        {
            UpdateStatus(uiEnemyData.EnemyUnitData.CurrentHp, uiEnemyData.CurrentDamage, 0);
            UpdateBuffIcon(uiEnemyData.buffs);
        }
        //여기에서 업데이트 
    }
}

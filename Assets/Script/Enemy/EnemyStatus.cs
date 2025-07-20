using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class EnemyStatus : MonoBehaviour
{

    //HP
    [SerializeField] HP_Bar HP_Bar;

    [SerializeField] Barrier_ViewUI Barrier_viewUI;
    
    
    //Damage UI
    [SerializeField] TextMeshProUGUI DamageText;

    //Shilld UI
    [SerializeField] TextMeshProUGUI ShilldText;

    //buff UI
    [SerializeField] Buff_Icon_UI Buff_UI;


    [SerializeField] GameObject _StatusPopUp;

    [SerializeField] EnemySkillPoint _enemySkillPoint;

    [SerializeField] TextMeshProUGUI _RhythmDamageText;

    public TextMeshProUGUI RhythmDamageText { get { return _RhythmDamageText; } }

    public GameObject StatusPopUp { get { return _StatusPopUp; } }
 



    public void Initialize(EnemyData enemyData)
    {
        HP_Bar.UpdateUI(enemyData.EnemyUnitData.MaxHp, enemyData.EnemyUnitData.CurrentHp);
        
        _enemySkillPoint.UpdateUI(enemyData.CurrentSkillPoint);

        Barrier_viewUI.UpdateUI(enemyData.CurrentDefense);

        DamageText.text = enemyData.CurrentDamage.ToString();
        Buff_UI.UpdateBuffIcon(enemyData.buffs);
        _StatusPopUp.SetActive(false);
    }

    
    public void UpdateStatus(EnemyData enemyData)
    {

        HP_Bar.UpdateUI(enemyData.EnemyUnitData.MaxHp, enemyData.EnemyUnitData.CurrentHp);

        _enemySkillPoint.UpdateUI(enemyData.CurrentSkillPoint);

        Barrier_viewUI.UpdateUI(enemyData.CurrentDefense);

        DamageText.text = enemyData.CurrentDamage.ToString();

        Buff_UI.UpdateBuffIcon(enemyData.buffs);
    }

    public void OnPassiveDescription()
    {
        
    }

    public void OffPassiveDescription()
    {
       
    }

 
}

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

    [SerializeField] NextAttackUIView _NextAttackUI;

    public TextMeshProUGUI RhythmDamageText { get { return _RhythmDamageText; } }

    public GameObject StatusPopUp { get { return _StatusPopUp; } }
 
    public NextAttackUIView NextAttackUI { get { return _NextAttackUI; } }

    EnemyData enemyData;

    Enemy UpdateTargetEnemy;

    public void Initialize(Enemy enemy)
    {
       
        UpdateTargetEnemy = enemy;
        enemyData = enemy.EnemyData;
        HP_Bar.UpdateUI(enemyData.EnemyUnitData.MaxHp, enemyData.EnemyUnitData.CurrentHp);
        
        _enemySkillPoint.UpdateUI(enemyData.CurrentSkillPoint , enemyData.MaxSkillPoint);

        Barrier_viewUI.UpdateUI(enemyData.EnemyUnitData.CurrentBarrier);

        DamageText.text = enemyData.CurrentDamage.ToString();
        Buff_UI.UpdateBuffIcon(enemyData.EnemyUnitData.buffs);
        _StatusPopUp.SetActive(false);

        NextAttackUI.UpdateUI(enemyData, UpdateTargetEnemy.AIMachine.aIBehavior as EnemyAIBehavior);
    }

    
    public void UpdateStatus()
    {

        enemyData = UpdateTargetEnemy.EnemyData;

        HP_Bar.UpdateUI(enemyData.EnemyUnitData.MaxHp, enemyData.EnemyUnitData.CurrentHp);

        _enemySkillPoint.UpdateUI(enemyData.CurrentSkillPoint, enemyData.MaxSkillPoint);


        Barrier_viewUI.UpdateUI(enemyData.EnemyUnitData.CurrentBarrier);
        DamageText.text = enemyData.CurrentDamage.ToString();

        Buff_UI.UpdateBuffIcon(enemyData.EnemyUnitData.buffs);

        NextAttackUI.UpdateUI(enemyData, UpdateTargetEnemy.AIMachine.aIBehavior as EnemyAIBehavior);
    }

    public void OnPassiveDescription()
    {
        
    }

    public void OffPassiveDescription()
    {
       
    }

 
}

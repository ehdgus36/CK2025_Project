using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerSkillSystem : MonoBehaviour
{
    [SerializeField] PlayableDirector SkillCutScene;
    [SerializeField] int SkillDamage;
    // Update is called once per frame
    void Update()
    {

   
        
    }

    public void SkillAttack()
    {
        List<Enemy> enemies = new List<Enemy>();

        for (int i = 0; i < GameManager.instance.EnemysGroup.Enemys.Count; i++)
        {
            enemies.Add(GameManager.instance.EnemysGroup.Enemys[i]);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].TakeDamage(SkillDamage);
        }
        GameManager.instance.FMODManagerSystem.FMODChangeNomal();
    }

    

    public void StartSkill()
    {
        SkillCutScene.Play();

       
        GameManager.instance.FMODManagerSystem.FMODChangeUpgrade();
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Effect/Strength_Attack/Click_Button");


        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, 0);
    }
}

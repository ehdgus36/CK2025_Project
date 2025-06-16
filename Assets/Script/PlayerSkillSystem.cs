using System.Collections.Generic;
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartSkill();
        }
        
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

        int GradeData = 0;
        if (GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, out GradeData))
        {

            if (GradeData >= 100)
            {
                SkillCutScene.Play();
            }
        }
        GameManager.instance.FMODManagerSystem.FMODChangeUpgrade();
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/Effect/Strength_Attack/Click_Button");
    }
}

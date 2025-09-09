using UnityEngine;

public class EnemySkillPoint : MonoBehaviour
{
    [SerializeField] GameObject[] SkillPoints;
    [SerializeField] GameObject[] SkillPointSloat;

    public void UpdateUI(int skill_point , int MaxSkill_Poins)
    {

        if (skill_point > SkillPoints.Length || skill_point < 0) return;


        for (int i = 0; i < SkillPointSloat.Length; i++)
        {
            SkillPointSloat[i].SetActive(false);
        }

        for (int i = 0; i < MaxSkill_Poins; i++)
        {
            SkillPointSloat[i].SetActive(true);
        }

       



        for (int i = 0; i < SkillPoints.Length;i++)
        {
            SkillPoints[i].SetActive(false);
        }

        for (int i = 0; i < skill_point; i++)
        {
            SkillPoints[i].SetActive(true);
        }

    }
}

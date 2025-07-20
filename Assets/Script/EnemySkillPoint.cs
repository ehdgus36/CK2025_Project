using UnityEngine;

public class EnemySkillPoint : MonoBehaviour
{
    [SerializeField] GameObject[] SkillPoints;


    public void UpdateUI(int skill_point)
    {

        if (skill_point > SkillPoints.Length || skill_point < 0) return;

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

using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] Transform SkillPointParent;
    [SerializeField] GameObject[] SkillFills;

    [SerializeField] GameObject SkillPoint;
    [SerializeField] Image Hpfill;

    [SerializeField] TextMeshProUGUI Hptext;



    int MaxHP;
    int CurrentHP;

    int MaxSkillCount;
    int CurrentSkillCount;

    public void Initialize(int maxHp, int maxSkillCount)
    {
        (MaxHP, MaxSkillCount) = (maxHp, maxSkillCount);
        (CurrentHP, CurrentSkillCount) = (MaxHP, MaxSkillCount);

        SkillFills = new GameObject[maxSkillCount];


        for (int i = 0; i < SkillFills.Length; i++)
        {
            GameObject skillpoint = Instantiate(SkillPoint);
            skillpoint.transform.SetParent(SkillPointParent);
            SkillFills[i] = skillpoint.transform.GetChild(0).gameObject;
        }


        for (int i = 0; i < SkillFills.Length; i++)
        {
            SkillFills[i].SetActive(false);
        }


        Hptext.text = MaxHP.ToString() + "/" + CurrentHP.ToString();
    }

    public void SetCurrentHp(int hp)
    {
        CurrentHP = hp;
        Hpfill.fillAmount = (float)CurrentHP / (float)MaxHP;


        Hptext.text = MaxHP.ToString() + "/" + CurrentHP.ToString();
    }

    public void SetCurrentSkill(int skill)
    {
        CurrentSkillCount = skill;

        for (int i = 0; i < SkillFills.Length; i++)
        {
            SkillFills[i].SetActive(false);
        }

        for (int i = 0; i < CurrentSkillCount; i++)
        {
            SkillFills[i].SetActive(true);
        }
    }
}

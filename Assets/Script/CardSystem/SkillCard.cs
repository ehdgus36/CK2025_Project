using UnityEngine;

public class SkillCard : Card
{
    [SerializeField] int SkillDamage = 10;
    [SerializeField] GameObject Skill_Cut;
    private void Start()
    {
        Skill_Cut = GameObject.Find("Skill_Cut").gameObject;
        Skill_Cut?.SetActive(false);
        Initialized(new SlotGroup());
    }
    public override void TargetExcute(Enemy Target, Card nextCard = null)
    {
        

        Debug.Log(cardData.Card_Des);
        this.transform.parent.gameObject.SetActive(false);
        Skill_Cut?.SetActive(false);
        Skill_Cut?.SetActive(true);
        base.TargetExcute(Target,nextCard);

        

        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, 0);
    }

    
}

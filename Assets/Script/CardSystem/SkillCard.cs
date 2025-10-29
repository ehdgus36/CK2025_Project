using UnityEngine;

public class SkillCard : Card
{
    
    [SerializeField] GameObject Skill_Cut;
    private void Start()
    {
        Skill_Cut = GameObject.Find("Skill_Cut").gameObject;
        Skill_Cut?.SetActive(false);
        if ("SKILL" == CardID) { CardAction = new SkillAction(this); }
        Initialized(new SlotGroup());
    }
    public override void TargetExcute(Enemy Target, Card nextCard = null)
    {
                
        Skill_Cut?.SetActive(false);
        Skill_Cut?.SetActive(true);


       
        StartCoroutine(CardAction.StartAction(GameManager.instance.Player, this, this.cardData, Target));



        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, 0);
    }

    
}

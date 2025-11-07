using UnityEngine;

public class SkillCard : Card
{
    
    [SerializeField] GameObject Skill_Cut;
    private void Start()
    {
        
        Skill_Cut?.SetActive(false);

        CardID = GameManager.instance.ItemDataLoader.stickerData.Card_Bring;

        if (CardID == null) return;

        if ("SKILL" == CardID) { CardAction = new SkillAction(this); }
        if ("SKILL2" == CardID) { CardAction = new SkillAction(this); }
        if ("SKILL3" == CardID) { CardAction = new SkillAction(this); }
        Initialized(new SlotGroup());
    }
    public override void TargetExcute(Enemy Target, Card nextCard = null)
    {
        if (CardID == null) return;

        Skill_Cut?.SetActive(false);
        Skill_Cut?.SetActive(true);
        Skill_Cut?.GetComponent<Animator>().Play("Skill1_Cut_Animation");



        StartCoroutine(CardAction.StartAction(GameManager.instance.Player, this, this.cardData, Target));



        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, 0);
    }

    
}

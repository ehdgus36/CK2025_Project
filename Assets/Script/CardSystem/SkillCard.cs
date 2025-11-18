using UnityEngine;

public class SkillCard : Card
{
    
    [SerializeField] GameObject Skill_Cut;

    string animeCode;
    private void Start()
    {
        
        Skill_Cut?.SetActive(false);

        CardID = GameManager.instance.ItemDataLoader.stickerData.Card_Bring;

        if (CardID == null) return;

        if ("SKILL1" == CardID) { CardAction = new SkillAction(this); animeCode = "Skill1_Cut_Animation"; }
        if ("SKILL2" == CardID) { CardAction = new Skill2Action(this); animeCode = "Skill2_Cut_Animation"; }
        if ("SKILL3" == CardID) { CardAction = new Skill3Action(this); animeCode = "Skill3_Cut_Animation"; }
        Initialized(new SlotGroup());
    }
    public override void TargetExcute(Enemy Target, Card nextCard = null)
    {
        if (CardID == null) return;

        Skill_Cut?.SetActive(false);
        Skill_Cut?.SetActive(true);
        Skill_Cut?.GetComponent<Animator>().Play(animeCode);



        StartCoroutine(CardAction.StartAction(GameManager.instance.Player, this, this.cardData, Target));



        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, 0);
    }

    
}

using UnityEngine;

public class SkillCard : Card
{
    [SerializeField] int SkillDamage = 10;
    private void Start()
    {
        Initialized(new SlotGroup());
    }
    public override void TargetExcute(Enemy Target, Card nextCard = null)
    {
        

        Debug.Log(cardData.Card_Des);
        this.transform.parent.gameObject.SetActive(false);
        base.TargetExcute(Target,nextCard);
        
        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.SKILL_POINT_DATA, 0);
    }

    
}

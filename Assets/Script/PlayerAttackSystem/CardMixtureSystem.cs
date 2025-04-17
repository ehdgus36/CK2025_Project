using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;






public class CardMixtureSystem : MonoBehaviour
{


    [SerializeField] List<Card> CardData;
    [SerializeField] SlotGroup CDMixtureSlotGroup;


    [SerializeField] TextAsset MixtureData;


    Player AttackPlayer;
    AttackData MaidAttackData;

    
    [SerializeField] List<AttackData> RecipeData;

    public void Initialize()
    {
        CDMixtureSlotGroup.Initialize(SelectionCard);
        // AttackPlayer = GameManager.instance.GetPlayer();
        List<Dictionary<string, object>> recipe = CSVReader.Read(MixtureData);

        for (int i = 0; i < CSVReader.Read(MixtureData).Count; i++)
        {
            RecipeData.Add(new AttackData(recipe , i));
        }

    }
    public void Start()
    {
        Initialize();
    }





    void SelectionCard()
    {
        CardData = CDMixtureSlotGroup.ReadData<Card>();
        if (CardData.Count == 3)
        {
            GameManager.instance.GetAttackManager().Attack(MaidAttackData, CardData[2].GetComponent<TargetCard>().GetTargetIndex());
        }

        if (CardData.Count == 2)
        {
            for (int i = 0; i < RecipeData.Count; i++)
            {
                if (RecipeData[i].Add_Code == (CardData[0].GetID() + CardData[1].GetID()))
                {
                    MaidAttackData = RecipeData[i];


                    Debug.Log(RecipeData[i].Add_Code);
                }
            }
        }



    }


}

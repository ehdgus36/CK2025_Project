using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;






public class CardMixtureSystem : MonoBehaviour
{


    [SerializeField] List<Card> CardData;
    [SerializeField] SlotGroup CDMixtureSlotGroup;


    [SerializeField] TextAsset MixtureData;


    Player AttackPlayer;
    AttackData MadeAttackData;

    [SerializeField] List<Dictionary<string, object>> Recipe;


    public void Initialize()
    {
        //CDMixtureSlotGroup.Initialize(SelectionCard);
       // AttackPlayer = GameManager.instance.GetPlayer();
        Recipe = CSVReader.Read(MixtureData);

    }
    public void Start()
    {
        Initialize();
    }




    private void Update()
    {
        SelectionCard();
    }

    void SelectionCard()
    {
        //CardData = CDMixtureSlotGroup.ReadData<Card>();
        if (CardData.Count == 3)
        {

        }

        if (CardData.Count == 2)
        {
            for (int i = 0; i < Recipe.Count; i++)
            {
                if ( Recipe[i]["Add_Code"].ToString() == (CardData[0].GetID() + CardData[1].GetID()) )
                {
                    Debug.Log(Recipe[i]["Base_Damage_1"].ToString());
                }
            }
        }



    }


}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;






public class CardMixtureSystem : MonoBehaviour
{


    [SerializeField] List<Card> CardData;
    [SerializeField] SlotGroup CDMixtureSlotGroup;

    [SerializeField] CemeteryUI[] Cemetery;


    [SerializeField] TextAsset MixtureData;

    [SerializeField] SkeletonGraphic GuitarAnime;
    Player AttackPlayer;
    AttackData MaidAttackData;

    
    [SerializeField] List<AttackData> RecipeData;


    private void OnDisable() //비활성화 하면 조합데에 있는 카드 묘지로
    {
        List< Card> CDdata = CDMixtureSlotGroup.ReadData<Card>();
        for (int i = 0; i < CDdata.Count; i++)
        {
           // Cemetery[i].Insert(CDdata[i]);
        }
      
    }

    public void Initialize()
    {
        CDMixtureSlotGroup.Initialize(SelectionCard);
        // AttackPlayer = GameManager.instance.GetPlayer();
        List<Dictionary<string, object>> recipe = CSVReader.Read(MixtureData);

        for (int i = 0; i < CSVReader.Read(MixtureData).Count; i++)
        {
            RecipeData.Add(new AttackData(recipe , i));
        }

        if (GuitarAnime != null)
        {
            GuitarAnime.AnimationState.SetAnimation(0, "Main", true);
            GuitarAnime.AnimationState.SetAnimation(1, "Dial", true);

        }

    }
   




    void SelectionCard()
    {
        CardData = CDMixtureSlotGroup.ReadData<Card>();
        GuitarAnime.AnimationState.ClearTrack(2);
        GuitarAnime.AnimationState.AddAnimation(2, "in_Guitar", false, 0.3f);
        if (CardData.Count == 3)
        {
            GuitarAnime.AnimationState.AddAnimation(3, "in_Tuner3", false, 0.3f);
            GuitarAnime.AnimationState.AddAnimation(4, "in_Tuner3-2", true, 0.3f);
            //GameManager.instance.GetAttackManager().Attack(MaidAttackData, CardData[2].GetComponent<TargetCard>().GetTargetIndex());
            return;
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

            GuitarAnime.AnimationState.AddAnimation(3, "in_Tuner2", false , 0.3f);
            GuitarAnime.AnimationState.AddAnimation(4, "in_Tuner2-2", true, 0.3f);
        }

        if (CardData.Count == 1)
        {
            GuitarAnime.AnimationState.AddAnimation(3, "in_Tuner1", false, 0.3f);
            GuitarAnime.AnimationState.AddAnimation(4, "in_Tuner1-2", true, 0.3f);
        }
       

    }


}

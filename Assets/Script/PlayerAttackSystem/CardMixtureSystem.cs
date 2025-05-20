using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;
using System.Collections;
using Unity.VisualScripting;







public class CardMixtureSystem : MonoBehaviour
{


    [SerializeField] List<Card> CardData;
    [SerializeField] SlotGroup CDMixtureSlotGroup;

    [SerializeField] CemeteryUI[] Cemetery;


    [SerializeField] TextAsset MixtureData;

    //[SerializeField] SkeletonGraphic GuitarAnime;

    [SerializeField] TextMeshProUGUI DamageText;
    Player AttackPlayer;
    RecipeData MaidAttackData;


    [SerializeField] List<RecipeData> RecipeData;

    [SerializeField] GameObject Descriptobj;
    [SerializeField] TextMeshProUGUI MixtureName;
    [SerializeField] TextMeshProUGUI Descript;

    

    [SerializeField] Dack[] dack;

    [SerializeField] AudioClip[] slot;
    [SerializeField] AudioSource audioSource;

  

    [SerializeField] GuitarSkill GuitarSkill;

    [SerializeField] int UpgradePercent = 0;

    bool isGradeAttack = false;

    public void GuitarSetUp()
    {
       

        if (UpgradePercent == 100)
        {
            isGradeAttack = true;
        }
    }

    public void Return()
    {
        List<Card> CDdata = CDMixtureSlotGroup.ReadData<Card>();
        for (int i = 0; i < CDdata.Count; i++)
        {
            // CDdata[i].GetComponent<Animator>().Play("Idle");

            if (CDdata[i].GetUpGradeCard() == null)
            {
                CDdata[i].transform.position = new Vector3(100, 100, 1000);
            }

            Cemetery[i].Insert(CDdata[i]);

        }


        
        DamageText.text = "";
        Descriptobj.SetActive(false);

        
    }

    public void Initialize()
    {
        CDMixtureSlotGroup.Initialize(SelectionCard);
       

    }
  




    void SelectionCard()
    {
        CardData = CDMixtureSlotGroup.ReadData<Card>();
     
        


        if (CardData.Count == 3)
        {
            GameManager.instance.Player.PlayerCardAnime();
          
            Descript.text = MaidAttackData.Explain_Down;



          

            if (isGradeAttack == false)
            {
                StartCoroutine(SendAttack()); // 타이밍바 실행
            }

            if (isGradeAttack == true)
            { 
          
            }

           

            
            audioSource.PlayOneShot(slot[2]);
            return;
        }

        if (CardData.Count == 2)
        {

            if (GameDataSystem.StaticGameDataSchema.RECIPE_DATA_BASE.SearchData(CardData[0].GetID() + CardData[1].GetID(),ref MaidAttackData))
            {
               
                MixtureName.text = MaidAttackData.Explain_Up_2;
                DamageText.text = MaidAttackData.Base_Damage_1.ToString();
                Descript.text = MaidAttackData.Explain_Down;
                Debug.Log(MaidAttackData.Add_Code);
            }


            
       
          

            GameManager.instance.Player.PlayerCardAnime();
            audioSource.PlayOneShot(slot[1]);
        }

        if (CardData.Count == 1)
        {
            Descriptobj.SetActive(true);
            MixtureName.text = CardData[0].Example;
            Descript.text = "";
           
           

            GameManager.instance.Player.PlayerCardAnime();
            audioSource.PlayOneShot(slot[0]);
        }


    }


    //스위치문 수정 필요 데미지 증감
    IEnumerator SendAttack()
    {
        GameManager.instance.AttackManager.Attack(MaidAttackData, CardData[2].GetComponent<TargetCard>().GetTargetIndex());
        yield return null;
    }
}

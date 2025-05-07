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

    [SerializeField] SkeletonGraphic GuitarAnime;

    [SerializeField] TextMeshProUGUI DamageText;
    Player AttackPlayer;
    RecipeData MaidAttackData;


    [SerializeField] List<RecipeData> RecipeData;

    [SerializeField] GameObject Descriptobj;
    [SerializeField] TextMeshProUGUI MixtureName;
    [SerializeField] TextMeshProUGUI Descript;

    [SerializeField] UpGradeBar UpGradeBar;

    [SerializeField] Dack[] dack;

    [SerializeField] AudioClip[] slot;
    [SerializeField] AudioSource audioSource;

    [SerializeField] Animator UIAnime;

    [SerializeField] NoteSystemBar NoteBar;
    public void GuitarSetUp()
    {
        UIAnime.Play("SetUp");
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


        GuitarAnime.AnimationState.ClearTrack(2);
        GuitarAnime.AnimationState.ClearTrack(3);
        GuitarAnime.AnimationState.ClearTrack(4);
        GuitarAnime.AnimationState.SetAnimation(0, "Main", true);
        DamageText.text = "";
        Descriptobj.SetActive(false);

        UIAnime.Play("Return");
    }

    public void Initialize()
    {
        CDMixtureSlotGroup.Initialize(SelectionCard);
        // AttackPlayer = GameManager.instance.GetPlayer();

        //StartCoroutine(SetUpRecipeData());

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
            GameManager.instance.Player.PlayerCardAnime();
            GuitarAnime.AnimationState.AddAnimation(3, "in_Tuner3", false, 0.3f);
            GuitarAnime.AnimationState.AddAnimation(4, "in_Tuner3-2", true, 0.3f);
            Descript.text = MaidAttackData.Explain_Down;

            UIAnime.Play("TimingBar");
            
            StartCoroutine(SendAttack()); // 타이밍바 실행

            UpGradeBar.SetPoint(CardData[2].Grade_Point);

            if (UpGradeBar.GetCurrentPoint() == 5)
            {
                dack[2].InsertCard(CardData[2].GetUpGradeCard());
                UpGradeBar.SetPoint(0);
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


            GuitarAnime.AnimationState.AddAnimation(3, "in_Tuner2", false, 0.3f);
            GuitarAnime.AnimationState.AddAnimation(4, "in_Tuner2-2", true, 0.3f);
            UpGradeBar.SetPoint(CardData[1].Grade_Point);
            if (UpGradeBar.GetCurrentPoint() == 5)
            {
                dack[1].InsertCard(CardData[1].GetUpGradeCard());
                UpGradeBar.SetPoint(0);
            }

            GameManager.instance.Player.PlayerCardAnime();
            audioSource.PlayOneShot(slot[1]);
        }

        if (CardData.Count == 1)
        {
            Descriptobj.SetActive(true);
            MixtureName.text = CardData[0].Example;
            Descript.text = "";
            GuitarAnime.AnimationState.AddAnimation(3, "in_Tuner1", false, 0.3f);
            GuitarAnime.AnimationState.AddAnimation(4, "in_Tuner1-2", true, 0.3f);
            UpGradeBar.SetPoint(CardData[0].Grade_Point);


            if (UpGradeBar.GetCurrentPoint() == 5)
            {
                dack[0].InsertCard(CardData[0].GetUpGradeCard());
                UpGradeBar.SetPoint(0);
            }

            GameManager.instance.Player.PlayerCardAnime();
            audioSource.PlayOneShot(slot[0]);
        }


    }


    //스위치문 수정 필요 데미지 증감
    IEnumerator SendAttack()
    {
       
        NoteBar.PlayNote();


        yield return new WaitUntil(() => (NoteBar.Verdict != ""));

        switch (NoteBar.Verdict)
        {
            case "Good":
                GameManager.instance.AttackManager.Attack(MaidAttackData, CardData[2].GetComponent<TargetCard>().GetTargetIndex());
                break;
            case "Miss":
                Debug.Log("miss 공격");
                GameManager.instance.AttackManager.Attack(MaidAttackData, CardData[2].GetComponent<TargetCard>().GetTargetIndex());
                break;
            default:
                GameManager.instance.AttackManager.Attack(MaidAttackData, CardData[2].GetComponent<TargetCard>().GetTargetIndex());
                break;
        }
       
    }
}

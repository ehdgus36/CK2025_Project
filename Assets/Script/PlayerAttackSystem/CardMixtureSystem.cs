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

    //public void GuitarSetUp()
    //{
    //    if (UpgradePercent == 100)
    //    {
    //        isGradeAttack = true;
    //    }
    //}

    //public void Return()
    //{
    //    List<Card> CDdata = CDMixtureSlotGroup.ReadData<Card>();
    //    for (int i = 0; i < CDdata.Count; i++)
    //    {
    //        // CDdata[i].GetComponent<Animator>().Play("Idle");

    //        if (CDdata[i].GetUpGradeCard() == null)
    //        {
    //            CDdata[i].transform.position = new Vector3(100, 100, 1000);
    //        }

    //        Cemetery[i].Insert(CDdata[i]);

    //    }

    //    DamageText.text = "";
    //    Descriptobj.SetActive(false);
    //}

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
               
            }

            if (isGradeAttack == true)
            {

            }


            return;
        }

        if (CardData.Count == 2)
        {

            

            GameManager.instance.Player.PlayerCardAnime();

        }

        if (CardData.Count == 1)
        {
            Descriptobj.SetActive(true);
           
            Descript.text = "";

            GameManager.instance.Player.PlayerCardAnime();
        }


    }


    ////스위치문 수정 필요 데미지 증감
    //IEnumerator SendAttack()
    //{
    //    GameManager.instance.AttackManager.Attack(MaidAttackData);
    //    yield return null;
    //}
}

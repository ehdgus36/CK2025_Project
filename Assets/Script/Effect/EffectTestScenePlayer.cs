using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class EffectTestScenePlayer : MonoBehaviour
{
    [SerializeField] List<Button> EffectActiveButton;
    [SerializeField] Button InstanceButton;

    [SerializeField] EffectData PlayerEffectData;
    [SerializeField] GameObject ButtonInstanceZone;

    [SerializeField] Enemy enemy;

    [SerializeField] EffectSystem PlayerEffectSystem;

    string[] CardCode = { "C1011",
                         
                          "C1021",
                          
                          "C1031",
                         
                          "C1041",
                         
                          "C2011",
                         
                          "C2021",
                         
                          "C2031",
                          
                          "C2041",
                         
                          "C3011",
                          
                          "C3021",
                          
                          "C3031",
                          "C3041",
                          "C3051"};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < CardCode.Length; i++)
        {
            GameObject insButtton = Instantiate(InstanceButton.gameObject);
            insButtton.transform.SetParent(ButtonInstanceZone.transform);
            Button insbutton = insButtton.GetComponent<Button>();

            string IndexCode = CardCode[i];
            object cardData;
            GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(IndexCode, out cardData);
            insbutton.onClick.AddListener(() => { PlayEffect(IndexCode); });
            insbutton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((CardData)cardData).Effect_Code;

            EffectActiveButton.Add(insbutton);
        }

        Destroy(InstanceButton.gameObject);

    }

    void PlayEffect(string CardCode)
    {
        object data;
        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(CardCode, out data);
        CardData cardData = (CardData)data;
        for (int i = 0; i < EffectActiveButton.Count; i++)
        {
            EffectActiveButton[i].interactable = false;
        }

        StartCoroutine(TargetAttack(cardData));
    }

    IEnumerator TargetAttack(CardData data)
    {
        Vector3 startPos = this.transform.position;
        if (data.MoveType == "M" )
        {
            this.transform.position = enemy.transform.position - new Vector3(2, 0, 0);
        }
        yield return new WaitForSeconds(.1f);
        if (data.Effect_Pos == "P")
        {
            PlayerEffectSystem.PlayEffect(data.Effect_Code, this.transform.position);
        }
        else
        {
            PlayerEffectSystem.PlayEffect(data.Effect_Code, enemy.transform.position);
        }
        yield return new WaitForSeconds(1.4f); // 이동 후 딜레이



        this.transform.position = startPos;
        yield return null;

        for (int i = 0; i < EffectActiveButton.Count; i++)
        {
            EffectActiveButton[i].interactable = true;
        }
    }
}

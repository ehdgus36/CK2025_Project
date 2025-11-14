using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ClearSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinText;
    [SerializeField] TextMeshProUGUI StageText;
    [SerializeField] string LoadScene = "GameMap";

    [SerializeField] GameObject ClearView;
    [SerializeField] GameObject UpgradeView;
    [SerializeField] Image itemImage;

    private void OnEnable()
    {
        if (ClearView == null && UpgradeView == null) return;

        CoinText.text = GameManager.instance?.GetClearGold.ToString();

        String stageData = "";
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<String>(GameDataSystem.KeyCode.DynamicGameDataKeys.STAGE_DATA, out stageData);

        StageText.text = stageData;

        ClearView?.SetActive(false);
        UpgradeView?.SetActive(false);




        // æ∆¿Ã≈€ ∑£¥˝

        List<String> randData = new List<String>();

        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<String>>(GameDataSystem.KeyCode.DynamicGameDataKeys.RAND_ITEM_DATA, out randData);


        string randstr = randData[UnityEngine.Random.Range(0, randData.Count)];
        randData.Remove(randstr);

        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.RAND_ITEM_DATA, randData);

        object data = null;
        if (GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(randstr, out data))
        {
            string Path = "ItemImage/";
            Sprite cardSprite = null;



            
          

            if (data is StickerItemData)
            {
                Path += ((StickerItemData)data).ItemImage;
                cardSprite = Resources.Load<Sprite>(Path);

                List<String> list = new List<String>();
                GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<String>>(GameDataSystem.KeyCode.DynamicGameDataKeys.STICKER_ITME_INVENTORY_DATA, out list);

                list.Add(randstr);
                GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STICKER_ITME_INVENTORY_DATA, list);
            }

            if (data is StrapItemData)
            {
                Path += ((StrapItemData)data).ItemImage;
                cardSprite = Resources.Load<Sprite>(Path);
                List<String> list = new List<String>();
                GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<String>>(GameDataSystem.KeyCode.DynamicGameDataKeys.STRAP_ITME_INVENTORY_DATA, out list);

                list.Add(randstr);
                GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STICKER_ITME_INVENTORY_DATA, list);
            }

            if (data is StringItemData)
            {
                Path += ((StringItemData)data).ItemImage;
                cardSprite = Resources.Load<Sprite>(Path);
                List<String> list = new List<String>();
                GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<String>>(GameDataSystem.KeyCode.DynamicGameDataKeys.STRING_ITME_INVENTORY_DATA, out list);

                list.Add(randstr);
                GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STICKER_ITME_INVENTORY_DATA, list);
            }

            itemImage.sprite = cardSprite;

        }

        StartCoroutine(ClearSequence());
    }


    IEnumerator ClearSequence()
    {
        GameManager.instance.ControlleCam.Play("DieCamAnime");

        yield return new WaitForSeconds(.5f);

        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/UI/Clear_Stage");
        ClearView?.SetActive(true);
        yield return new WaitUntil(() => ClearView.activeSelf == false);
        GameManager.instance.FMODManagerSystem.PlayEffectSound("event:/UI/Upgrade/Upgrade_Appear");
        UpgradeView?.SetActive(true);
    }

    public void LoadMap()
    {
        SceneManager.LoadScene(LoadScene);
    }
}

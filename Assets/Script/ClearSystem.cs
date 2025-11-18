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
    [SerializeField] TextMeshProUGUI ItemDesc;

    private void OnEnable()
    {
        if (ClearView == null && UpgradeView == null) return;

        CoinText.text = GameManager.instance?.GetClearGold.ToString();

        String stageData = "";
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<String>(GameDataSystem.KeyCode.DynamicGameDataKeys.STAGE_DATA, out stageData);

        StageText.text = stageData;

        ClearView?.SetActive(false);
        UpgradeView?.SetActive(false);


        ItemDesc.transform.parent.gameObject.SetActive(false);

        // 아이템 랜덤

        List<String> randData = new List<String>();

        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<String>>(GameDataSystem.KeyCode.DynamicGameDataKeys.RAND_ITEM_DATA, out randData);


        if (randData.Count > 0)
        {
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

                    list[list.IndexOf("0")] = randstr;
                    GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STICKER_ITME_INVENTORY_DATA, list);

                    ItemDesc.text = string.Format("<color=#E0096C>Sticker</color>\n<size=18>{0}</size>", ((StickerItemData)data).ItemDes);
                }

                if (data is StrapItemData)
                {
                    Path += ((StrapItemData)data).ItemImage;
                    cardSprite = Resources.Load<Sprite>(Path);
                    List<String> list = new List<String>();
                    GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<String>>(GameDataSystem.KeyCode.DynamicGameDataKeys.STRAP_ITME_INVENTORY_DATA, out list);

                    list[list.IndexOf("0")] = randstr;
                    GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STRAP_ITME_INVENTORY_DATA, list);

                    ItemDesc.text = string.Format("<color=#C6A8EE>Strap</color>\n<size=18>{0}</size>", ((StrapItemData)data).ItemDes);
                }

                if (data is StringItemData)
                {
                    Path += ((StringItemData)data).ItemImage;
                    cardSprite = Resources.Load<Sprite>(Path);
                    List<String> list = new List<String>();
                    GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<String>>(GameDataSystem.KeyCode.DynamicGameDataKeys.STRING_ITME_INVENTORY_DATA, out list);

                    list[list.IndexOf("0")] = randstr;
                    GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.STRING_ITME_INVENTORY_DATA, list);

                    ItemDesc.text = string.Format("<color=#0D9E9B>String</color>\n<size=18>{0}</size>", ((StringItemData)data).ItemDes);
                }

                itemImage.sprite = cardSprite;

            }                      
        }
        else if (randData.Count == 0)
        {
            itemImage.color = new Color(1, 1, 1, 0); // 투명
            itemImage.raycastTarget = false;
        }

        StartCoroutine(ClearSequence());
    }


    IEnumerator ClearSequence()
    {
      
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

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{

    [SerializeField] Image[] ItemImage;

    [SerializeField] bool isStart = false;


    List<string> itemCodeData = null;

    private void Start()
    {
        PopUpView();
    }
    public void PopUpView()
    {

        if (this.gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        if (isStart == true) this.gameObject.SetActive(true);

        for (int i = 0; i  <  ItemImage.Length; i++)
        {
            ItemImage[i].color = new Color(1,1,1,0);
        }


        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.ITEM_HOLD_DATA, out itemCodeData);

        int selectImageIndex = 0;

        for (int i = 0; i < itemCodeData.Count; i++)
        {
            if (itemCodeData[i] == "0") continue;

            object getitem = null;

            GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(itemCodeData[i], out getitem);

            string Path = "ItemImage/";
            Sprite cardSprite = null;

            if (i == 0)
            {
                Path += ((StickerItemData)getitem).ItemImage;
                cardSprite = Resources.Load<Sprite>(Path);
            }

            if (i == 1)
            {
                Path += ((StrapItemData)getitem).ItemImage;
                cardSprite = Resources.Load<Sprite>(Path);
            }

            if (i == 2)
            {
                Path += ((StringItemData)getitem).ItemImage;
                cardSprite = Resources.Load<Sprite>(Path);
            }

            ItemImage[selectImageIndex].sprite = cardSprite;
            ItemImage[selectImageIndex].color = Color.white;

            selectImageIndex++;
        }
    }

    public void Exit()
    {
        this.gameObject.SetActive(false);
    }
}

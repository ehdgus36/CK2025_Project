using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{

    [SerializeField] Image[] ItemImage;

    [SerializeField] bool isStart = false;

    [SerializeField] TextMeshProUGUI[] Itemtext;
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


            if (getitem != null)
            {
                if (i == 0)
                {
                    if (getitem is StickerItemData)
                    {
                        Path += ((StickerItemData)getitem).ItemImage;
                        cardSprite = Resources.Load<Sprite>(Path);

                        Itemtext[selectImageIndex].text = string.Format("<color=#E0096C>{0}</color>\n<size=14>{1}</size>", ((StickerItemData)getitem).ItemNameKR,((StickerItemData)getitem).ItemDes);
                    }
                }

                if (i == 1)
                {
                    if (getitem is StrapItemData)
                    {
                        Path += ((StrapItemData)getitem).ItemImage;
                        cardSprite = Resources.Load<Sprite>(Path);


                        Itemtext[selectImageIndex].text = string.Format("<color=#C6A8EE>{0}</color>\n<size=14>{1}</size>", ((StrapItemData)getitem).ItemNameKR,((StrapItemData)getitem).ItemDes);
                    }
                }

                if (i == 2)
                {
                    if (getitem is StringItemData)
                    {
                        Path += ((StringItemData)getitem).ItemImage;
                        cardSprite = Resources.Load<Sprite>(Path);


                        Itemtext[selectImageIndex].text = string.Format("<color=#0D9E9B>{0}</color>\n<size=14>{1}</size>", ((StringItemData)getitem).ItemNameKR , ((StringItemData)getitem).ItemDes);
                    }
                }
            }

            ItemImage[selectImageIndex].sprite = cardSprite;
            ItemImage[selectImageIndex].color = Color.white;
            ItemImage[selectImageIndex].raycastTarget = true;




            selectImageIndex++;
        }
    }

    public void Exit()
    {
        this.gameObject.SetActive(false);
    }
}

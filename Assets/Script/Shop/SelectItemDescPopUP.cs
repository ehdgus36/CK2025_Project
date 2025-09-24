using UnityEngine;
using TMPro;
using UnityEngine.UI;



[System.Serializable]
struct PopUpItemData
{
    [SerializeField]public string itemID;
    [SerializeField]public Sprite itemImage;
}
public class SelectItemDescPopUP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ItemName;
    [SerializeField] TextMeshProUGUI ItemRank;
    [SerializeField] TextMeshProUGUI ItemDesc;
    [SerializeField] TextMeshProUGUI ItmePrice;

    [SerializeField] Image ItemImage;
    [SerializeField] PopUpItemData[] popUpItemDatas;


    public void ViewPopUP(string itemID)
    {
        ItemData data;
        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(itemID, out data);
        ItemName.text = data.Name;
       



        for (int i = 0; i < popUpItemDatas.Length; i++)
        {
            if (popUpItemDatas[i].itemID == itemID)
            {
                if (itemID[0] == 'I')
                {
                    ItemImage.rectTransform.sizeDelta = new Vector2(1024f, 1024f);
                    ItemRank.text = data.Tag;
                    ItemDesc.text = data.Example;
                    ItmePrice.text = data.Price.ToString();
                    ItemImage.sprite = popUpItemDatas[i].itemImage;
                }

                if (itemID[0] == 'C')
                {
                    ItemImage.rectTransform.sizeDelta = new Vector2(576f, 576f);
                    ItemRank.text = data.Tag;
                    ItemDesc.text = data.Example;
                    ItmePrice.text = data.Price.ToString();
                    object cardData;

                    GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(itemID, out cardData);

                    ItemImage.sprite = Resources.Load<Sprite>("CardImage/" + ((CardData)cardData).Card_Im);

                }
            }
            else
            {
                if (itemID[0] == 'C')
                {
                    ItemImage.rectTransform.sizeDelta = new Vector2(576f, 576f);
                    ItemRank.text = data.Tag;
                    ItemDesc.text = data.Example;
                    ItmePrice.text = data.Price.ToString();
                    object cardData;

                    GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(itemID, out cardData);

                    ItemImage.sprite = Resources.Load<Sprite>("CardImage/" + ((CardData)cardData).Card_Im);

                }
            }

        }
    
    }

}

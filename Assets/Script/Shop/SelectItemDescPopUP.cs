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



    public void ViewPopUP(string itemID)
    {
        ShopData data;
      
        object cardData;

        GameDataSystem.StaticGameDataSchema.Shop_DATA_BASE.SearchData(itemID, out data);
        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(itemID, out cardData);

        ItemImage.rectTransform.sizeDelta = new Vector2(576f, 576f);

      

        ItemName.text = ((CardData)cardData).Card_Name_KR;
        ItemDesc.text = ((CardData)cardData).Card_Des;
        ItemRank.text = data.Rank;
        ItmePrice.text = data.Price.ToString();

        ItemImage.sprite = Resources.Load<Sprite>("CardImage/" + ((CardData)cardData).Card_Im);
    }

}

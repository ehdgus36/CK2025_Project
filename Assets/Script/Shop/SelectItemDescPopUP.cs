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


    [SerializeField] TextMeshProUGUI BuffText;

    [SerializeField] Image ItemImage;

    [SerializeField] ShopEvent ShopEvent;

    CardData SelectCardData;

    public void ViewPopUP(string itemID)
    {
        ShopData data;
      
        object cardData;

        GameDataSystem.StaticGameDataSchema.Shop_DATA_BASE.SearchData(itemID, out data);
        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(itemID, out cardData);

        ItemImage.rectTransform.sizeDelta = new Vector2(576f, 576f);

        SelectCardData = (CardData)cardData;

        ItemName.text = ((CardData)cardData).Card_Name_KR;
        ItemDesc.text = ((CardData)cardData).Card_Des;

        if (data.Rank == "1") { ItemRank.text = "ÀÏ¹Ý"; }
        if (data.Rank == "2") { ItemRank.text = "Èñ±Í"; }
        if (data.Rank == "3") { ItemRank.text = "Àü¼³"; }


        float itemPrice = (float)data.Price;
        if (ShopEvent.GetItemDataLoader.strapData.Shop_Sale > 0)
        {
            itemPrice = Mathf.Round(((float)data.Price * ((100f - (float)ShopEvent.GetItemDataLoader.strapData.Shop_Sale) / 100f)));
        }

        ItmePrice.text = itemPrice.ToString();

        ItemImage.sprite = Resources.Load<Sprite>("CardImage/" + ((CardData)cardData).Card_Im);
    }


    public void BuffDesc(string Tag)
    {


        if (Tag == "buff1")
        {
            BuffText.text = SelectCardData.Buff_Ex1;
            BuffText.text = BuffText.text.Replace("<burnup>", "5");
            BuffText.text = BuffText.text.Replace("<buzz>", "25");
            BuffText.text = BuffText.text.Replace("<burnout>", "30");
        }

        if (Tag == "buff2")
        {
            BuffText.text = SelectCardData.Buff_Ex2;
            BuffText.text = BuffText.text.Replace("<burnup>", "5");
            BuffText.text = BuffText.text.Replace("<buzz>", "25");
            BuffText.text = BuffText.text.Replace("<burnout>", "30");
        }
    }

}

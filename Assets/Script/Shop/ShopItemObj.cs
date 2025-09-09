using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ShopItemObj :MonoBehaviour, IPointerDownHandler
{

    [SerializeField] string ItemID;
    [SerializeField] string ItemType;
    [SerializeField] TextMeshProUGUI ItemNameText;
    [SerializeField] TextMeshProUGUI ItemPriceText;

    [HideInInspector] public ShopEvent ShopEvent;

    bool isSoldOut = false;

    Image CardImage;

    private void Start()
    {
        ResetCard();
    }

    public void ResetCard()
    {
        if (ItemID == "")
        {
            string randomCard = GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.RandomCard();
            ItemID = randomCard;
          
            ItemData data;
            GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(randomCard, out data);
            Debug.Log(data.Name + " sdfdsfa d");

            //ItemNameText.text = data.Name;
            ItemPriceText.text = data.Price.ToString();

            if (CardImage = GetComponent<Image>())
            {
                object cardData = null;
                GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(ItemID,out cardData);
                CardImage.sprite = Resources.Load<Sprite>("CardImage/" + ((CardData)cardData).Card_Im);
            }
        }
        else
        {
            if (ItemID[0] == 'I')
            {
                ItemData data;
                GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(ItemID, out data);

                //ItemNameText.text = data.Name;
                ItemPriceText.text = "Coin: " + data.Price.ToString();
            }
            else
            {

                string randomCard = GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.RandomCard();
                ItemID = randomCard;

                ItemData data;
                GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(ItemID, out data);

                //ItemNameText.text = data.Name;
                ItemPriceText.text = "Coin: " + data.Price.ToString();
                if (CardImage = GetComponent<Image>())
                {
                    object cardData = null;
                    GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(ItemID, out cardData);
                    CardImage.sprite = Resources.Load<Sprite>("CardImage/" + ((CardData)cardData).Card_Im);
                }
            }
        }

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (isSoldOut == true) return;

        ShopEvent.SelectItemID = ItemID;
        if (ItemType == "Tape") ShopEvent.SelectTape(ShopEvent.TapeList.IndexOf(this));
        if (ItemType == "Peak") ShopEvent.SelectPeak(ShopEvent.PeakList.IndexOf(this));

        
    }

    public void SoldOut()
    {
        ItemNameText.text = string.Format("<s>{0}</s>", ItemNameText.text);

        ItemPriceText.text = "SOLD OUT!!";
        isSoldOut = true;
    }
}

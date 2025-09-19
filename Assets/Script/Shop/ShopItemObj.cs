using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
public class ShopItemObj :MonoBehaviour, IPointerDownHandler , IPointerEnterHandler,IPointerExitHandler
{

    [SerializeField] public string ItemID;
    [SerializeField] string ItemType;
    [SerializeField] TextMeshProUGUI ItemNameText;
    [SerializeField] TextMeshProUGUI ItemPriceText;

    [HideInInspector] public ShopEvent ShopEvent;

    bool isSoldOut = false;

    [SerializeField]Image CardImage;

    [SerializeField]Vector3 startPos;
    [SerializeField]Quaternion startRotat;


    bool isSelect = false;
    private void Start()
    {
        startPos = transform.position;
        startRotat = transform.rotation;

        ResetCard();
    }

    public void PositionReset()
    {
        isSelect = false;
        CardImage.color = Color.white;
        transform.position = startPos;
        transform.rotation = startRotat;
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

            if (CardImage == null)
            {
                CardImage = GetComponent<Image>();
                object cardData = null;
                GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(ItemID, out cardData);
                CardImage.sprite = Resources.Load<Sprite>("CardImage/" + ((CardData)cardData).Card_Im);
            }
            else
            {
                object cardData = null;
                GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(ItemID, out cardData);
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
                if (CardImage == null)
                {
                    CardImage = GetComponent<Image>();
                    object cardData = null;
                    GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(ItemID, out cardData);
                    CardImage.sprite = Resources.Load<Sprite>("CardImage/" + ((CardData)cardData).Card_Im);
                }
                else
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

        Vector3 Pos = transform.position;
        Quaternion Rotat = transform.rotation;

        ShopEvent.SelectItemID = ItemID;
        if (ItemType == "Tape") ShopEvent.SelectTape(ShopEvent.TapeList.IndexOf(this));
        if (ItemType == "Peak") ShopEvent.SelectPeak(ShopEvent.PeakList.IndexOf(this));


        transform.position = Pos;
        transform.rotation = Rotat;

        CardImage.color = new Color(0, 0, 0, 0);
        isSelect = true;
    }

    public void SoldOut()
    {
        ItemNameText.text = string.Format("<s>{0}</s>", ItemNameText.text);

        ItemPriceText.text = "SOLD OUT!!";
        isSoldOut = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelect == true) return;

        StartCoroutine(MoveSlot());
    }

    IEnumerator MoveSlot()
    {
        Vector3 targetPos = transform.position + new Vector3(0,55f,0);
        Quaternion targetQuat = Quaternion.Euler(0,0, transform.rotation.z +5f);

        float t = 0;

        for (int i = 0; i < 10; i++)
        {
            t += .1f;
            transform.position = Vector3.Lerp(transform.position, targetPos, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuat, t);

            yield return new WaitForSeconds(.025f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();

        if (isSelect == true) return;

        transform.position = startPos;
        transform.rotation = startRotat;
    }
}

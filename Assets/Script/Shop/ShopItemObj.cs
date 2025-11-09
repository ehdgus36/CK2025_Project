using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FMODUnity;
using System.Collections.Generic;

public class ShopItemObj : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] public string ItemID;
    [SerializeField] string ItemType;
    [SerializeField] TextMeshProUGUI ItemNameText;
    [SerializeField] TextMeshProUGUI ItemPriceText;

    [HideInInspector] public ShopEvent ShopEvent;

    bool isSoldOut = false;

    [SerializeField] Image CardImage;

    [SerializeField] Vector3 startPos;
    [SerializeField] Quaternion startRotat;
    [SerializeField] RectTransform Empty;

    [SerializeField] bool isRedCard;
    [SerializeField] bool isYellowCard;
    [SerializeField] bool isBlueCard;

    int startIndex;

    bool isSelect = false;

    public string randomCard;
    private void Start()
    {
        startPos = transform.position;
        startRotat = transform.rotation;

        // ResetCard();
        startIndex = this.transform.GetSiblingIndex();
    }

    public void PositionReset()
    {
        isSelect = false;
        CardImage.color = Color.white;
        transform.position = startPos;
        transform.rotation = startRotat;
        transform.localScale = Vector3.one;
    }

    public void ResetCard(ShopItemObj previewData = null)
    {
        List<string> cardID = new List<string>();

        if (isRedCard) { cardID.AddRange(new List<string>() { "C1011", "C1021", "C1021" }); }
        if (isYellowCard) { cardID.AddRange(new List<string>() { "C1031", "C1041", "C1061", "C1071", "C2051", "C3021" }); }
        if (isBlueCard) { cardID.AddRange(new List<string>() { "C1051", "C2031", "C2041", "C3031", "C3041", "C3051" }); }



        isSoldOut = false;
        if (previewData == null)
            randomCard = cardID[Random.Range(0, cardID.Count)];
        else 
        {
            randomCard = cardID[Random.Range(0, cardID.Count)];
            while (randomCard == previewData.randomCard)
            {
                randomCard = cardID[Random.Range(0, cardID.Count)];
            }
        }
        ItemID = randomCard;

        ShopData data;
        GameDataSystem.StaticGameDataSchema.Shop_DATA_BASE.SearchData(randomCard, out data);

        if (CardImage == null) CardImage = GetComponent<Image>();

        object cardData = null;
        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(ItemID, out cardData);
        CardImage.sprite = Resources.Load<Sprite>("CardImage/" + ((CardData)cardData).Card_Im);


        ItemNameText.text = ((CardData)cardData).Card_Name_KR;

        float itemPrice = (float)data.Price;
        if (ShopEvent.GetItemDataLoader.strapData.Shop_Sale > 0)
        {
            itemPrice = Mathf.Round((float)data.Price / (float)ShopEvent.GetItemDataLoader.strapData.Shop_Sale);
        }

        ItemPriceText.text = ((int)itemPrice).ToString();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (isSoldOut == true) return;

        Vector3 Pos = transform.position;
        Quaternion Rotat = transform.rotation;

        ShopEvent.SelectItemID = ItemID;
        if (ItemType == "Tape") ShopEvent.SelectTape(ShopEvent.TapeList.IndexOf(this));
        if (ItemType == "Peak") ShopEvent.SelectPeak(ShopEvent.PeakList.IndexOf(this));

        RuntimeManager.PlayOneShot("event:/UI/Card_Click");

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
        this.transform.SetAsLastSibling();

        Empty.localScale = Vector3.one * 2f;

        RuntimeManager.PlayOneShot("event:/UI/Card_Over");

        StartCoroutine(MoveSlot());
    }

    IEnumerator MoveSlot()
    {
        Vector3 targetPos = transform.position + new Vector3(0, 55f, 0);
        Quaternion targetQuat = Quaternion.Euler(0, 0, transform.rotation.z + 5f);

        float t = 0;

        for (int i = 0; i < 10; i++)
        {
            t += .1f;
            transform.position = Vector3.Lerp(transform.position, targetPos, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuat, t);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 1.2f, t);

            yield return new WaitForSeconds(.025f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();

        if (isSelect == true) return;

        transform.position = startPos;
        transform.rotation = startRotat;
        transform.localScale = Vector3.one;
        Empty.localScale = Vector3.one;

        this.transform.SetSiblingIndex(startIndex);
    }
}

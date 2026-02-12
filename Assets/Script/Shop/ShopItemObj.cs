using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine.Rendering;

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

    [SerializeField] GameObject SoldOutObject;

    int startIndex;

    bool isSelect = false;

    public string randomCard;

    Coroutine StopObject;
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

        CardImage.color = isSoldOut == false ? Color.white : CardImage.color;
        transform.position = startPos;
        transform.rotation = startRotat;
        transform.localScale = Vector3.one;


        GetComponent<Image>().raycastTarget = true;
    }

    public void ResetCard(ShopItemObj previewData = null)
    {
        List<string> cardID = new List<string>();

        if (isRedCard) { cardID.AddRange(new List<string>() { "CA1011", "CA1021", "CA3061" }); }
        if (isYellowCard) { cardID.AddRange(new List<string>() { "CB1031", "CB1061", "CB1071", "CB2051", "CB3021" }); }
        if (isBlueCard) { cardID.AddRange(new List<string>() { "CD1051", "CD2031", "CD2041", "CD3031", "CD3041", "CD3051" ,"CD1041"}); }

        SoldOutObject.SetActive(false);


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

        Debug.Log("카드 이미지" + ((CardData)cardData).Card_Im);
        CardImage.sprite = Resources.Load<Sprite>("CardImage/" + ((CardData)cardData).Card_Im);
        CardImage.color = Color.white;

        ItemNameText.text = ((CardData)cardData).Card_Name_KR;

        float itemPrice = (float)data.Price;
        if (ShopEvent.GetItemDataLoader.strapData.Shop_Sale > 0)
        {          
            itemPrice = Mathf.Round(((float)data.Price * ((100f - (float)ShopEvent.GetItemDataLoader.strapData.Shop_Sale) / 100f)));
        }

        ItemPriceText.text = ((int)itemPrice).ToString();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (isSoldOut == true) return;

        if (StopObject != null)
            StopCoroutine(StopObject);

        Vector3 Pos = transform.position;
        Quaternion Rotat = transform.rotation;

        ShopEvent.SelectItemID = ItemID;
        if (ItemType == "Tape") ShopEvent.SelectTape(ShopEvent.TapeList.IndexOf(this));
        if (ItemType == "Peak") ShopEvent.SelectPeak(ShopEvent.PeakList.IndexOf(this));

        RuntimeManager.PlayOneShot("event:/UI/Card_Click");


        Vector3 targetPos = transform.position + new Vector3(0, 0.8f, 0);
        Quaternion targetQuat = Quaternion.Euler(0, 0, transform.rotation.z + 5f);

  
        transform.position = Vector3.Lerp(transform.position, targetPos, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQuat, 1);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 1.2f, 1);

        


        GetComponent<Image>().raycastTarget = false;
        CardImage.color = new Color(0, 0, 0, 0);
        isSelect = true;
    }

    public void SoldOut()
    {
        ItemNameText.text = string.Format("<s>{0}</s>", ItemNameText.text);

        CardImage.color = new Color(1, 1, 1, 0);

        ItemPriceText.text = "";
        isSoldOut = true;

        SoldOutObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelect == true) return;
        this.transform.SetAsLastSibling();

        Empty.localScale = Vector3.one * 2f;

        RuntimeManager.PlayOneShot("event:/UI/Card_Over");

        StopObject = StartCoroutine(MoveSlot());
    }

    IEnumerator MoveSlot()
    {
        Vector3 targetPos = transform.position + new Vector3(0, 0.8f, 0);
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
        if (StopObject != null)
            StopCoroutine(StopObject);

        if (isSelect == true) return;

        transform.position = startPos;
        transform.rotation = startRotat;
        transform.localScale = Vector3.one;
        Empty.localScale = Vector3.one;

        this.transform.SetSiblingIndex(startIndex);
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;
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

    int startIndex;

    bool isSelect = false;
    private void Start()
    {
        startPos = transform.position;
        startRotat = transform.rotation;

        ResetCard();
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

    public void ResetCard()
    {
        isSoldOut = false;
        string randomCard = GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.RandomCard();
        ItemID = randomCard;

        ShopData data;
        GameDataSystem.StaticGameDataSchema.Shop_DATA_BASE.SearchData(randomCard, out data);

        if (CardImage == null) CardImage = GetComponent<Image>();

        object cardData = null;
        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.SearchData(ItemID, out cardData);
        CardImage.sprite = Resources.Load<Sprite>("CardImage/" + ((CardData)cardData).Card_Im);


        ItemNameText.text = ((CardData)cardData).Card_Name_KR;
        ItemPriceText.text = data.Price.ToString();
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
        this.transform.SetAsLastSibling();

        Empty.localScale = Vector3.one * 2f;

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
            transform.localScale = Vector3.Lerp( transform.localScale,Vector3.one * 1.2f,t);

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

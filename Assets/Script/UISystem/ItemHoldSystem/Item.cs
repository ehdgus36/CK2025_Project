using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Item : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string ItemType { get; protected set; }
    public string ItemName { get; protected set; }
    public string ItemDesc { get; protected set; }

    public string ItemCode => ItemID;

    GameObject DescObject;

    [SerializeField] protected string ItemID;
    void Start()
    {
        if (ItemID != "" && ItemID != "0")
        {

            GetComponent<RectTransform>().sizeDelta = new Vector3(128f, 128f);
            Initialized();
        }
    }

    protected abstract void Initialized();


    public void Initialized(string itemID)
    {
        ItemID = itemID;
        Initialized();
        GetComponent<RectTransform>().sizeDelta = new Vector3(128f, 128f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RuntimeManager.PlayOneShot("event:/UI/Item_Stage/Item_Click");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (DescObject == null)
            DescObject = GameObject.Find("ItemDescView");

        if (DescObject != null)
        {
            Debug.Log("item클릭 확인");
            DescObject.transform.position = this.transform.position;
            DescObject.SetActive(true);
            DescObject.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("<color=#FFA200>{0}</color>\n<size=18>{1}</size>", ItemName, ItemDesc);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (DescObject == null)
            DescObject = GameObject.Find("ItemDescView");

        if (DescObject != null)
        {
            DescObject.SetActive(false);
        }

    }
}

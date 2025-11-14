using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string ItemType { get; protected set; }
    public string ItemName { get; protected set; }
    public string ItemDesc { get; protected set; }

    public string ItemCode => ItemID;

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
}

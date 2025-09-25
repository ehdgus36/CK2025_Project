using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string ItemType { get; protected set; }
    public string ItemName { get; protected set; }
    public string ItemDesc { get; protected set; }

    [SerializeField] protected string ItemID;
    void Start()
    {
        Initialized();
    }

    protected abstract void Initialized();
    
}

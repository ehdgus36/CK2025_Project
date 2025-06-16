using UnityEngine;
using TMPro;
using UnityEngine.UI;



[System.Serializable]
struct PopUpItemData
{
    [SerializeField]public string itemID;
    [SerializeField]public Sprite itemImage;
    [TextArea(2, 3)] public string Desc;
    [SerializeField]public string itemTag;

}
public class SelectItemDescPopUP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ItemName;
    [SerializeField] TextMeshProUGUI ItemTag;
    [SerializeField] TextMeshProUGUI ItemDesc;

    [SerializeField] Image ItemImage;
    [SerializeField] PopUpItemData[] popUpItemDatas;


    public void ViewPopUP(string itemID)
    {
        ItemData data;
        GameDataSystem.StaticGameDataSchema.ITEM_DATA_BASE.SearchData(itemID, out data);
        ItemName.text = data.Name;
        

        for (int i = 0; i < popUpItemDatas.Length; i++)
        {
            if (popUpItemDatas[i].itemID == itemID)
            { 
                ItemTag.text =data.Tag;
                ItemDesc.text = data.Example;
                ItemImage.sprite = popUpItemDatas[i].itemImage;
            }
        }
    
    }

}

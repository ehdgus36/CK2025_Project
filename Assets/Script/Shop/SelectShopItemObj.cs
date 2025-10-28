using UnityEngine;
using TMPro;

public class SelectShopItemObj : MonoBehaviour
{
    string ItemName = "";
    string price = "";

    GameObject shopObj;
    public void Initialize(GameObject shopItemObj)
    {  
        shopObj = shopItemObj;
    }

    private void OnEnable()
    {
        TextMeshProUGUI[] myText = this.GetComponentsInChildren<TextMeshProUGUI>();

        ItemName = shopObj?.GetComponent<TextMeshProUGUI>().text;
        price = shopObj?.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;


        myText[0].text = ItemName;
        myText[1].text = price;
    }
}

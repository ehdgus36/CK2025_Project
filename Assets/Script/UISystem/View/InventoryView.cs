using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [System.Serializable]
    struct ItemViewData
    {
        [SerializeField] public string key;
        [SerializeField] public Sprite image;
    }

    [SerializeField] ItemViewData[] itemViewDatas;
    [SerializeField] Image[] ItemImage;

    [SerializeField] bool isStart = false;

    Dictionary<string, Sprite> itemsData = new Dictionary<string, Sprite>();

    List<string> itemCodeData = null;

    private void Start()
    {
        PopUpView();
    }
    public void PopUpView()
    {
        if (itemsData.Count == 0)
        {
            for (int i = 0; i < itemViewDatas.Length; i++)
            {
                itemsData.Add(itemViewDatas[i].key, itemViewDatas[i].image);
            }
        }

        if (this.gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        if(isStart == true) this.gameObject.SetActive(true);


        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.ITME_DATA, out itemCodeData);
        if (itemCodeData != null)
        {
            for (int i = 0; i < itemCodeData.Count; i++)
            {
                ItemImage[i].sprite = itemsData[itemCodeData[i]];

                ItemImage[i].color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void Exit()
    {
        this.gameObject.SetActive(false);
    }
}

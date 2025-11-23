using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextLinkEvent : MonoBehaviour
{
    public TMP_Text tmpText;
    public GameObject tooltip;

    private int currentLink = -1;

    [SerializeField] SelectItemDescPopUP popUP;

    public void Update()
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(tmpText, Input.mousePosition, Camera.main);

        if (linkIndex != -1 && linkIndex != currentLink)
        {
            currentLink = linkIndex;
            TMP_LinkInfo linkInfo = tmpText.textInfo.linkInfo[linkIndex];

            if (linkInfo.GetLinkID() == "buff1" || linkInfo.GetLinkID() == "buff2")
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                tooltip.transform.position = pos;
                popUP.BuffDesc(linkInfo.GetLinkID());
                Debug.Log("정보처리");
                tooltip.SetActive(true);
            }
        }
        else if (linkIndex == -1)
        {
            currentLink = -1;
            tooltip.SetActive(false);
        }
    }



    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
        tooltip.SetActive(false);
    }

}

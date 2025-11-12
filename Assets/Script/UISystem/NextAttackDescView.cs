using UnityEngine;
using TMPro;

public class NextAttackDescView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDesc;
    private void OnEnable()
    {
        if(textDesc == null) textDesc = GetComponent<TextMeshProUGUI>();

        //Vector2 size;
        //size.x = textDesc.preferredWidth;
        //size.y = textDesc.preferredHeight;

        //GetComponent<RectTransform>().sizeDelta = size;
    }
}

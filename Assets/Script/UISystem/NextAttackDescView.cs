using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextAttackDescView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDesc;
    private void OnEnable()
    {
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
        StartCoroutine(sort());
    }

    IEnumerator sort()
    {

        yield return null;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());

        yield return null;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
    }
}

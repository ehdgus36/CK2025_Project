using TMPro;
using UnityEngine;

public class BuffTextRePlace : MonoBehaviour
{
    TextMeshProUGUI mainText;
    public void OnEnable()
    {
        mainText = GetComponent<TextMeshProUGUI>();

        mainText.text = mainText.text.Replace("<burnup>", FireBuff.GetBuffValue.ToString());
        mainText.text = mainText.text.Replace("<buzz>", AttackDamageDownBuff.GetBuffValue.ToString());
        Debug.Log("텍스트 리플레이스");
    }
}

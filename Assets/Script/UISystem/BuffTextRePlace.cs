using TMPro;
using UnityEngine;

public class BuffTextRePlace : MonoBehaviour
{
    TextMeshProUGUI mainText;
    public void OnEnable()
    {
        if(mainText == null)
        mainText = GetComponent<TextMeshProUGUI>();

        mainText.text = mainText.text.Replace("<burnup>", FireBuff.GetBuffValue.ToString());
        mainText.text = mainText.text.Replace("<buzz>", AttackDamageDownBuff.GetBuffValue.ToString());
        Debug.Log("텍스트 리플레이스");
    }

    public void Replace()
    {
        if (mainText == null)
            mainText = GetComponent<TextMeshProUGUI>();

        mainText.text = mainText.text.Replace("<burnup>", FireBuff.GetBuffValue.ToString());
        mainText.text = mainText.text.Replace("<buzz>", AttackDamageDownBuff.GetBuffValue.ToString());
        mainText.text = mainText.text.Replace("<burnout>", FireBuff.GetBuffValue.ToString());
        Debug.Log("텍스트 리플레이스");
    }
}

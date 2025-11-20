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
       
    }

    public void Replace()
    {
        if (mainText == null)
            mainText = GetComponent<TextMeshProUGUI>();

        mainText.text = mainText.text.Replace("<burnup>", FireBuff.GetBuffValue.ToString());
        mainText.text = mainText.text.Replace("<buzz>", AttackDamageDownBuff.GetBuffValue.ToString());
        mainText.text = mainText.text.Replace("<burnout>", FireBuffBrunOut.GetBuffValue.ToString());
       
    }
}

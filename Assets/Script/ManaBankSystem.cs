using UnityEngine;
using TMPro;

public class ManaBankSystem : MonoBehaviour
{

    int CurrentMana = 0;
    TextMeshProUGUI ManaBankText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ManaBankText = GetComponent<TextMeshProUGUI>();
    }

    public void insertMana(int mana)
    {
        CurrentMana += mana;
        ManaBankText.text = "ManaBanke : " + CurrentMana.ToString();
    }
}

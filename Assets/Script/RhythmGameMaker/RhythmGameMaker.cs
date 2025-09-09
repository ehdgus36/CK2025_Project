using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RhythmGameMaker : MonoBehaviour
{
    [SerializeField] MakerNote[] makerNotes;
    [SerializeField] TextMeshProUGUI code;
    [SerializeField] Button copyButton;

    private void Start()
    {
        copyButton.onClick.AddListener(copyText);
    }

    public void OnEnable()
    {
        code.text = "";
        for (int i = 0; i < makerNotes.Length; i++)
        {
            makerNotes[i].gameMaker = this;
            code.text += makerNotes[i].code.ToString();
        }
    }

    public void ResetCode()
    {
        code.text = "";
        for (int i = 0; i < makerNotes.Length; i++)
        {
            code.text += makerNotes[i].code.ToString();
        }
    }

    public void copyText()
    {
        GUIUtility.systemCopyBuffer = code.text;
        
    }
}

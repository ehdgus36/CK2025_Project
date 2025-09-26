using UnityEngine;
using System.Collections;
using TMPro;
public class Inputnote : MonoBehaviour
{
    public bool good;
    public bool miss;

    public int mouseInput;

    [SerializeField] Sprite LSprite;
    [SerializeField] Sprite RSprite;

    [SerializeField] TextMeshProUGUI bpmtext;

    public void Reset()
    {
        
    }

    public void StartNote(char tag)
    {
        if (tag == '1')
        {
            GetComponent<UnityEngine.UI.Image>().sprite = LSprite;
            mouseInput = 0;
            if(bpmtext != null)
                bpmtext.color = Color.blue;
        }


        if (tag == '2')
        {
            GetComponent<UnityEngine.UI.Image>().sprite = RSprite;
            mouseInput = 1;
            if (bpmtext != null)
                bpmtext.color = Color.green;
        }

       

        good = false;
        miss = false;

        StartCoroutine(noteState());
    }

    IEnumerator noteState()
    {
        good = true;
        yield return new WaitForSeconds(0.2f);
        good = false;
        miss = true;
    }
}

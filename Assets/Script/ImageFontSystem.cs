using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageFontSystem : MonoBehaviour
{
    [SerializeField] GameObject[] MainFont;
    [SerializeField] GameObject[] MainFont2;
    [SerializeField] GameObject[] SubFont;
    [SerializeField] GameObject[] SubFont2;

    [SerializeField] GameObject Parentheses1;
    [SerializeField] GameObject Parentheses2;
    [SerializeField] GameObject hyphen;

    [SerializeField] GameObject Mainhyphen;

    [SerializeField] string mainfont;
    [SerializeField] string subfont;

    private void Start()
    {
        FontConvert(mainfont, subfont);
    }


    IEnumerator DelayDisable()
    {
        yield return new WaitForSeconds(1.2f);
        this.gameObject.SetActive(false);
    }

    public void FontConvert(string num, string subFont)
    {
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);


        for (int i = 0; i < MainFont.Length;i++)
        {
            MainFont[i].SetActive(false);
           
        }

        for (int i = 0; i < MainFont2.Length; i++)
        {
            MainFont2[i].SetActive(false);
            
        }

        for (int i = 0; i < SubFont.Length; i++)
        {
            SubFont[i].SetActive(false);
        }

        for (int i = 0; i < SubFont2.Length; i++)
        {
            SubFont2[i].SetActive(false);
        }


        if (num != null)
        {
            if (num.Length == 1)
            {
                int index = int.Parse(num);
                MainFont[index].SetActive(true);
            }

            if (num.Length == 2)
            {
                string f = num[0].ToString();
                string b = num[1].ToString();

                int index = int.Parse(f);
                MainFont[index].SetActive(true);
                index = int.Parse(b);
                MainFont2[index].SetActive(true);
            }

            if (subFont != null)
            {
                Parentheses1.SetActive(true);
                Parentheses2.SetActive(true);
                hyphen.SetActive(true);

                if (subFont.Length == 1)
                {
                    int index = int.Parse(subFont);
                    MainFont[index].SetActive(true);
                }

                if (subFont.Length == 2)
                {
                    string f = subFont[0].ToString();
                    string b = subFont[1].ToString();

                    int index = int.Parse(f);
                    SubFont[index].SetActive(true);
                    index = int.Parse(b);
                    SubFont2[index].SetActive(true);
                }
            }
            else
            {
                Parentheses1.SetActive(false);
                Parentheses2.SetActive(false);
                hyphen.SetActive(false);
            }

        }

        StartCoroutine(DelayDisable());
        
    }

    public void FontConvert(string num, string subFont , Color fontColor)
    {
        this.gameObject.SetActive(true);

        Mainhyphen.GetComponent<Image>().color = fontColor;

        for (int i = 0; i < MainFont.Length; i++)
        {
            MainFont[i].SetActive(true);
            MainFont[i].GetComponent<Image>().color = fontColor;
          
        }

        for (int i = 0; i < MainFont2.Length; i++)
        {
            MainFont2[i].SetActive(true);
            MainFont2[i].GetComponent<Image>().color = fontColor;
            
        }

        FontConvert(num, null);
    }
}

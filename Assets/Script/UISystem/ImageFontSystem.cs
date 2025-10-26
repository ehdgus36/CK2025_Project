using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageFontSystem : MonoBehaviour
{
    [SerializeField] GameObject[] MainFont;
    [SerializeField] GameObject[] MainFont2;
    [SerializeField] Image Hyphen;

    [SerializeField] float speed;


    [SerializeField ]Image mainFont;
    [SerializeField] Image mainFont2;

    Vector3 StartPos = Vector3.zero;
    IEnumerator DelayDisable()
    {
        Color Alpha = Hyphen.color;

        for (int i = 0; i < 60; i++)
        {
            if (Hyphen != null)
                Hyphen.color = Alpha;


            if (mainFont != null)
                mainFont.color = Alpha;

            if (mainFont2 != null)
                mainFont2.color = Alpha;


            Alpha.a -= 1.0f / 60;
            transform.position += new Vector3(0, speed /60f, 0);
            yield return new WaitForSeconds(1.0f / 60);
        }

        Alpha.a = 1.0f;

        if (mainFont != null)
            mainFont.color = Alpha;

        if(mainFont2 != null)
            mainFont2.color = Alpha;


        Hyphen.color = Alpha;


        mainFont?.gameObject.SetActive(false);
        mainFont2?.gameObject.SetActive(false);
        Hyphen.gameObject.SetActive(false);

        this.transform.position = StartPos;
    }

    public void FontConvert(string num)
    {
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);

        

        Hyphen.gameObject.SetActive(false);
        if (StartPos.x == Vector3.zero.x)
        {
            StartPos = this.transform.position;
        }

        this.transform.position = StartPos;


        for (int i = 0; i < MainFont.Length;i++)
        {
            MainFont[i].SetActive(false);
           
        }

        for (int i = 0; i < MainFont2.Length; i++)
        {
            MainFont2[i].SetActive(false);
            
        }


        Hyphen.gameObject.SetActive(true);

        if (num != null)
        {
            if (num.Length == 1)
            {
                int index = int.Parse(num);
                mainFont = MainFont[index].gameObject.GetComponent<Image>();
                MainFont[index].SetActive(true);
            }

            if (num.Length == 2)
            {
                string f = num[0].ToString();
                
                string b = num[1].ToString();

                int index = int.Parse(f);
                MainFont[index].SetActive(true);
                mainFont = MainFont[index].gameObject.GetComponent<Image>();
                index = int.Parse(b);
                MainFont2[index].SetActive(true);
                mainFont2 = MainFont2[index].gameObject.GetComponent<Image>();
            }

        }

        StartCoroutine(DelayDisable());
        
    }

}

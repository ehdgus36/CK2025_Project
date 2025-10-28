using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class NoteSystemBar : MonoBehaviour
{

    [SerializeField] SpriteRenderer Good;
    [SerializeField] SpriteRenderer Normal;
    [SerializeField] SpriteRenderer Bad;
    [SerializeField] SpriteRenderer BG;
    [SerializeField] SpriteRenderer Note;
    [SerializeField] float speed;

    [SerializeField] string _Verdict;

    public string Verdict 
    {
        get
        {
            return _Verdict;
        }
    }

    [SerializeField] bool isTrun = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

     void Initialize()
     {

        float posX = Random.Range(BG.bounds.min.x + 1, BG.bounds.max.x) - 1;
        Good.transform.position = new Vector2(posX, BG.transform.position.y);
        Normal.transform.position = new Vector2(posX, BG.transform.position.y);
        Bad.transform.position = new Vector2(posX, BG.transform.position.y);
        isTrun = false;
        _Verdict = "";

     }

  

    public void PlayNote()
    {
        Initialize();
       
        StartCoroutine("PlayNoteSystem");
    }
    IEnumerator PlayNoteSystem()
    {
        while (true)
        {

            if (Input.GetKey(KeyCode.Space))
            {
                break;
            }

            if (isTrun == false)
            {
                Note.transform.position = Vector2.MoveTowards(Note.transform.position, new Vector2(BG.bounds.max.x, BG.transform.position.y), speed * Time.deltaTime);
            }

            if (isTrun == true)
            {
                Note.transform.position = Vector2.MoveTowards(Note.transform.position, new Vector2(BG.bounds.min.x, BG.transform.position.y), speed * Time.deltaTime);
            }

            if (Note.transform.position.x >= BG.bounds.max.x)
            {
                isTrun = true;
            }

            if (Note.transform.position.x <= BG.bounds.min.x)
            {
                isTrun = false;
            }

            yield return new WaitForSeconds(0.0166666666666667f); // 60프레임
        }
        //반복문 나와서


        //Good판정
        if (Good.bounds.min.x <= Note.transform.position.x && Good.bounds.max.x >= Note.transform.position.x)
        {
            _Verdict = "Good";

            yield break;
        }
        //Normal판정
        else if (Normal.bounds.min.x <= Note.transform.position.x && Normal.bounds.max.x >= Note.transform.position.x)
        {
            _Verdict = "Normal";

            yield break;
        }
        //Bad판정
        else if (Bad.bounds.min.x <= Note.transform.position.x && Bad.bounds.max.x >= Note.transform.position.x)
        {
            _Verdict = "Bad";

            yield break;
        }

        //Miss판정
        else if (Bad.bounds.min.x > Note.transform.position.x || Bad.bounds.max.x < Note.transform.position.x)
        {
            _Verdict = "Miss";

            yield break;
        }





    }

}

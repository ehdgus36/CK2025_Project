using UnityEngine;
using System.Collections;

public class NoteSystemManager : MonoBehaviour
{
    [SerializeField] NoteSystem[] NoteSystems;
    [SerializeField] int currentindex = 0;
    public void Start()
    {
        StartCoroutine("StartSystem");
    }

    IEnumerator StartSystem()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i<  NoteSystems.Length; i++)
        {
            NoteSystems[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }


        while (true) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NoteSystems[currentindex].isTrigger = true;
                currentindex++;
            }
            if (currentindex == NoteSystems.Length)
            {
                break;
            }

            yield return new WaitForSeconds(0.016f);
        }
        yield return null;
    }

}

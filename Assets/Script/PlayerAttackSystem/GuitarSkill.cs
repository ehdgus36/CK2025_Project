using System.Collections.Generic;
using UnityEngine;

public class GuitarSkill : MonoBehaviour
{
    [SerializeField] Transform[] Pos;
    [SerializeField] GameObject[] NotePrefab;
    [SerializeField] SpriteRenderer HitBox;

    [SerializeField] float BPM;
    [SerializeField] List<Note> Notes;
    double CurrentTime;

    [SerializeField] int Score = 0;
    public void Update()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime >= 60d / BPM)
        {

            Note note_s = Instantiate(NotePrefab[Random.Range(0, NotePrefab.Length)], Pos[Random.Range(0, Pos.Length)].transform.position , transform.rotation).GetComponent<Note>();
            CurrentTime -= 60d / BPM;
            Notes.Add(note_s);
        }




        if (Notes.Count != 0)
        {
            if (Notes[0].transform.position.x > HitBox.bounds.max.x)
            {
                Notes[0].gameObject.SetActive(false);
                Notes.RemoveAt(0);
            }

            if (Input.GetKeyDown(Notes[0].key))
            {
                if (Notes[0].transform.position.x >= HitBox.bounds.min.x && Notes[0].transform.position.x <= HitBox.bounds.max.x)
                {
                    Notes[0].gameObject.SetActive(false);
                    Notes.RemoveAt(0);

                    Score++;
                }
                else if (Notes[0].transform.position.x < HitBox.bounds.min.x && Notes[0].transform.position.x <= HitBox.bounds.max.x)
                {
                    Notes[0].gameObject.SetActive(false);
                    Notes.RemoveAt(0);
                }


            }
        }
       
    }
}

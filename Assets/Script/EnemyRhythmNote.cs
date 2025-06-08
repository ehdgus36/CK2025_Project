using UnityEngine;

public class EnemyRhythmNote : MonoBehaviour
{
    [SerializeField] float NoteSpeed = 5;
   

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.down * NoteSpeed * Time.deltaTime;
    }
}

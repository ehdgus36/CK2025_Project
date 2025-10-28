using UnityEngine;

public class EnemyRhythmNote : MonoBehaviour
{
    float NoteSpeed = 0.7f *1.5f;
   

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.down * NoteSpeed * Time.deltaTime;
    }
}

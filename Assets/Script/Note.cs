using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] float NoteSpeed = 5;
    [SerializeField] public KeyCode key;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.right * NoteSpeed * Time.deltaTime;
    }
}

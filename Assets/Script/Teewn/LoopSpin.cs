using System.Collections;
using UnityEngine;

public class LoopSpin : MonoBehaviour
{
    [SerializeField] float Speed;

    bool Loop = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Play();
    }

    public void Play()
    {
        Loop = true;
        StartCoroutine("Spin");
    }

    public void Stop()
    {
        Loop = false;
        StopCoroutine("Spin");

        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    IEnumerator Spin()
    {
        while (Loop)
        {
            transform.Rotate(0, 0, Speed);
            yield return new WaitForSeconds(0.05f);
        }
    }
}

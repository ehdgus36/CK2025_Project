using System.Collections;
using UnityEngine;

public class LoopSpin : MonoBehaviour
{
    [SerializeField] float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("Spin");
    }

    IEnumerator Spin()
    {
        while (true)
        {
            transform.Rotate(0, 0, Speed);
            yield return new WaitForSeconds(0.05f);
        }
    }
}

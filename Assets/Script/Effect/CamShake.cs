using UnityEngine;
using System.Collections;

public class CamShake : MonoBehaviour
{
    [SerializeField]
    private float m_roughness;      //거칠기 정도
    [SerializeField]
    private float m_magnitude;      //움직임 범위

    public void PlayShake()
    {
        StartCoroutine(Shake(.3f));
    }

    public void Update()
    {
   
    }


    IEnumerator Shake(float duration)
    {
        float halfDuration = duration / 2;
        float elapsed = 0f;
        float tick = Random.Range(-10f, 10f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime / halfDuration;

            tick += Time.deltaTime * m_roughness;
            transform.position = new Vector3(
                Mathf.PerlinNoise(tick, 0) - .5f * m_magnitude * Mathf.PingPong(elapsed, halfDuration),
                Mathf.PerlinNoise(0, tick) - .5f * m_magnitude * Mathf.PingPong(elapsed, halfDuration),
                -13f);

            yield return null;
        }

        transform.position = new Vector3(0, 0, -13.0f);
    }
}

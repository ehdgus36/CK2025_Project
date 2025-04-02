using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BomBom : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)] float Speed;
    [SerializeField] Vector2 BomSize;
    Vector2 StartSize;
    bool Loop = true;

    [SerializeField]bool isScale = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Play();
    }

    public void Play()
    {
        StartSize = transform.localScale;
        Loop = true;
        StartCoroutine("BOM");
    }

    public void Stop()
    {
        Loop = false;
        StopCoroutine("BOM");

        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    IEnumerator BOM()
    {
        while (Loop)
        {
            yield return new WaitForSeconds(0.05f);

            if (this.transform.localScale.x >= BomSize.x) isScale = false; // 다 커지면 작아지게

            if (this.transform.localScale.x <= StartSize.x) isScale = true; // 다 작아지면 다시 커지게

            if (isScale == false)  //작아짐
            {
                transform.localScale = Vector2.MoveTowards(transform.localScale, StartSize, Speed/10.0f);
                continue;
            }

            if (isScale == true)  //커짐
            {
                transform.localScale = Vector2.MoveTowards(transform.localScale, BomSize, Speed / 10.0f);
                continue;
            }

            
           

            
        }
    }
}

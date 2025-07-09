using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class CardArrowSystem : MonoBehaviour
{


    [SerializeField]List<Transform> CurveDotObj;

    [SerializeField] Transform CardPos;
    [SerializeField] Transform P01;
    [SerializeField] Transform Arrow;

    [SerializeField] Canvas canvas;

    Coroutine UpdateArrow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        UpdateArrow =StartCoroutine(UpdateArrowPos());  
       
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateArrow);
    }

    IEnumerator UpdateArrowPos()
    {
        while (true)
        {
            Vector2 localPoint;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                Input.mousePosition,
                canvas.renderMode == RenderMode.ScreenSpaceCamera ? canvas.worldCamera : null,
                out localPoint
            );

            Arrow.localPosition = localPoint;
            Vector2 direction = Arrow.position - P01.position;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            Arrow.rotation = Quaternion.Euler(0, 0, -angle);

            BezierCurveDrowArrow();
            yield return new WaitForSeconds(.05f);
        }
    }

    void BezierCurveDrowArrow()
    {
        float curve_t = 1.0f / CurveDotObj.Count;

        float t = 0;
        for (int i = 0; i < CurveDotObj.Count; i++)
        {
            Vector3 dotPos =  BezierCurve(CardPos.position, P01.position, Arrow.position, t);
            CurveDotObj[i].transform.position = dotPos;
            t += curve_t;
        }
       
    }

    Vector3 BezierCurve(Vector3 P0, Vector3 P1, Vector3 P2, float t) // 커브의 포지션을 반환
    {
        Vector3 M0 = Vector3.Lerp(P0, P1, t);
        Vector3 M1 = Vector3.Lerp(P1, P2, t);

        Vector3 B0 = Vector3.Lerp(M0, M1, t);

        return B0;
    }
}

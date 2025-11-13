using UnityEngine;

public class CustomMouse : MonoBehaviour
{

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // 카메라 거리
        transform.position = cam.ScreenToWorldPoint(mousePos);
    }
}

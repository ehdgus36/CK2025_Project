using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MakerNote : MonoBehaviour, IPointerDownHandler
{
    Image MakerNoteimage;

    public int code = 0;
    public RhythmGameMaker gameMaker;
    [SerializeField] bool IsNote = true;
    
   
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsNote) return; 

        if (MakerNoteimage = GetComponent<Image>())
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    MakerNoteimage.color = Color.blue;
                    code = 1;
                    break;

                case PointerEventData.InputButton.Right:
                    Debug.Log("오른쪽 마우스 버튼 눌림");
                    MakerNoteimage.color = Color.green;
                    code = 2;
                    break;

                case PointerEventData.InputButton.Middle:

                    MakerNoteimage.color = Color.white;
                    code = 0;
                    Debug.Log("가운데(휠) 버튼 눌림");
                    break;
            }
        }

        gameMaker.ResetCode();
    }
}

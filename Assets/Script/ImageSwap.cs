using UnityEngine;

public class ImageSwap : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteRenderer;
    [SerializeField] Sprite SwapImage;

    public void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartSwapImage();
        }
    }

    public void StartSwapImage()
    { 
        SpriteRenderer.sprite = SwapImage;
    }
}

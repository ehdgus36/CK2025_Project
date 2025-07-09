using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClearSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinText;

    private void OnEnable()
    {
        CoinText.text = GameManager.instance?.GetClearGold.ToString();
    }
    public void LoadMap()
    {
        SceneManager.LoadScene("GameMap");
    }
}

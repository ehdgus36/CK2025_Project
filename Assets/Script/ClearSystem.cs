using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClearSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinText;
    [SerializeField] string LoadScene = "GameMap";
    private void OnEnable()
    {
        CoinText.text = GameManager.instance?.GetClearGold.ToString();
    }
    public void LoadMap()
    {
        SceneManager.LoadScene(LoadScene);
    }
}

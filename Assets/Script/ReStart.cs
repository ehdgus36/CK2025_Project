using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{
    public void ReStartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

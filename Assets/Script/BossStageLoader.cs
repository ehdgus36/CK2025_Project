using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStageLoader : MonoBehaviour
{
    public void BossStageLoad()
    {

        SceneManager.LoadScene("BossStage");
    }
}

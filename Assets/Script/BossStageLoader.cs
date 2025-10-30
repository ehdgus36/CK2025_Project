using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStageLoader : MonoBehaviour
{
    [SerializeField] string SceneID = "BossStage";

    public void BossStageLoad()
    {

        SceneManager.LoadScene(SceneID);
    }
}

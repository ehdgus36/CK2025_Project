using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStageLoader : MonoBehaviour
{
    [SerializeField] string SceneID;

    public void BossStageLoad()
    {

        SceneManager.LoadScene(SceneID);
    }
}

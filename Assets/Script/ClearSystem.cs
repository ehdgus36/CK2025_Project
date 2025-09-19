using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;
using TMPro;

public class ClearSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinText;
    [SerializeField] string LoadScene = "GameMap";

    [SerializeField] GameObject ClearView;
    [SerializeField] GameObject UpgradeView;


    private void OnEnable()
    {
        if (ClearView == null && UpgradeView == null) return;

        CoinText.text = GameManager.instance?.GetClearGold.ToString();
        ClearView?.SetActive(false);
        UpgradeView?.SetActive(false);


        
        StartCoroutine(ClearSequence());
    }


    IEnumerator ClearSequence()
    {
        UpgradeView?.SetActive(true);
        yield return new WaitUntil(() => UpgradeView.activeSelf == false);
        ClearView?.SetActive(true);
    }

    public void LoadMap()
    {
        SceneManager.LoadScene(LoadScene);
    }
}

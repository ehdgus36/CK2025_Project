using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;
using TMPro;
using System;

public class ClearSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinText;
    [SerializeField] TextMeshProUGUI StageText;
    [SerializeField] string LoadScene = "GameMap";

    [SerializeField] GameObject ClearView;
    [SerializeField] GameObject UpgradeView;


    private void OnEnable()
    {
        if (ClearView == null && UpgradeView == null) return;

        CoinText.text = GameManager.instance?.GetClearGold.ToString();

        String stageData = "";
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<String>(GameDataSystem.KeyCode.DynamicGameDataKeys.STAGE_DATA, out stageData);

        StageText.text = stageData;

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

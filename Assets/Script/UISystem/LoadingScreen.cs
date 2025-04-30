using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingUI;
    public Slider progressBar;
    public Text progressText;

    public void LoadScene(string sceneName)
    {
        loadingUI.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float timer = 0f;

        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                progressBar.value = op.progress;
                progressText.text = Mathf.RoundToInt(op.progress * 100f) + " %";
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                progressText.text = "99 %";

                if (progressBar.value >= 1f)
                {
                   
                    
                  
                    op.allowSceneActivation = true;


                    yield return new WaitForSeconds(2);
                    progressText.text = "100 %";

                    yield break;
                }
            }
        }


       

    }
}

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System;
public class Tutorial : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] String LoadSceneName;
    Image tutorialImg;

    int index = 0;

    private void Start()
    {
        if (tutorialImg == null)
        {
            tutorialImg = GetComponent<Image>();
        }

        tutorialImg.sprite = sprites[index];
    }

    public void nextTutorial()
    {
        index++;

        if (index == sprites.Length)
        {
            
            FindFirstObjectByType<LoadingScreen>().LoadScene(LoadSceneName);
            return;
        }

        tutorialImg.sprite = sprites[index];
    }
}

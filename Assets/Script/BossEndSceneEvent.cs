using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(VideoPlayer))]
public class BossEndSceneEvent : MonoBehaviour
{
   VideoPlayer BossCutScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BossCutScene = GetComponent<VideoPlayer>();
        BossCutScene.loopPointReached += (vp) => { SceneManager.LoadScene("GameMap"); };
    }

   

}

using UnityEngine;
using System;
using TMPro;
using UnityEngine.Networking;
using System.Collections;


[System.Serializable]
public class PlayData
{
    public string playerName;
    public string playTime;
}
public class PlayTimeUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PlayTimeText;
    [SerializeField] TMP_InputField playerName;

    private void Start()
    {
        var elapsed = DateTime.Now - GameDataSystem.DynamicGameDataSchema.PlayTime;
        PlayTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
          elapsed.Hours,
          elapsed.Minutes,
          elapsed.Seconds);

       
    }

    public void UploadData()
    {
        UploadPlayTime(playerName.text, PlayTimeText.text);
    }


    public string webAppUrl = "https://script.google.com/macros/s/AKfycbwBy0XBiOqHc-na5Fjmg7kmf7ZjXAD1xSU8O8xz47IayE1hqtkwCwZR_YTpoWEOo0pj/exec";

    public void UploadPlayTime(string playerName, string playTime)
    {
        StartCoroutine(PostData(playerName, playTime));
    }

    IEnumerator PostData(string playerName, string playTime)
    {
        PlayData data = new PlayData()
        {
            playerName = playerName,
            playTime = playTime
        };


        var json = JsonUtility.ToJson(data);
        UnityWebRequest request = new UnityWebRequest(webAppUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            Debug.Log("업로드 성공!");
        else
            Debug.LogError("업로드 실패: " + request.error);
    }
}

using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;

public class PlayTimeUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PlayTimeText;
    [SerializeField] TMP_InputField playerName;

    // API Gateway URL
    public string apiUrl = "https://ma096lhw8i.execute-api.ap-northeast-2.amazonaws.com/prod/rank";


    private void Start()
    { 
        var elapsed = DateTime.Now - GameDataSystem.DynamicGameDataSchema.PlayTime;
        PlayTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);
    }

    public void UploadData()
    {
        StartCoroutine(UploadPlayTime(playerName.text, PlayTimeText.text));
    }

    IEnumerator UploadPlayTime(string user, string time)
    {
        string url = "https://ma096lhw8i.execute-api.ap-northeast-2.amazonaws.com/prod/rank";

        RankData data = new RankData( "aaaa", "aaaaaadfdff");

        string json = JsonUtility.ToJson(data);
        byte[] body = Encoding.UTF8.GetBytes(json);

        UnityWebRequest req = new UnityWebRequest(url, "POST");
        req.uploadHandler = new UploadHandlerRaw(body);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("Sending JSON: " + json);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Upload 실패: " + req.error);
        }
        else
        {
            Debug.Log("Upload 성공: " + req.downloadHandler.text);
        }

        Debug.Log("Sending JSON: " + json);
    }
}

[System.Serializable]
public class RankData
{
    public string UserName;
    public string PlayTime;

    public RankData(string name, string time)
    {
        this.UserName = name;
        this.PlayTime = time;
    }
}

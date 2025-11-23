using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;



public class CSVDownLoader : MonoBehaviour
{
    private const string DeffultURL = "https://docs.google.com/spreadsheets/d/1RpGLto7vEkCxZXFkMZJgvgTofoX4ccSQFoRKTGL_W2s/export?format=csv&gid=613876710";
    private static string sheetUrl;

    private static string fileName = "DownLoadCSVDataTable.csv";
    private static string saveFolder = "Assets/Resources/DataTable";


    [SerializeField] GameObject DownLoadTextObj;

    public void DataTableDownLoadButton()
    {
        StartCoroutine(DownloadAndSaveCSV());

    }



    private IEnumerator DownloadAndSaveCSV()
    {
        DownLoadTextObj.SetActive(true);
        saveFolder = Path.Combine(Application.streamingAssetsPath, "DataTable");


        UnityWebRequest www = UnityWebRequest.Get(DeffultURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("? 다운로드 실패: " + www.error);
            yield break;
        }

        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);

        string fullPath = Path.Combine(saveFolder, fileName);
        File.WriteAllText(fullPath, www.downloadHandler.text);
        Debug.Log($"? CSV 저장 완료: {fullPath}");




        TextAsset DownLoadCSVDataTable = new TextAsset();
        string filePath = Path.Combine(saveFolder, "DownLoadCSVDataTable.csv");

        if (File.Exists(filePath))
        {
            DownLoadCSVDataTable = new TextAsset(File.ReadAllText(filePath));

        }
        else
        {
            Debug.LogError("파일 없음: " + filePath);
        }

        List<Dictionary<string, object>> DownLoad = CSVReader.Read(DownLoadCSVDataTable);
        for (int i = 0; i < DownLoad.Count; i++)
        {
            sheetUrl = DownLoad[i]["URL"].ToString();


            www = UnityWebRequest.Get(sheetUrl);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("? 다운로드 실패: " + www.error);
                yield break;
            }

            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            fullPath = Path.Combine(saveFolder, DownLoad[i]["TableName"].ToString() + ".csv");
            File.WriteAllText(fullPath, www.downloadHandler.text);
            Debug.Log($"? CSV 저장 완료: {fullPath}");



        }

        DownLoadTextObj.SetActive(false);
        //AssetDatabase.Refresh();
    }
}


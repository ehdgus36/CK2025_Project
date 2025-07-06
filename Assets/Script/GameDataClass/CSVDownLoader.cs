#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.Networking;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using System.Collections.Generic;

public class CSVDownloader : EditorWindow
{
    private const string DeffultURL = "https://docs.google.com/spreadsheets/d/1RpGLto7vEkCxZXFkMZJgvgTofoX4ccSQFoRKTGL_W2s/export?format=csv&gid=613876710";
    private string sheetUrl;
    
    private string fileName = "DownLoadCSVDataTable.csv";
    private string saveFolder = "Assets/Resources/DataTable";

    [MenuItem("Tools/Google Sheet CSV Downloader")]
    public static void ShowWindow()
    {
        GetWindow<CSVDownloader>("CSV Downloader");
    }

    private void OnGUI()
    {

        EditorGUILayout.LabelField("Google Sheet CSV URL", EditorStyles.boldLabel);
     

        if (GUILayout.Button("CSV 데이터 갱신"))
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(DownloadAndSaveCSV());
        }

    }

    private IEnumerator DownloadAndSaveCSV()
    {

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

        AssetDatabase.Refresh(); // 에셋 갱신

        TextAsset DownLoadCSVDataTable = Resources.Load<TextAsset>("DataTable/DownLoadCSVDataTable");


        List<Dictionary<string, object>> DownLoad = CSVReader.Read(DownLoadCSVDataTable);
        for (int i = 0;i < DownLoad.Count; i++)
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

            AssetDatabase.Refresh(); // 에셋 갱신

        }

    }
}
#endif

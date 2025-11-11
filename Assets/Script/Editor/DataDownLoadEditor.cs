using UnityEngine;
using UnityEditor;
using System.IO;
using System.IO.Compression;
using UnityEngine.Networking;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using System.Collections.Generic;

public class DataDownLoadEditor : EditorWindow
{
    private string zipUrl = "https://drive.google.com/uc?export=download&id=YOUR_FILE_ID";
    private string CardDatazipPath = "Assets/DownloadedFolder.zip";
    private string extractPath = "Assets/ExtractedFolder";


    private const string DeffultURL = "https://docs.google.com/spreadsheets/d/1RpGLto7vEkCxZXFkMZJgvgTofoX4ccSQFoRKTGL_W2s/export?format=csv&gid=613876710";
    private static string sheetUrl;

    private static string fileName = "DownLoadCSVDataTable.csv";
    private static string saveFolder = "Assets/Resources/DataTable";


    [MenuItem("Tools/Download & Setup Folder")]
    public static void ShowWindow()
    {
        GetWindow<DataDownLoadEditor>("Folder Downloader");
    }

    private void OnGUI()
    {
        GUILayout.Label("Google Drive Folder Downloader", EditorStyles.boldLabel);
        zipUrl = EditorGUILayout.TextField("ZIP Download URL", zipUrl);
        CardDatazipPath = EditorGUILayout.TextField("ZIP Save Path", CardDatazipPath);
        extractPath = EditorGUILayout.TextField("Extract Path", extractPath);

        if (GUILayout.Button("Card Image DownLoad"))
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(DownloadAndSetup());
        }

        if (GUILayout.Button("DataTable SetUp"))
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(DownloadAndSaveCSV());
        }
    }

    private IEnumerator DownloadAndSetup()
    {
        //경로 설정
        CardDatazipPath = Application.streamingAssetsPath;
        extractPath = Path.Combine(Application.streamingAssetsPath, "CardImage");

        // 다운로드
        UnityWebRequest www = UnityWebRequest.Get(zipUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Download failed: " + www.error);
            yield break;
        }

        File.WriteAllBytes(CardDatazipPath, www.downloadHandler.data);
        Debug.Log("ZIP Downloaded: " + CardDatazipPath);

        // 기존 폴더 삭제
        if (Directory.Exists(extractPath))
            Directory.Delete(extractPath, true);

        // 압축 해제
        ZipFile.ExtractToDirectory(CardDatazipPath, extractPath);
        Debug.Log("Extracted to: " + extractPath);

        // 스프라이트 싱글 모드 적용
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { extractPath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.spriteImportMode = SpriteImportMode.Single;
                importer.SaveAndReimport();
            }
        }

        Debug.Log("All sprites set to Single mode!");
        AssetDatabase.Refresh();
    }


    private IEnumerator DownloadAndSaveCSV()
    {
        
        saveFolder = Path.Combine( Application.streamingAssetsPath,"DataTable");


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

        AssetDatabase.Refresh();
    }
}




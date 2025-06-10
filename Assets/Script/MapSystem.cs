using UnityEngine;

struct MapIcon
{
    public string SceneName;
    public bool IsInto;
}
public class MapSystem : MonoBehaviour
{
    [SerializeField] LoadStage[] loadingScreens;

    static bool[] LoadingScreensSave;

    public void Start()
    {
        if (LoadingScreensSave == null)
        {
            LoadingScreensSave = new bool[loadingScreens.Length];

            for (int i = 0; i < loadingScreens.Length; i++)
            {
                LoadingScreensSave[i] = true;
            }
        }
        else
        {
            for (int i = 0; i < loadingScreens.Length; i++)
            {
                loadingScreens[i].isInto = LoadingScreensSave[i];
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
        
        }
    }
    public void UpdateData()
    {
        for (int i = 0; i < loadingScreens.Length; i++)
        {
            LoadingScreensSave[i] = loadingScreens[i].isInto;
        }

    }
}

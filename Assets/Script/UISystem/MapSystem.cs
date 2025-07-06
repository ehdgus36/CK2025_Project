using System.Text;

using UnityEngine;
using UnityEngine.UI;

struct MapIcon
{
    public string SceneName;
    public bool IsInto;
}
public class MapSystem : MonoBehaviour
{
    [SerializeField] LoadStage[] loadingScreens;


    [SerializeField]string Data;

    // 0 :이면 진입불가  1: 입장가능  2: 클리어한거

    const string MapKey = "MapSave";
    public void Start()
    {
        if (GameDataSystem.DynamicGameDataSchema.DynamicDataBaseContainsKey(MapKey))
        {
            string SceneSaveData = "";
            GameDataSystem.DynamicGameDataSchema.LoadDynamicData<string>(MapKey, out SceneSaveData);

            Data = SceneSaveData;

            for (int i = 0; i < SceneSaveData.Length; i++)
            {
                switch (SceneSaveData[i])
                {
                    case '0':
                        loadingScreens[i].state = StageState.LOCK;
                        break;
                    case '1':
                        loadingScreens[i].state = StageState.NULOCK;
                        break;
                    case '2':
                        loadingScreens[i].state = StageState.ClEAR;
                        break;
                }

                
            }

        }


        for (int i = 0; i < loadingScreens.Length; i++)
        {
            loadingScreens[i].SetUP();
        }
    }

    public void Update()
    {
        
    }
    public void UpdateData()
    {

    }

    public void Save()
    {
        string SAVEDATA = "";

        for (int i = 0; i < loadingScreens.Length; i++)
        {
            switch (loadingScreens[i].state)
            {
                case StageState.LOCK:
                    SAVEDATA += "0";
                    break;
                case StageState.NULOCK:
                    SAVEDATA += "1";
                    break;
                case StageState.ClEAR:
                    SAVEDATA += "2";
                    break;
            }

           
        }


        Data = SAVEDATA;

        if (GameDataSystem.DynamicGameDataSchema.DynamicDataBaseContainsKey(MapKey))
        {
            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(MapKey, Data);
        }
        else
        {
            GameDataSystem.DynamicGameDataSchema.AddDynamicDataBase(MapKey, SAVEDATA);
        }

       
    }
}

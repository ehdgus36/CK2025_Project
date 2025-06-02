using UnityEngine;
using UnityEngine.UI;
using GameDataSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;


/// <summary> 게임씬에 있는 UI를 관리하는 클래스 </summary>

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _UseCardCountText;
    [SerializeField] GameObject _Black;


    public TextMeshProUGUI UseCardCountText { get { return _UseCardCountText; } }
    public GameObject Black { get { return _Black; } }


    List<DynamicUIObject> GameUIObj;
    [SerializeField] bool isStart;


    private void Start()
    {
        if (isStart)
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        GameDataSystem.DynamicGameDataSchema.RemoveDynamicUIDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA);

        DynamicUIObject[] dynamicUIObjects = FindObjectsByType<DynamicUIObject>(FindObjectsSortMode.InstanceID);

        if (dynamicUIObjects.Length != 0)
        {
            for (int i = 0; i < dynamicUIObjects.Length; i++)
            {
                GameUIObj.Add(dynamicUIObjects[i]);
                DynamicGameDataSchema.AddDynamicUIDataBase(dynamicUIObjects[i].DynamicDataKey, dynamicUIObjects[i]);
            }
        }

    }
}

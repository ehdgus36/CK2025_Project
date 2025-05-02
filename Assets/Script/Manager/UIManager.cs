using UnityEngine;
using UnityEngine.UI;
using GameDataSystem;
using System.Collections;
using System.Collections.Generic;


/// <summary> 게임씬에 있는 UI를 관리하는 클래스 </summary>

public class UIManager : MonoBehaviour
{
    [SerializeField] List<DynamicUIObject> GameUIObj;

    public void Initialize()
    {
        ///DynamicUIObject[] dynamicUIObjects = FindObjectsByType<DynamicUIObject>(FindObjectsSortMode.InstanceID);

        //if (dynamicUIObjects.Length != 0)
        //{
        //    //for (int i = 0; i < dynamicUIObjects.Length; i++)
        //    //{
        //    //    GameUIObj.Add(dynamicUIObjects[i]);
        //    //    DynamicGameDataSchema.AddDynamicUIDataBase(dynamicUIObjects[i].DynamicDataKey, dynamicUIObjects[i]);
        //    //}
        //}

    }
}

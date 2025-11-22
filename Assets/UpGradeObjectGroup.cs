using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UpGradeObjectGroup : MonoBehaviour
{
    [SerializeField] CardUpGradeSystem[] upGradeSystem;

    private void Start()
    {

        List<string> DackData = new List<string>();
        

        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<List<string>>(GameDataSystem.KeyCode.DynamicGameDataKeys.DACK_DATA, out DackData);


        


        for (int i = 0; i < upGradeSystem.Length; i++)
        {
            upGradeSystem[i].SetUp(new List<string>(DackData));
        }
    }
}

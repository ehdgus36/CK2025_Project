using UnityEngine;
using UnityEngine.Playables;

public class PlayerSkillSystem : MonoBehaviour
{
    [SerializeField] PlayableDirector SkillCutScene;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            SkillCutScene.Play();

            int GradeData = 0;
            if (GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.UPGRADE_POINT_DATA, out GradeData))
            {

                if (GradeData >= 100)
                {
                    SkillCutScene.Play();
                }
            }
            
        }
        
    }
}

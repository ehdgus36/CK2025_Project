using Spine.Unity;
using UnityEngine;

public class KPOP_BackGround : MonoBehaviour
{
    [SerializeField] MeshRenderer NPC;
    [SerializeField] MeshRenderer Player;

    Player PlayerUnit;
    Enemy Boss;


    void Start()
    {
        NPC.sortingOrder = 1;
        Player.sortingOrder = 0;

        PlayerUnit = GameManager.instance.Player;
        Boss = GameManager.instance.EnemysGroup.Enemys[0];
       
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerUnit == null)
        {
            PlayerUnit = GameManager.instance.Player;
        }

        if (Boss == null)
        {
            Boss = GameManager.instance.EnemysGroup.Enemys[0];
        }

        // 플레이어가 비율이 높을때
        if (((float)PlayerUnit.PlayerUnitData.CurrentHp / (float)PlayerUnit.PlayerUnitData.MaxHp) > ((float)Boss.EnemyData.EnemyUnitData.CurrentHp / (float)Boss.EnemyData.EnemyUnitData.MaxHp))
        {       
            NPC.sortingOrder = 0;
            Player.sortingOrder = 1;
        }
        else if (((float)PlayerUnit.PlayerUnitData.CurrentHp / (float)PlayerUnit.PlayerUnitData.MaxHp) <= ((float)Boss.EnemyData.EnemyUnitData.CurrentHp / (float)Boss.EnemyData.EnemyUnitData.MaxHp))
        {
            NPC.sortingOrder = 1;
            Player.sortingOrder = 0;
        }
    }

  
}

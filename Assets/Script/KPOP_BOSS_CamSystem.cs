using Unity.Cinemachine;
using UnityEngine;

public class KPOP_BOSS_CamSystem : MonoBehaviour
{
    CinemachineBrain brain;

    public CinemachineCamera Player;
    public CinemachineCamera Boss;

    public void SwitchToBoss()
    {
        if (Boss.Priority == 20) return; // 이미 보스 화면이면 안함

        Boss.Priority = 20;
        Player.Priority = 10;
    }

    public void SwitchToPlayer()
    {
        if (Player.Priority == 20) return; // 이미 보스 화면이면 안함

        Boss.Priority = 10;
        Player.Priority = 20;
    }
    void Start()
    {
        brain = GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (GameManager.instance.GetThisTurnUnit is Player)
        {
            SwitchToPlayer();
        }
        if (GameManager.instance.GetThisTurnUnit is EnemysGroup)
        {
            SwitchToBoss();
        }

    }
}

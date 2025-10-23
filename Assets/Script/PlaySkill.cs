using UnityEngine;

public class PlaySkill : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] Transform SkillPos;
    public void SkillAttack()
    {
        GameManager.instance.EnemysGroup.Enemys[0].TakeDamage(GameManager.instance.Player,30);
        Debug.Log(GameManager.instance.EnemysGroup.Enemys[0].name + "skilllsfadsfadsf");
    }

    public void EndSkillAttack()
    {

        GameManager.instance.Player.gameObject.transform.position = startPos;
       
    }
}

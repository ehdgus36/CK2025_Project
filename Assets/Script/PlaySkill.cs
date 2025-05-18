using UnityEngine;

public class PlaySkill : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] Transform SkillPos;
    public void SkillAttack()
    {
        startPos =  GameManager.instance.Player.gameObject.transform.position;
        GameManager.instance.Player.PlayerAnimator.Play("ultimate");
        GameManager.instance.Player.gameObject.transform.position = SkillPos.position;
    }

    public void EndSkillAttack()
    {

        GameManager.instance.Player.gameObject.transform.position = startPos;
       
    }
}

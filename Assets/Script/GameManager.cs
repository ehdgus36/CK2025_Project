using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Enemy enemy;

    [SerializeField] GameObject EnemyDamageEffect;
    //현재 턴
    [SerializeField] string ThisTurn = "player";

    //플레이어 기능 비활성화, 스와이프 카드 홀드
    // Start is called before the first frame update

    public static GameManager instance;
    void Start()
    {
        if (instance == null) { instance = this; }
       // if (player == null || enemy == null) { return; }
        EnemyDamageEffect.SetActive(false);

       // player.gameManager = this;
       // enemy.gameManager = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwichTurn()
    {
    
    }

    public void AttackDamage(int damage)
    {
        EnemyDamageEffect.SetActive(true);
    }
}

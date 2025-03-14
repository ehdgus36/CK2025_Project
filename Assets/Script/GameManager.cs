using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Unit player;
    [SerializeField] Unit enemy;

    [SerializeField] GameObject EnemyDamageEffect;
    //현재 턴
    [SerializeField] Unit ThisTurnUnit;
    [SerializeField] Unit NextTurnUnit;

    [SerializeField] Dack CardDack;

    //플레이어 기능 비활성화, 스와이프 카드 홀드
    // Start is called before the first frame update

    public static GameManager instance;
    void Start()
    {
        if (instance == null) 
        {
            instance = this; 
        }

        if (enemy == null) return;

        player = GameObject.FindObjectOfType<Player>();
        ThisTurnUnit = player;
        NextTurnUnit = enemy;

        ThisTurnUnit.StartTurn();
    }

   

    public void TurnSwap()
    {
        ThisTurnUnit.EndTurn(); //ThisTurnUnit이 변경전 EndTurn실행하여 마무리
        (ThisTurnUnit, NextTurnUnit) = (NextTurnUnit, ThisTurnUnit); //swap

        ThisTurnUnit.StartTurn(); //ThisTurnUnit이 변경후 StartTurn함수 실행
    }
    public void AttackDamage(int damage)
    {
        NextTurnUnit.TakeDamage(damage);// 현재Unit을 기준으로 다음 Unit에게 데미지를 줌
    }


    public void CardDrow()
    { 
    
    }

    
}

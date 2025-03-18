using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Unit player;
    [SerializeField] Unit enemy;

    [SerializeField] GameObject EnemyDamageEffect;
    //현재 턴
    [SerializeField] Unit ThisTurnUnit;
    [SerializeField] Unit NextTurnUnit;

    [SerializeField] Dack CardDack;
    [SerializeField] SlotGroup PlayerSloats;
    [SerializeField] ChipAttackSystem PlayerAttackSystem;

    [SerializeField] Button TurnEndButton;


    //Manager

    [SerializeField] WaveManager WaveManager;
    //플레이어 기능 비활성화, 스와이프 카드 홀드
    // Start is called before the first frame update



    public static GameManager instance;

    public ChipAttackSystem GetPlayerAttackSystem() { return PlayerAttackSystem; }
  


     void Initialize()
    {
        if (enemy == null) return;

        player = FindFirstObjectByType<Player>();
        InitializeTurn();


        TurnEndButton.onClick.AddListener(TurnSwap);
    }


    public void InitializeTurn()
    {
        ThisTurnUnit = player;
        NextTurnUnit = enemy;

        ThisTurnUnit.InitTurnCount();
        NextTurnUnit.InitTurnCount();

        ThisTurnUnit.StartTurn();
    }
      
    void Start()
    {
        if (instance == null) 
        {
            instance = this; 
        }
        Initialize();

    }

   

    public void TurnSwap()
    {
        if (PlayerAttackSystem == null) return;
        if (PlayerAttackSystem.GetIsCard() == true) return;

        ThisTurnUnit.EndTurn(); //ThisTurnUnit이 변경전 EndTurn실행하여 마무리
        (ThisTurnUnit, NextTurnUnit) = (NextTurnUnit, ThisTurnUnit); //swap

        ThisTurnUnit.StartTurn(); //ThisTurnUnit이 변경후 StartTurn함수 실행
    }
    public void AttackDamage(int damage)
    {
        NextTurnUnit.TakeDamage(damage);// 현재Unit을 기준으로 다음 Unit에게 데미지를 줌
    }


    public void PlayerCardDrow()
    {
        SlotUI[] playerCardSlots = PlayerSloats.Getsloat();
        List<Card> playerCard = CardDack.CardDrow(playerCardSlots.Length);

        for (int i = 0; i < playerCardSlots.Length; i++)
        {
            playerCardSlots[i].InsertData(playerCard[i].gameObject);
        }
    }


    //플레이어 턴 종료시 남은 카드 덱으로 도려주기
    public void PlayerCardReturn()
    {
        List<GameObject> playerCard = PlayerSloats.ReadData();
        for (int i = 0; i < playerCard.Count; i++)
        {
            if (playerCard[i].GetComponent<Card>())
            {
                CardDack.InsertCard(playerCard[i].GetComponent<Card>());
            }
        }
      
    }

    public void NextWave()
    {
        if (ThisTurnUnit == player)
        {
            TurnSwap();
        }
        WaveManager.NextWave();
    }
   
}

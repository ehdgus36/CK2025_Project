using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player Player;
    [SerializeField] EnemysGroup Enemy;

    [SerializeField] GameObject EnemyDamageEffect;
    //현재 턴
    [SerializeField] Unit ThisTurnUnit;
    [SerializeField] Unit NextTurnUnit;

    [SerializeField] Dack CardDack;
    [SerializeField] SlotGroup PlayerCardSloats;
    [SerializeField] CardMixtureSystem PlayerAttackSystem;

    [SerializeField] Button TurnEndButton;
    [SerializeField] public GameObject GameClear;
    [SerializeField] public GameObject GameOver;


    //Manager

    [SerializeField] WaveManager WaveManager;
    [SerializeField] HpManager HpManager;
    [SerializeField] AttackManager AttackManager;
    [SerializeField] UIManager UIManager;
    [SerializeField]
    //플레이어 기능 비활성화, 스와이프 카드 홀드
    // Start is called before the first frame update



    public static GameManager instance;

    public void SetEnemy(EnemysGroup enemy) { Enemy = enemy; }
    public CardMixtureSystem GetPlayerAttackSystem() { return PlayerAttackSystem; }
    public Button GetTurnButton() { return TurnEndButton; }
    public HpManager GetHpManager() { return HpManager; }
    public AttackManager GetAttackManager() { return AttackManager; }

    public EnemysGroup GetEnemysGroup() {return Enemy; }

    public Player GetPlayer() { return Player; }
    
     void Initialize()
    {
        if (Enemy == null) return;

        Player = FindFirstObjectByType<Player>();
        InitializeTurn();

        if (WaveManager == null)
        {
            WaveManager = GetComponent<WaveManager>();
            WaveManager.Initialize();
        }
        else
        {
            WaveManager.Initialize();
        }

        if (HpManager == null)
        {
            HpManager = GetComponent<HpManager>();
            HpManager.Initialize();
        }
        else
        {
            HpManager.Initialize();
        }

        if (AttackManager == null)
        {
            AttackManager = GetComponent<AttackManager>();
            AttackManager.Initialize();
        }
        else
        {
            AttackManager.Initialize();
        }
        TurnEndButton.onClick.AddListener(TurnSwap);
        PlayerAttackSystem.Initialize();
    }


    public void InitializeTurn()
    {
        
        ThisTurnUnit = Player;
        NextTurnUnit = Enemy;

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


        ThisTurnUnit.EndTurn(); //ThisTurnUnit이 변경전 EndTurn실행하여 마무리
        (ThisTurnUnit, NextTurnUnit) = (NextTurnUnit, ThisTurnUnit); //swap

        ThisTurnUnit.StartTurn(); //ThisTurnUnit이 변경후 StartTurn함수 실행
    }
  

    public void PlayerCardDrow()
    {
        SlotUI[] playerCardSlots = PlayerCardSloats.Getsloat();
        List<Card> playerCard = CardDack.CardDrow(playerCardSlots.Length);

        for (int i = 0; i < playerCardSlots.Length; i++)
        {
           playerCardSlots[i].InsertData(playerCard[i].gameObject);
        }

        PlayerCardSloats.GetComponent<Animator>().Play("Drow");
    }


    //플레이어 턴 종료시 남은 카드 덱으로 돌려려주기
    public void PlayerCardReturn()
    {
        PlayerCardSloats.GetComponent<Animator>().Play("CardReturn");

        StartCoroutine("CardReturn");
    }
    IEnumerator CardReturn()
    { 
        yield return new WaitForSeconds(1f);
        List<Card> playerCard = PlayerCardSloats.ReadData<Card>();
        for (int i = 0; i < playerCard.Count; i++)
        {
            CardDack.InsertCard(playerCard[i].GetComponent<Card>());
        }
       
    }


    public void NextWave()
    {
        if (ThisTurnUnit == Player)
        {
            TurnSwap();
        }

        PlayerCardReturn();

        WaveManager.NextWave();
        HpManager.Initialize();

        
    }
   
}

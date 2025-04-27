using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq.Expressions;

public class GameManager : MonoBehaviour
{
    public Player Player { get { return _Player; } }
    [SerializeField] private Player _Player; // 인스펙터

    public EnemysGroup EnemysGroup{ get { return _EnemysGroup; } }
    [SerializeField] private EnemysGroup _EnemysGroup; // 인스팩터
    
    //현재 턴
    [SerializeField] Unit ThisTurnUnit;
    [SerializeField] Unit NextTurnUnit;

    public CardMixtureSystem PlayerAttackSystem { get { return _PlayerAttackSystem; } }
    [SerializeField] CardMixtureSystem _PlayerAttackSystem;


    [SerializeField] public GameObject GameClear;
    [SerializeField] public GameObject GameOver;


 


   
    [SerializeField] public AttackManager AttackManager { get; private set; }
    [SerializeField] public UIManager UIManager { get; private set; }

    //플레이어 기능 비활성화, 스와이프 카드 홀드
    // Start is called before the first frame update

    public static GameManager instance { get; private set; }

  
   
    
     void Initialize()
    {
        

        _Player = FindFirstObjectByType<Player>();
        _EnemysGroup = FindFirstObjectByType<EnemysGroup>();

        ThisTurnUnit = Player;
        NextTurnUnit = EnemysGroup;

        ThisTurnUnit.InitTurnCount();
        NextTurnUnit.InitTurnCount();


        ThisTurnUnit.StartTurn();


        if (AttackManager == null)
        {
            AttackManager = GetComponent<AttackManager>();
            AttackManager.Initialize();
        }
        else
        {
            AttackManager.Initialize();
        }

      
        _EnemysGroup?.Initialize();
        _Player?.Initialize();
        //enemygroup 초기화
        _PlayerAttackSystem?.Initialize();



        if (UIManager == null)
        {
            UIManager = GetComponent<UIManager>();
            UIManager.Initialize();
        }
        else
        {
            UIManager.Initialize();
        }
    }


    private void Awake()
    {
        if (instance == null) 
        {
            instance = this; 
        }
        Initialize();

    }

    public void ReStart(string sceneName)
    { 
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetInt("PlayerHP", 50);
        Player.addHP(100);
    }


    public void GameClearFun()
    {
        StartCoroutine(DeleyLoadScene());
    }

    IEnumerator DeleyLoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Title");
        Player.PlayerSave();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayerPrefs.SetInt("PlayerHP", 50);
            Player.addHP(100);
        }
    }

    public void TurnSwap()
    {     

        ThisTurnUnit.EndTurn(); //ThisTurnUnit이 변경전 EndTurn실행하여 마무리
        (ThisTurnUnit, NextTurnUnit) = (NextTurnUnit, ThisTurnUnit); //swap

        ThisTurnUnit.StartTurn(); //ThisTurnUnit이 변경후 StartTurn함수 실행
    }
  

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq.Expressions;

public class GameManager : MonoBehaviour
{
    //Private
    private Player _Player;

    private EnemysGroup _EnemysGroup;

    private Unit ThisTurnUnit;
    private Unit NextTurnUnit;

    private AudioSource BGMAudioSource;
    //Get; Set;

    public CardMixtureSystem PlayerAttackSystem { get { return _PlayerAttackSystem; } }
    public CamShake Shake { get { return _Shaker; } }
    public Player Player { get { return _Player; } }

    public EnemysGroup EnemysGroup
    {
        get
        {
            if (_EnemysGroup.Enemys.Count == 0)
                Debug.LogError("EnemysGroup의 Count값이 0 입니다 지정된 Enemy가 없습니다");

            return _EnemysGroup;
        }
    }




    //Public

    public static GameManager instance { get; private set; }
    public AttackManager AttackManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public MetronomeSystem Metronome { get; private set; }




    //인스펙터에서 데이터 받아옴

    [SerializeField] CardMixtureSystem _PlayerAttackSystem;
    [SerializeField] public GameObject GameClear;
    [SerializeField] public GameObject GameOver;
    [SerializeField] CamShake _Shaker;

    [SerializeField] GameObject PlayerTurnMark;
    [SerializeField] GameObject EnemyTurnMark;
    GameObject ThisTrunMark;
    GameObject NextTrunMark;

    IEnumerator Initialize()
     {
        _Player = FindFirstObjectByType<Player>();
        _EnemysGroup = FindFirstObjectByType<EnemysGroup>();

        ThisTurnUnit = Player;
        NextTurnUnit = EnemysGroup;

        ThisTrunMark = PlayerTurnMark;
        NextTrunMark = EnemyTurnMark;
       
        yield return null;

        if (BGMAudioSource == null)
        {
            BGMAudioSource = GetComponent<AudioSource>();
        }

        if (Metronome == null)
        {
            Metronome = GetComponent<MetronomeSystem>();        
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

        _EnemysGroup?.Initialize();
        _Player?.Initialize();

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

        yield return null;

        ThisTurnUnit.StartTurn();
        Metronome.AddOnceMetronomEvent(() => { BGMAudioSource.Play(); });
        StartCoroutine(TurnMark());
     }


    private void Awake()
    {
        if (instance == null) 
        {
            instance = this; 
        }
        StartCoroutine(Initialize());
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

        StartCoroutine(TurnMark());
    }

    IEnumerator TurnMark()
    {
        ThisTrunMark.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ThisTrunMark.SetActive(false);

        (ThisTrunMark, NextTrunMark) =  (NextTrunMark ,ThisTrunMark); // swap

    }

}

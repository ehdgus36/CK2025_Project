using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Private
    private Player _Player;

    private EnemysGroup _EnemysGroup;

    private Unit ThisTurnUnit;
    private Unit NextTurnUnit;

    public Animator UIAnime;
    //Get; Set;


    public Unit GetThisTurnUnit { get { return ThisTurnUnit; } }

    public CamShake Shake { get { return _Shaker; } }
    public Player Player { get { return _Player; } }

    public EnemysGroup EnemysGroup
    {
        get
        {
            if (_EnemysGroup.Enemys.Count == 0)
                Debug.Log("EnemysGroup의 Count값이 0 입니다 지정된 Enemy가 없습니다");

            return _EnemysGroup;
        }
    }




    //Public

    public static GameManager instance { get; private set; }

    public UIManager UIManager { get; private set; }
    public MetronomeSystem Metronome { get; private set; }

    public ExcutSelectCardSystem ExcutSelectCardSystem { get; private set; }

    public AbilitySystem AbilitySystem { get; private set; }

    //public GameObject PlayerCardSlot { get { return _CardSlot; } }

    public ItemDataLoader ItemDataLoader { get; private set; }

    public CardCastPlace PlayerCardCastPlace { get { return _PlayerCardCastPlace; } }

    public CemeteryUI CardCemetery { get { return _CardCemetery; } }

    public PlayerCDSlotGroup PlayerCDSlotGroup { get { return _PlayerCDSlotGroup; } }


    public PostProcessingSystem PostProcessingSystem { get { return _PostProcessingSystem; } }

    public FMODManagerSystem FMODManagerSystem { get { return _FMODManagerSystem; } }

    public DimBackGroundObject DimBackGroundObject { get { return _DimBackGroundObject; } }



    public void ExitGame()
    {
        Application.Quit();
    }

    //인스펙터에서 데이터 받아옴

    [SerializeField] CardCastPlace _PlayerCardCastPlace;

    [SerializeField] CemeteryUI _CardCemetery;
    [SerializeField] PlayerCDSlotGroup _PlayerCDSlotGroup;
    [SerializeField] GameObject GameClear;
    [SerializeField] GameObject GameOver;
    [SerializeField] CamShake _Shaker;

    // [SerializeField] GameObject _CardSlot;
    [SerializeField] GameObject PlayerTurnMark;
    [SerializeField] GameObject EnemyTurnMark;
    [SerializeField] GameObject GameStartMark;
    [SerializeField] Button EndTurnButton;

    [SerializeField] PostProcessingSystem _PostProcessingSystem;
    [SerializeField] FMODManagerSystem _FMODManagerSystem;
    [SerializeField] GameObject EventSystem;

    [SerializeField] Animator _ControlleCam;

    [SerializeField]int ClearGold = 50;

    [SerializeField] DimBackGroundObject _DimBackGroundObject;

    GameObject ThisTrunMark;
    GameObject NextTrunMark;


    public Animator ControlleCam => _ControlleCam;
    public int GetClearGold { get { return ClearGold; } }

    public Button GetEndTurnButton { get { return EndTurnButton; } }

    bool isStart = false; // 게임 처음 시작할 때("전투 시작 UI 표시") 표시

    bool isClear = false;
    IEnumerator Initialize()
    {
        
        ItemDataLoader = gameObject.GetComponent<ItemDataLoader>();

        ExcutSelectCardSystem = gameObject.GetComponent<ExcutSelectCardSystem>();
        ItemDataLoader?.LoadData();

        _Player = FindFirstObjectByType<Player>();
        _EnemysGroup = FindFirstObjectByType<EnemysGroup>();

        ThisTurnUnit = Player;
        NextTurnUnit = EnemysGroup;

        ThisTrunMark = PlayerTurnMark;
        NextTrunMark = EnemyTurnMark;


        AbilitySystem = new AbilitySystem();

        yield return null;
        UIAnime.Play("Active_UIAnimation");



        if (Metronome == null)
        {
            Metronome = GetComponent<MetronomeSystem>();        
        }


        _FMODManagerSystem?.Initialize();
        _PostProcessingSystem?.Initialized();
        _EnemysGroup?.Initialize();
        _Player?.Initialize();

       

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

        EndTurnButton?.onClick.AddListener(TurnSwap);
        EndTurnButton?.onClick.AddListener(() => { _FMODManagerSystem.PlayEffectSound("event:/UI/In_Game/Turnend_Button"); }); // 클릭시 사운드
        
        //EndTurnButton?.gameObject.SetActive(false);


        _PlayerCardCastPlace.Reset();
        ExcutSelectCardSystem.initialize();
        ThisTurnUnit.StartTurn();
        //Metronome.AddOnceMetronomEvent(() => { BGMAudioSource.Play(); });
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

    public void GameFail()
    {
        StartCoroutine(FaillDeleyLoadScene());
    }

    IEnumerator FaillDeleyLoadScene()
    {
        UIAnime.Play("Hide_UIAnimation");
        yield return new WaitForSeconds(.2f);
        _ControlleCam.Play("DieCamAnime");
        Player.PlayerAnimator.MainLayerPlayAnimation("Die_Ani");
        yield return new WaitForSeconds(1.5f);

        GameOver.SetActive(true);
        _FMODManagerSystem.PlayEffectSound("event:/UI/Fail_Stage");


        yield return new WaitForSeconds(.35f);
        Player.gameObject.SetActive(false);
    }


    public void GameClearFun()
    {
        if (isClear == true) return;

        isClear = true;
        GameDataSystem.StaticGameDataSchema.CARD_DATA_BASE.ResetTable();
        StartCoroutine(DeleyLoadScene());
    }

    IEnumerator DeleyLoadScene()
    {

        
        yield return new WaitForSeconds(1f);
        Player.transform.GetChild(0).transform.localPosition = Vector3.zero;
        UIAnime.Play("Hide_UIAnimation");
        _PlayerCDSlotGroup.gameObject.SetActive(false);

        UIManager.CardDescription.SetActive(false);

        yield return new WaitForSeconds(.2f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => PlayerCardCastPlace.isByeByeStart == false );



        int gold = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA,out gold);
        gold += ClearGold;


        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, gold);


        GameManager.instance.ControlleCam.Play("DieCamAnime");
        yield return new WaitForSeconds(.5f);
        FMODManagerSystem.PlayEffectSound("event:/Character/PC/Game_Clear");
        _Player.PlayerAnimator.MainLayerPlayAnimation("Win_Ani");

        yield return new WaitForSeconds(1.5f);

        FMODManagerSystem.FMODChangeClear();

        if (_Player.isDie == true) yield break;

        GameClear.SetActive(true);
        Player.PlayerSave();

        // 돈 입금기능 해야함
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayerPrefs.SetInt("PlayerHP", 50);
            Player.addHP(100);
        }
    }


    public void EndTurn()
    {
        EndTurnButton.gameObject.SetActive(false);
    }

    public void TurnSwap()
    {
        // 턴앤드 클릭시 TurnSwap함수 재생

        if (Player.isDie == true || isClear == true) return;

        EndTurn();
        Metronome.AddOnceMetronomX4Event(() =>
        {
            ThisTurnUnit.EndTurn(); //ThisTurnUnit이 변경전 EndTurn실행하여 마무리
            (ThisTurnUnit, NextTurnUnit) = (NextTurnUnit, ThisTurnUnit); //swap

            ThisTurnUnit.StartTurn(); //ThisTurnUnit이 변경후 StartTurn함수 실행

            if (ThisTurnUnit.GetType() == typeof(Player))
            {
                UIAnime.Play("Active_UIAnimation");
                _FMODManagerSystem.FMODChangePlayer();
               
            }

            if (ThisTurnUnit.GetType() == typeof(EnemysGroup))
            {
                UIAnime.Play("Hide_UIAnimation");
                _FMODManagerSystem.FMODChangeMonsterTurn();
            }

            StartCoroutine(TurnMark());
        });

       

    }

    IEnumerator TurnMark()
    {
        UIInputSetActive(false);
        if (isStart == false)
        {    
            isStart = true;
            yield return new WaitForSeconds(.2f);
            _FMODManagerSystem.PlayEffectSound("event:/UI/Game_Start"); // 사운드도 같이
            GameStartMark.SetActive(true);
            yield return new WaitForSeconds(1f);
            GameStartMark.SetActive(false);
        }


        yield return new WaitForSeconds(.2f);
        _FMODManagerSystem.PlayEffectSound("event:/UI/In_Game/Turn_Change"); // 사운드도 같이
        ThisTrunMark.SetActive(true);
        yield return new WaitForSeconds(1f);
        ThisTrunMark.SetActive(false);

        (ThisTrunMark, NextTrunMark) =  (NextTrunMark ,ThisTrunMark); // swap

        UIInputSetActive(true);

    }



    public void FailEvent()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    /// <summary>
    /// Combo의 수치를 업그레이드
    /// </summary>
    /// <param name="data"></param>
    public void ComboUpdate(int data)
    {
        //StartCoroutine(UPdateComboCount(data*2));
    }

 


    public void UIInputSetActive(bool active)
    {
       EventSystem.SetActive(active);
    }
}

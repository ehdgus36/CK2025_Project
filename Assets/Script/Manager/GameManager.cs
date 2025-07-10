using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq.Expressions;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    //Private
    private Player _Player;

    private EnemysGroup _EnemysGroup;

    private Unit ThisTurnUnit;
    private Unit NextTurnUnit;

   
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
   
    public UIManager UIManager { get; private set; }
    public MetronomeSystem Metronome { get; private set; }

    public ExcutSelectCardSystem ExcutSelectCardSystem { get; private set; }

    //public GameObject PlayerCardSlot { get { return _CardSlot; } }

    public ItemDataLoader ItemDataLoader { get; private set; }

    public CardCastPlace PlayerCardCastPlace { get { return _PlayerCardCastPlace; } }

    public CemeteryUI CardCemetery { get { return _CardCemetery; } }

    public PlayerCDSlotGroup PlayerCDSlotGroup { get { return _PlayerCDSlotGroup; } }


    public PostProcessingSystem PostProcessingSystem { get { return _PostProcessingSystem; } }

    public FMODManagerSystem FMODManagerSystem { get { return _FMODManagerSystem; } }


    public void ExitGame()
    {
        Application.Quit();
    }

    //인스펙터에서 데이터 받아옴

    [SerializeField] CardCastPlace _PlayerCardCastPlace;
    [SerializeField] CardMixtureSystem _PlayerAttackSystem;
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

    [SerializeField] int ClearGold = 0;
    
    GameObject ThisTrunMark;
    GameObject NextTrunMark;

    public int GetClearGold { get { return ClearGold; } }

    bool isStart = false; // 게임 처음 시작할 때("전투 시작 UI 표시") 표시
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
       
        yield return null;

     

        if (Metronome == null)
        {
            Metronome = GetComponent<MetronomeSystem>();        
        }


        _FMODManagerSystem?.Initialize();
        _PostProcessingSystem?.Initialized();
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

        EndTurnButton?.onClick.AddListener(TurnSwap);
        EndTurnButton?.onClick.AddListener(() => { _FMODManagerSystem.PlayEffectSound("event:/UI/Turn_End"); }); // 클릭시 사운드
        
        //EndTurnButton?.gameObject.SetActive(false);


        _PlayerCardCastPlace.Reset();
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
        GameOver.SetActive(true);
        _FMODManagerSystem.PlayEffectSound("event:/UI/Fail_Stage");
    }

    public void GameClearFun()
    {
        StartCoroutine(DeleyLoadScene());
    }

    IEnumerator DeleyLoadScene()
    {

        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => PlayerCardCastPlace.isByeByeStart == false );

        int gold = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA,out gold);
        gold += ClearGold;


        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, gold);
        _FMODManagerSystem.PlayEffectSound("event:/UI/Clear_Stage"); // 클리어 사운드
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
        EndTurnButton.gameObject.SetActive(true);
    }

    public void TurnSwap()
    {
         // 턴앤드 클릭시 TurnSwap함수 재생

        ThisTurnUnit.EndTurn(); //ThisTurnUnit이 변경전 EndTurn실행하여 마무리
        (ThisTurnUnit, NextTurnUnit) = (NextTurnUnit, ThisTurnUnit); //swap

        ThisTurnUnit.StartTurn(); //ThisTurnUnit이 변경후 StartTurn함수 실행

        StartCoroutine(TurnMark());

    }

    IEnumerator TurnMark()
    {
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
        _FMODManagerSystem.PlayEffectSound("event:/UI/Change_Turn"); // 사운드도 같이
        ThisTrunMark.SetActive(true);
        yield return new WaitForSeconds(1f);
        ThisTrunMark.SetActive(false);

        (ThisTrunMark, NextTrunMark) =  (NextTrunMark ,ThisTrunMark); // swap

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
        StartCoroutine(UPdateComboCount(data*2));
    }

    IEnumerator UPdateComboCount(int count)
    {
        int combo = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.COMBO_DATA, out combo);
        for (int i = 0; i < count; i++)
        {
            combo++;
            GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.COMBO_DATA, combo);
            
            if(i % 200 == 0)yield return null;
        }
    }


    public void UIInputSetActive(bool active)
    {
        EventSystem?.SetActive(active);
    }
}

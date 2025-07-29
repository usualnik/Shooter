using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public event Action<GameMode> OnGameStarted;
    public event Action OnGameEneded;


    public static GameManager Instance { get; private set; }
    public bool IsMobilePlatform {  get; private set; }
    public float EndGameTimerMax {  get; private set; }
    public GameMode Mode { get; private set; }

    public bool IsPlayerWin { get; private set; }
    public bool IsEqualScore { get; private set; }

    public int PreviousRating { get; private set; }
    public int PreviousCurrency { get; private set; }

    [SerializeField] private GameMode _mode;
    [SerializeField] private bool _isTutorial;
    
    private GameData _currentGameData;

    private const int MainMenuBuildIndex = 1;

    private bool _isGameActive = false;
    private bool _isCountingDownTenSec = false;

    public enum GameMode
    {
        None = 0,
        ThreeVsThree = 1,
        PlayerBoss = 2,
        MobBoss = 3
    }
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance od GameManager");
        }

        if (_isTutorial)
        {
            IsMobilePlatform = Application.isMobilePlatform;
            EndGameTimerMax = 90;
            Mode = GameMode.ThreeVsThree;
        }
        else
        {
            ChooseRandomGameMode();
            IsMobilePlatform = Application.isMobilePlatform;
            EndGameTimerMax = 90;
            Mode = _mode;
        }

             
    }

    private void Start()
    {
        Invoke(nameof(StartGame),1f);
    }

    private void ChooseRandomGameMode()
    {
        int randomValue = Random.Range(0, 3);
        
        switch (randomValue)
        {
            case 0:
                _mode = GameMode.ThreeVsThree;
                break;
            case 1:
                _mode = GameMode.PlayerBoss;
                break;  
            case 2:
                _mode = GameMode.MobBoss;
                break;
            default:
                break;
        }
    }
    
    private void StartGame()
    {
        OnGameStarted?.Invoke(_mode);
        _isGameActive = true;
    }

    private void Update()
    {
        EndGameTimerMax -= Time.deltaTime;

        if (EndGameTimerMax <= 10f && !_isCountingDownTenSec)
        {
            _isCountingDownTenSec = true;
            AudioManager.Instance.Play("Countdown");       
            
        }

        if (EndGameTimerMax <= 0 && _isGameActive)
        {
            if (_mode == GameMode.ThreeVsThree) 
            {
                EndGame();
                _isGameActive = false;
            }
            else
            {
                KillBossEndGame(false);
                _isGameActive = false;
            }
           
        }
    }
    private void EndGame()
    {
        PreviousRating = PlayerData.Instance.GetRating();
        PreviousCurrency = PlayerData.Instance.GetSoftCurrency();


        if(GameUIManager.Instance.GetPlayerTeamScore() > GameUIManager.Instance.GetEnemyTeamScore())
        {
            // Player Win Condition
            PlayerData.Instance.AddSoftCurrency(10);
            PlayerData.Instance.AddExperience(200);           
            PlayerData.Instance.GainRating(Random.Range(500, 1000));
            IsPlayerWin = true;
        }
        else if(GameUIManager.Instance.GetPlayerTeamScore() == GameUIManager.Instance.GetEnemyTeamScore())
        {
            // Equal cond
            PlayerData.Instance.AddSoftCurrency(5);
            PlayerData.Instance.AddExperience(150);            
            PlayerData.Instance.GainRating(Random.Range(500, 1000));
            IsEqualScore = true;
        }
        else
        {
            // Player Loose Condition
            PlayerData.Instance.AddSoftCurrency(1);
            PlayerData.Instance.AddExperience(100);            
            PlayerData.Instance.GainRating(Random.Range(100, 500));
            IsPlayerWin = false;
        }

        OnGameEneded?.Invoke(); // Прокидывать резульат победы

        Invoke(nameof(LoadMainMenu),3f);
    }

    public void KillBossEndGame(bool bossKilled)
    {

        PreviousRating = PlayerData.Instance.GetRating();
        PreviousCurrency = PlayerData.Instance.GetSoftCurrency();


        switch (_mode)
        {
            case GameMode.None:
                break;
            case GameMode.ThreeVsThree:
                break;

            case GameMode.PlayerBoss:
                if (bossKilled)
                {
                    // if boss dead and it was player - player loose condition
                    PlayerData.Instance.AddSoftCurrency(1);
                    PlayerData.Instance.AddExperience(100);
                    PlayerData.Instance.GainRating(Random.Range(100, 500));
                    IsPlayerWin = false;
                }
                else
                {
                    //if boss not dead and it was player - player win
                    PlayerData.Instance.AddSoftCurrency(10);
                    PlayerData.Instance.AddExperience(200);
                    PlayerData.Instance.GainRating(Random.Range(500, 1000));
                    IsPlayerWin = true;
                }
                    break;

            case GameMode.MobBoss:

                if (bossKilled)
                {                   
                    //if boss dead and it was mob - player win
                    PlayerData.Instance.AddSoftCurrency(10);
                    PlayerData.Instance.AddExperience(200);
                    PlayerData.Instance.GainRating(Random.Range(500, 1000));
                    IsPlayerWin = true;
                }
                else
                {
                    //if boss NOT dead and it was mob - player loose
                    PlayerData.Instance.AddSoftCurrency(1);
                    PlayerData.Instance.AddExperience(100);
                    PlayerData.Instance.GainRating(Random.Range(100, 500));
                    IsPlayerWin = false;
                }
                break;
            default:
                break;
        }

        OnGameEneded?.Invoke(); // Прокидывать резульат победы

        Invoke(nameof(LoadMainMenu), 3f);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuBuildIndex);
    }

 

}
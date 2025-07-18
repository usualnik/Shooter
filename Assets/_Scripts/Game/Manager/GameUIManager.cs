using System.Collections;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance {  get; private set; }

    [Header("Start | End Panels")]
    [SerializeField] private GameObject _startGamePanel;
    [SerializeField] private GameObject _endGamePanel;

    [Header("Score Panel")]
    [SerializeField] private TextMeshProUGUI _playerTeamScoreText;
    [SerializeField] private TextMeshProUGUI _enemyTeamScoreText;
    [SerializeField] private TextMeshProUGUI _gameTimerText;

    [Header("Boss HP panel")]
    [SerializeField] private GameObject _bossHP;

    [Header("Player Death Panel")]
    [SerializeField] private GameObject _playerDeathPanel;


    private const string PlayerTeamTag = "PlayerTeam";
    private const string EnemyTeamTag = "EnemyTeam";

    private int _playerTeamScore = 0;
    private int _enemyTeamScore = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one instance of GameUIManager");
    }

    private void Start()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameEneded += GameManager_OnGameEneded;
    }
      
    private void OnDestroy()
    {
        GameManager.Instance.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.Instance.OnGameEneded -= GameManager_OnGameEneded;
    }

    private void GameManager_OnGameStarted(GameManager.GameMode gameMode)
    {
        ShowStartGamePanel();

        if (gameMode == GameManager.GameMode.PlayerBoss || gameMode == GameManager.GameMode.MobBoss)
        {
            ShowBossHpPanel();
        }           

    }
    private void GameManager_OnGameEneded()
    {
        ShowEndGamePanel();
    }

    private void ShowBossHpPanel()
    {
        _bossHP.SetActive(true);
        
    }
    private void ShowStartGamePanel()
    {   
        //Show panel
        
        StartCoroutine(HideStartGamePanel());
    }

    private void ShowEndGamePanel()
    {
        _endGamePanel.SetActive(true);
    }
    private IEnumerator HideStartGamePanel()
    {
        yield return new WaitForSeconds(4f);
        _startGamePanel.SetActive(false);
    }

    public void AddScore(string tag)
    {
        switch (tag)
        {
            case PlayerTeamTag:
                _playerTeamScore++;
                _playerTeamScoreText.text = _playerTeamScore.ToString();
                break;
            case EnemyTeamTag:
                _enemyTeamScore++;
                _enemyTeamScoreText.text = _enemyTeamScore.ToString();
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        //_gameTimerText.text = GameManager.Instance.EndGameTimerMax.ToString("0");
       
        int minutes = Mathf.FloorToInt(GameManager.Instance.EndGameTimerMax / 60);
        int seconds = Mathf.FloorToInt(GameManager.Instance.EndGameTimerMax % 60);

        _gameTimerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
    
    public void ShowPlayerDeathPanel()
    {
        _playerDeathPanel.SetActive(true);
        StartCoroutine(HidePlayerDeathPanel());
    }
    private IEnumerator HidePlayerDeathPanel()
    {
        yield return new WaitForSeconds(5f);
        _playerDeathPanel.SetActive(false);
    }
    public int GetPlayerTeamScore() => _playerTeamScore;
    public int GetEnemyTeamScore() => _enemyTeamScore;


}

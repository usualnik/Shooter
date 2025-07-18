using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuUIManager : MonoBehaviour
{
    public static MainMenuUIManager Instance { get; private set; }

    [Header("Menu windows refs")]
    [SerializeField] private GameObject[] _windows;

    [Header("Player Rating text ref")]
    [SerializeField] private TextMeshProUGUI _ratingText;

    [Header("Player Currency text ref")]
    [SerializeField] private TextMeshProUGUI _softCurrencyText;
    [SerializeField] private TextMeshProUGUI _hardCurrencyText;

    [Header("Player level refs")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _levelProgressText;
    [SerializeField] private Image _levelProgressBar;

    [Header("Player stats text refs")]
    [SerializeField] private TextMeshProUGUI _levelPointsText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _critChanceText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of PlayerData");
        }
    }
    private void Start()
    {
        //Open main Menu
        OpenWindow(0);

        InitRatingText();
        InitCurrencyText();
        InitPlayerLevelUI();
        InitPlayerStatsText();

        PlayerData.Instance.OnLevelUp += PlayerData_OnLevelUp;
        PlayerData.Instance.OnStatChanged += PlayerData_OnStatChanged;
    }

    

    private void OnDestroy()
    {
        PlayerData.Instance.OnLevelUp -= PlayerData_OnLevelUp;
        PlayerData.Instance.OnStatChanged -= PlayerData_OnStatChanged;
    }

    private void PlayerData_OnStatChanged()
    {
        UpdatePlayerStatsText();
    }

    private void PlayerData_OnLevelUp()
    {
        UpdatePlayerLevelUI();
        UpdatePlayerStatsText();

    }

    public void OpenWindow(int index)
    {
        //Close every other window
        foreach (var window in _windows)
        {
            window.gameObject.SetActive(false);
        }
        //Open one, that we need
        _windows[index].SetActive(true);
    }

    #region Init
    private void InitRatingText()
    {
       _ratingText.text = PlayerData.Instance.GetRating().ToString();
    }
    private void InitCurrencyText()
    {
        _softCurrencyText.text = PlayerData.Instance.GetSoftCurrency().ToString("D4");
        _hardCurrencyText.text = PlayerData.Instance.GetHardCurrency().ToString("D4");

    }
    private void InitPlayerLevelUI()
    {     
        _levelText.text = PlayerData.Instance.GetPlayerLevel().ToString();
        
        _levelProgressText.text = string.Format("{0} / {1}", PlayerData.Instance.GetCurrentLevelProgress().ToString()
            , PlayerData.Instance.GetLevelProgressMax().ToString());

        _levelProgressBar.fillAmount = (float)PlayerData.Instance.GetCurrentLevelProgress() / (float)PlayerData.Instance.GetLevelProgressMax();
    }
    
    private void InitPlayerStatsText()
    {
        _levelPointsText.text = PlayerData.Instance.GetLevelPoints().ToString();
        _healthText.text = PlayerData.Instance.GetHealth().ToString();
        _damageText.text = PlayerData.Instance.GetDamage().ToString();
        _critChanceText.text = PlayerData.Instance.GetCritChance().ToString();
        _attackSpeedText.text = PlayerData.Instance.GetAttackSpeed().ToString();
    }

    #endregion

    #region Update
    public void UpdateRatingText()
    {
        _ratingText.text = PlayerData.Instance.GetRating().ToString();
    }
    public void UpdatePlayerStatsText()
    {
        _levelPointsText.text = PlayerData.Instance.GetLevelPoints().ToString();
        _healthText.text = PlayerData.Instance.GetHealth().ToString();
        _damageText.text = PlayerData.Instance.GetDamage().ToString();
        _critChanceText.text = PlayerData.Instance.GetCritChance().ToString();
        _attackSpeedText.text = PlayerData.Instance.GetAttackSpeed().ToString();
    }

    private void UpdatePlayerLevelUI()
    {
        _levelText.text = PlayerData.Instance.GetPlayerLevel().ToString();

        _levelProgressText.text = string.Format("{0} / {1}", PlayerData.Instance.GetCurrentLevelProgress().ToString()
            , PlayerData.Instance.GetLevelProgressMax().ToString());

        _levelProgressBar.fillAmount = (float)PlayerData.Instance.GetCurrentLevelProgress() / (float)PlayerData.Instance.GetLevelProgressMax();
    }

    #endregion
}

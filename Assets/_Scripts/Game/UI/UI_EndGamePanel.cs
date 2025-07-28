using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class UI_EndGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _ratingText;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private TextMeshProUGUI _softCurrencyText;

    private float animationDuration = 1.5f; 

    //Rating
    private int _currentRating; 
    private int _targetRating;

    //Currency
    private int _currentCurrency;
    private int _targetCurrency;

    private void OnEnable()
    {
        UpdateEndGameText();
    }

    private void UpdateEndGameText()
    {
        _scoreText.text = $"{GameUIManager.Instance.GetPlayerTeamScore().ToString()} | {GameUIManager.Instance.GetEnemyTeamScore().ToString()}";

        ShowResultText();

        ShowRatingText();       
        ShowCurrencyText();        
    }

    private void ShowResultText()
    {
        if (GameManager.Instance.IsEqualScore)
        {
            if(YG2.envir.language == "ru")
                _resultText.text = $"Õ»◊‹ﬂ!!!";
            else
                _resultText.text = $"DRAW!!!";
        }
        else if (GameManager.Instance.IsPlayerWin)
        {
            _resultText.color = Color.green;

            if (YG2.envir.language == "ru")
                _resultText.text = $"¬¿ÿ¿  ŒÃ¿Õƒ¿ œŒ¡≈ƒ»À¿!!!";
            else
                _resultText.text = $"YOUR TEAM IS THE WINNER!!!";
        }
        else
        {
            _resultText.color = Color.red;

            if (YG2.envir.language == "ru")
                _resultText.text = $"¬¿ÿ¿  ŒÃ¿Õƒ¿ œ–Œ»√–¿À¿!!!";
            else
                _resultText.text = $"YOUR TEAM WAS DEFEATED!!!";
        }
    }


    public void ShowRatingText()
    {
        _currentRating = GameManager.Instance.PreviousRating;
        _targetRating = PlayerData.Instance.GetRating();

        StartCoroutine(AnimateRating());
    }
    private IEnumerator AnimateRating()
    {
        float elapsedTime = 0f;
        int startRating = _currentRating;

        while (elapsedTime < animationDuration)
        {
            _currentRating = (int)Mathf.Lerp(startRating, _targetRating, elapsedTime / animationDuration);
            _ratingText.text = _currentRating.ToString();

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _currentRating = _targetRating;
        _ratingText.text = _currentRating.ToString();
    }

    public void ShowCurrencyText()
    {
        _currentCurrency = GameManager.Instance.PreviousCurrency;
        _targetCurrency = PlayerData.Instance.GetSoftCurrency();

        StartCoroutine(AnimateCurrency());
    }

    private IEnumerator AnimateCurrency()
    {
        float elapsedTime = 0f;
        int startCurrency = _currentCurrency;

        while (elapsedTime < animationDuration)
        {
            _currentCurrency = (int)Mathf.Lerp(startCurrency, _targetCurrency, elapsedTime / animationDuration);
            _softCurrencyText.text = _currentCurrency.ToString();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _currentCurrency = _targetCurrency;
        _softCurrencyText.text = _currentCurrency.ToString("d4");
    }

}

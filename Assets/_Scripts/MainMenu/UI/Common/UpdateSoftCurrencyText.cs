using TMPro;
using UnityEngine;

public class UpdateSoftCurrencyText : MonoBehaviour
{
    private TextMeshProUGUI _softCurrencyText;

    private void Awake()
    {
        _softCurrencyText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        PlayerData.Instance.OnPlayerSoftCurrencyChanged += PlayerData_OnPlayerSoftCurrencyChanged;
    }

    private void OnDestroy()
    {
        PlayerData.Instance.OnPlayerSoftCurrencyChanged -= PlayerData_OnPlayerSoftCurrencyChanged;
    }

    private void PlayerData_OnPlayerSoftCurrencyChanged()
    {
        UpdateSoftCurrency();
    }


    private void OnEnable()
    {
        UpdateSoftCurrency();
    }

    private void UpdateSoftCurrency()
    {
        _softCurrencyText.text = PlayerData.Instance.GetSoftCurrency().ToString("D4");
    }
}

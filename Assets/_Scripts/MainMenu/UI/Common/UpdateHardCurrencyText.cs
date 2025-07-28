using TMPro;
using UnityEngine;

public class UpdateHardCurrencyText : MonoBehaviour
{
    private TextMeshProUGUI _hardCurrencyText;

    private void Awake()
    {
        _hardCurrencyText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        PlayerData.Instance.OnPlayerHardCurrencyChanged += PlayerData_OnPlayerHardCurrencyChanged;
    }

    private void OnDestroy()
    {
        PlayerData.Instance.OnPlayerHardCurrencyChanged -= PlayerData_OnPlayerHardCurrencyChanged;
    }

    private void PlayerData_OnPlayerHardCurrencyChanged()
    {
        UpdateHardCurrency();
    }

    private void OnEnable()
    {
        UpdateHardCurrency();
    }

    private void UpdateHardCurrency()
    {
        _hardCurrencyText.text = PlayerData.Instance.GetHardCurrency().ToString("D4");
    }
}

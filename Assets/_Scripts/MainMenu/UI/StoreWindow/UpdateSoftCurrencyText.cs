using TMPro;
using UnityEngine;

public class UpdateSoftCurrencyText : MonoBehaviour
{
    private TextMeshProUGUI _softCurrencyText;

    private void Awake()
    {
        _softCurrencyText = GetComponent<TextMeshProUGUI>();
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

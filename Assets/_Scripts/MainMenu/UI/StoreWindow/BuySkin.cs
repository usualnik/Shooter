using System;
using UnityEngine;
using UnityEngine.UI;

public class BuySkin : MonoBehaviour
{
    public event Action OnBuySkin;


    [SerializeField] private SkinHandler _skinButtonsHandler;
    

    private Button _buyButton;

    private const int RequireHardCurrencyToBuy = 20;

    private void Awake()
    {
        _buyButton = GetComponent<Button>();

    }
    private void Start()
    {
        _buyButton.onClick.AddListener(BuySkinButton);
    }

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(BuySkinButton);
    }

    private void BuySkinButton()
    {
        if(PlayerData.Instance.GetHardCurrency() >= RequireHardCurrencyToBuy)
        {
            PlayerData.Instance.AddHardCurrency(-RequireHardCurrencyToBuy);

            _skinButtonsHandler.UnlockNewSkin();
            OnBuySkin?.Invoke();
        }
    }


}

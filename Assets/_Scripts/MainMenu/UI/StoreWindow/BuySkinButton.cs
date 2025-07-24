using UnityEngine;
using UnityEngine.UI;

public class BuySkinButton : MonoBehaviour
{
    [SerializeField] private SkinHandler _skinButtonsHandler;

    private Button _buyButton;

    private const int RequireHardCurrencyToBuy = 20;

    private void Awake()
    {
        _buyButton = GetComponent<Button>();

    }
    private void Start()
    {
        _buyButton.onClick.AddListener(BuySkin);
    }

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(BuySkin);
    }

    private void BuySkin()
    {
        if(PlayerData.Instance.GetHardCurrency() >= RequireHardCurrencyToBuy)
        {
            PlayerData.Instance.AddHardCurrency(-RequireHardCurrencyToBuy);

            _skinButtonsHandler.UnlockNewSkin();

        }
    }
}

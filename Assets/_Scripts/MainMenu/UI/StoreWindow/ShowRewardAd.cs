using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShowRewardAd : MonoBehaviour
{
    private Button _button;      
    string rewardID;

    private const int HardCurrencyRewardValue = 5;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(MyRewardAdvShow);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(MyRewardAdvShow);
    }

    public void MyRewardAdvShow()
    {
       
        YG2.RewardedAdvShow(rewardID, () =>
        {
            PlayerData.Instance.AddHardCurrency(HardCurrencyRewardValue);
                        
        });
    }
}

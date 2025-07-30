using UnityEngine;
using YG;

public class YandexSaveLoadManager : MonoBehaviour
{
    
    void Start()
    {
        // Set Player data accordingly to server data
        PlayerData.Instance.SetSoftCurrency(YG2.saves.SoftCurrency);
        PlayerData.Instance.SetHardCurrency(YG2.saves.HardCurrency);
        PlayerData.Instance.SetFirstTimePlayed(YG2.saves.IsFirstTimePlayed);
        
    }

}

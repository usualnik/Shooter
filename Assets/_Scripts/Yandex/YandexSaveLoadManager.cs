using UnityEngine;
using YG;

public class YandexSaveLoadManager : MonoBehaviour
{
    
    void Start()
    {
        // Set Player data accordingly to server data
        PlayerData.Instance.SetSoftCurrency(YG2.saves.softCurrency);
        PlayerData.Instance.SetHardCurrency(YG2.saves.hardCurrency);
    }

}

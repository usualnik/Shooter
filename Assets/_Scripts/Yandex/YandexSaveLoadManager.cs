using UnityEngine;
using YG;

public class YandexSaveLoadManager : MonoBehaviour
{
    
    void Start()
    {     
        PlayerData.Instance.SetPlayerData(YG2.saves.YandexServerData);     

    }

}

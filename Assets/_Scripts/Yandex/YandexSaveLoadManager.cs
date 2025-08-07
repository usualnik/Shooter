using UnityEngine;
using YG;

public class YandexSaveLoadManager : MonoBehaviour
{
    public static YandexSaveLoadManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
            PlayerData.Instance.SetPlayerData(YG2.saves.YandexServerData);
    }
   

}

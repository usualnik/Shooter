using UnityEngine;
using YG;

public class SetFirstTimePlayed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerData.Instance.SetFirstTimePlayed(false);
        YG2.saves.AlreadyPlayed = true;
        PlayerData.Instance.SavePlayerDataToCloud();
    }

   
}

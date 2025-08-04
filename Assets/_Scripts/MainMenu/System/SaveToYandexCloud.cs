using UnityEngine;

public class SaveToYandexCloud : MonoBehaviour
{
    private float _saveTimer;
    private const float SaveTimerMax = 15f;

    private void Awake()
    {
        _saveTimer = SaveTimerMax;
    }

    void Start()
    {
        //Every menu loading - save progress;
        PlayerData.Instance.SavePlayerDataToCloud();
    }

    private void Update()
    {
        _saveTimer -= Time.deltaTime;
       
        if (_saveTimer <= 0)
        {
            PlayerData.Instance.SavePlayerDataToCloud();
            _saveTimer = SaveTimerMax;
        }
    }
}

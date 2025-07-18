using TMPro;
using UnityEngine;

public class UI_PlayerDeathPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
                        
    private float _playerDeathTimer;
    private const float PlayerDeatTimerMax = 5f;

    private bool _startTimer = false;

    private void OnEnable()
    {
        _startTimer = true;
        _playerDeathTimer = PlayerDeatTimerMax;
    }

    private void OnDisable()
    {
        _startTimer = false;
    }
        
    void Update()
    {
        if (_startTimer)
        {
            _playerDeathTimer -= Time.deltaTime;
            _timerText.text = string.Format("Воскрешение через... \n {0}", _playerDeathTimer.ToString("0"));
        }       
    }
}

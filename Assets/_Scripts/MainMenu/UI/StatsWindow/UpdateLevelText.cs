using TMPro;
using UnityEngine;

public class UpdateLevelText : MonoBehaviour
{
    private TextMeshProUGUI _levelText;

    private void Awake()
    {
        _levelText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        PlayerData.Instance.OnLevelUp += PlayerData_OnLevelUp;
    }
    private void OnDestroy()
    {
        PlayerData.Instance.OnLevelUp -= PlayerData_OnLevelUp;
    }
    private void OnEnable()
    {
        UpdatePlayerLevelText();
    }

    private void PlayerData_OnLevelUp()
    {
        _levelText.text = PlayerData.Instance.GetPlayerLevel().ToString();
    }

    private void UpdatePlayerLevelText()
    {
        _levelText.text = PlayerData.Instance.GetPlayerLevel().ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class AddStatButton : MonoBehaviour
{
	private enum StatButtonType
	{
		HealthButton,
		DamageButton,
		CritChanceButton,
		AttackSpeedButton
	}

	[SerializeField] private StatButtonType buttonType;
	
	private Button _button;

	private const int HeathIncreaseValue = 1;
    private const float DamageIncreaseValue = 0.2f;
    private const int CritChanceIncreaseValue = 1;
    private const float AttackSpeedIncreaseValue = -0.01f;

    private void Awake()
    {
        _button = GetComponent<Button>();		
    }

    private void Start()
    {
		_button.onClick.AddListener(SendStatData);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(SendStatData);
    }

    private void SendStatData()
	{
		switch (buttonType)
		{
			case StatButtonType.HealthButton:
				PlayerData.Instance.AddHealth(HeathIncreaseValue);				
				break;
			case StatButtonType.DamageButton:
                PlayerData.Instance.AddDamage(DamageIncreaseValue);
                break;
			case StatButtonType.CritChanceButton:
                PlayerData.Instance.AddCritChance(CritChanceIncreaseValue);
                break;
			case StatButtonType.AttackSpeedButton:
                PlayerData.Instance.AddAttackSpeed(AttackSpeedIncreaseValue);
                break;
			default:
				break;
		}
	}

}

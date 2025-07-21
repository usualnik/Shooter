using TMPro;
using UnityEngine;

public class SetPlayerNickname : MonoBehaviour
{
    private TextMeshProUGUI _playerNickText;

    private void Awake()
    {
        _playerNickText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _playerNickText.text = PlayerData.Instance.GetNickName();
    }

}

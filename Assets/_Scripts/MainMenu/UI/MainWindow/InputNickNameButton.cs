using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputNickNameButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _inputFieldText;
    [SerializeField] private TextMeshProUGUI _playerNickNameText;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        _button.onClick.AddListener(ChangePlayerNickName);
    }

    public void ChangePlayerNickName()
    {
        PlayerData.Instance.SetNickname( _inputFieldText.text );
        _playerNickNameText.text = _inputFieldText.text;
    }
}

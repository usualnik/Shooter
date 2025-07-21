using TMPro;
using UnityEngine;

public class NicknameHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nicknameText;

    private TMP_InputField _inputField;

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
    }

    void Start()
    {      
        _inputField.onEndEdit.AddListener(UpdateNickname);
    }

    private void UpdateNickname(string newNickname)
    {
        nicknameText.text = newNickname;

        PlayerData.Instance.SetNickname(newNickname);
    }
}
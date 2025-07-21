using TMPro;
using UnityEngine;

public class UI_GenerateBotNickname : MonoBehaviour
{
    private TextMeshProUGUI nicknameText;

    private void Awake()
    {
        nicknameText  = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {       
        nicknameText.text = NicknameGenerator.Instance.GetRandomNickname();
    }
}
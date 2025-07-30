using UnityEngine;
using TMPro;


public class NickNameText : MonoBehaviour
{
    private TMP_InputField _inputField;

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
    }


    private void OnEnable()
    {
        _inputField.text = PlayerData.Instance.GetNickName();
    }

   
}

using UnityEngine;
using UnityEngine.UI;

public class PressButtonSound : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        if (TryGetComponent(out Button button))
        {
            _button = button;
        }        
    }

    private void Start()
    {
        _button.onClick?.AddListener(PlayButtonSound);
    }
    private void OnDestroy()
    {
        _button.onClick?.RemoveListener(PlayButtonSound);
    }

    private void PlayButtonSound()
    {
        AudioManager.Instance.Play("Button");
    }
}

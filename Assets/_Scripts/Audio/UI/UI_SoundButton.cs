using UnityEngine;
using UnityEngine.UI;

public class UI_SoundButton : MonoBehaviour
{
    private Button _soundButton;
    private Image _buttonImage;

    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    private bool _isPlaying;


    private void Awake()
    {
        _soundButton = GetComponent<Button>();
        _buttonImage = GetComponent<Image>();

        _isPlaying = true;

    }

    private void Start()
    {
        _soundButton.onClick.AddListener(ChangeSprite);

    }

    private void OnDestroy()
    {
        _soundButton.onClick.RemoveListener(ChangeSprite);

    }

    private void ChangeSprite()
    {
        if (_isPlaying)
        {
            _buttonImage.sprite = _soundOffSprite;
            _isPlaying = false;                       
        }
        else
        {
            
            _buttonImage.sprite = _soundOnSprite;
            _isPlaying = true;          

        }
    }
}

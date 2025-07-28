using UnityEngine;
using UnityEngine.UI;



public class UI_MusicButton : MonoBehaviour
{
    private Button _musicButton;
    private Image _buttonImage;

    [SerializeField] private Sprite _musicOnSprite;
    [SerializeField] private Sprite _musicOffSprite;

    private bool _isPlaying;


    private void Awake()
    {
        _musicButton = GetComponent<Button>();
        _buttonImage = GetComponent<Image>();
        _isPlaying = true;
       
    }

    private void Start()
    {
        _musicButton.onClick.AddListener(ChangeSprite);

    }

    private void OnDestroy()
    {
        _musicButton.onClick.RemoveListener(ChangeSprite);

    }

    private void ChangeSprite()
    {
        if (_isPlaying)
        {
            _buttonImage.sprite = _musicOffSprite;
            _isPlaying = false;
        }
        else
        {
            _buttonImage.sprite = _musicOnSprite;
            _isPlaying = true;
        }
    }
}

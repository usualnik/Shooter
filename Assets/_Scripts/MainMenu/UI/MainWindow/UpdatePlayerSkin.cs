using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerSkin : MonoBehaviour
{
    //private SpriteRenderer _spriteRenderer;
    private Image _image;

    private void Awake()
    {        
       // _spriteRenderer = GetComponent<SpriteRenderer>();
       _image = GetComponent<Image>();
    }

    void Start()
    {
        PlayerData.Instance.OnPlayerSkinChanged += PlayerData_OnPlayerSkinChanged;
    }

    private void OnDestroy()
    {
        PlayerData.Instance.OnPlayerSkinChanged -= PlayerData_OnPlayerSkinChanged;
    }

    private void OnEnable()
    {
        UpdatePlayerVisuals();
    }

    private void PlayerData_OnPlayerSkinChanged()
    {
        UpdatePlayerVisuals();
    }
    
    void UpdatePlayerVisuals()
    {        
        _image.sprite = PlayerData.Instance.GetSkinDataSO().SkinSprite;
    }
}

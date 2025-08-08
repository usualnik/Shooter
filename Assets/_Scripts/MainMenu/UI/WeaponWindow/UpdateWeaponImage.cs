using UnityEngine;
using UnityEngine.UI;

public class UpdateWeaponImage : MonoBehaviour
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
        PlayerData.Instance.OnPlayerWeaponChanged += PlayerData_OnPlayerWeaponChanged;
    }

    private void OnDestroy()
    {
        PlayerData.Instance.OnPlayerSkinChanged -= PlayerData_OnPlayerWeaponChanged;
    }

    private void OnEnable()
    {
        UpdatePlayerVisuals();
    }

    private void PlayerData_OnPlayerWeaponChanged()
    {
        UpdatePlayerVisuals();
    }

    void UpdatePlayerVisuals()
    {
        if(_image != null) 
            _image.sprite = PlayerData.Instance.GetCurrentGunSO().ItemPreview;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerSkin : MonoBehaviour
{   
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {        
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        PlayerData.Instance.OnPlayerSkinChanged += PlayerData_OnPlayerSkinChanged;
    }

    private void OnDestroy()
    {
        PlayerData.Instance.OnPlayerSkinChanged -= PlayerData_OnPlayerSkinChanged;
    }

    private void PlayerData_OnPlayerSkinChanged()
    {
        UpdatePlayerVisuals();
    }
    
    void UpdatePlayerVisuals()
    {        
        _spriteRenderer.sprite = PlayerData.Instance.GetSkinDataSO().SkinSprite;
    }
}

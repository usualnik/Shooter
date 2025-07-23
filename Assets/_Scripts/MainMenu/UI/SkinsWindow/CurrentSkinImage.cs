using UnityEngine;
using UnityEngine.UI;

public class CurrentSkinImage : MonoBehaviour
{
    private Image _currentSlinImage;

    private void Awake()
    {
        _currentSlinImage = GetComponent<Image>();        
    }
    private void Start()
    {
        UpdateSkinSprite();

        PlayerData.Instance.OnPlayerSkinChanged += PlayerData_OnPlayerSkinChanged;
    }

    private void OnDestroy()
    {
        PlayerData.Instance.OnPlayerSkinChanged -= PlayerData_OnPlayerSkinChanged;
    }

    private void PlayerData_OnPlayerSkinChanged()
    {
       UpdateSkinSprite();
    }

    private void UpdateSkinSprite()
    {
        _currentSlinImage.sprite = PlayerData.Instance.GetSkinDataSO().SkinSprite;
    }
}

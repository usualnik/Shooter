using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseSkinButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SkinDataSO skinDataSO;
    [SerializeField] private Image _skinPreview;
    [SerializeField] private SkinHandler _selectedButtonHandler;
    [SerializeField] private bool _isUnlocked;

    private Image _buttonImage;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
    }

    private void Start()
    {
        if(skinDataSO != null)
            _skinPreview.sprite = skinDataSO.SkinSprite;

        if (_isUnlocked)
            _selectedButtonHandler.UpdateButtonColor(_buttonImage);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isUnlocked)
        {
            SetPlayerSkinData();
            _selectedButtonHandler.UpdateButtonColor(_buttonImage);
        }      
    }

    private void SetPlayerSkinData()
    {
        PlayerData.Instance.SetSkinData(skinDataSO);
    }

    public bool IsUnlocked() { return _isUnlocked; }
    public void SetUnlocked() { _isUnlocked = true; }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseSkinButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _skinIndex;

    [SerializeField] private SkinDataSO skinDataSO;
    [SerializeField] private Image _skinPreview;   
    [SerializeField] private bool _isUnlocked;
    [SerializeField] private GameObject _lockIcon;

    [SerializeField] private SkinHandler _skinHandler;
   
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
        {
            _skinHandler.UpdateButtonColor(_buttonImage);
            _lockIcon.SetActive(false);

        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isUnlocked)
        {
            SetPlayerSkinData();
            _skinHandler.UpdateButtonColor(_buttonImage);
        }      
    }

    private void SetPlayerSkinData()
    {
        PlayerData.Instance.SetSkinData(skinDataSO);
    }

    public void RemoveLockIcon()
    {
        _lockIcon.SetActive(false);
    }

    public bool IsUnlocked() { return _isUnlocked; }
    public void SetUnlocked() { _isUnlocked = true; }
    public SkinDataSO GetSkinData() { return skinDataSO; }
    public int GetSkinIndex() { return _skinIndex; }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponButton : MonoBehaviour
{
    [Header("Configure cell")]
    [SerializeField] private int _weaponIndex;
    [SerializeField] private WeaponSO WeaponData;
    [SerializeField] private Image _weaponPreview;
    [SerializeField] private bool _isUnlocked;
    [SerializeField] private int _weaponPrice;
    [SerializeField] private GameObject _lockIcon;

    [Header("System refs")]
    [SerializeField] private WeaponSelectedButtonHandler _weaponSelectedButtonHandler;

    [Header("Text refs")]
    [SerializeField] private TextMeshProUGUI _priceText;  

    private Image _buttonImage;
    private Button _button;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        if (_isUnlocked) 
        {
            _lockIcon.SetActive(false);
        }
        _weaponPreview.sprite = WeaponData?.ItemPreview;               
        _button.onClick.AddListener(SelectButton);

        _priceText.text = "$ " + _weaponPrice.ToString();

        InitUnlockedWeapon();

    }


    private void InitUnlockedWeapon()
    {
        if (PlayerData.Instance.GetUnlockedWeaponsIndexesList().Contains(_weaponIndex))
        {
            _lockIcon.SetActive(false);
            _priceText.gameObject.SetActive(false);
            _isUnlocked = true;
        }
    }


    private void OnDestroy()
    {
        
        _button.onClick.RemoveListener(SelectButton);
    }

    private void SelectButton()
    {
        if (_isUnlocked)
        {
            PlayerData.Instance.SetPlayerWeaponData(WeaponData);
            _weaponSelectedButtonHandler.UpdateColor(_buttonImage);
        }else if (!_isUnlocked && PlayerData.Instance.GetSoftCurrency() >= _weaponPrice)
        {
            BuyWeapon();
        }
        
    }

    private void BuyWeapon()
    {
        _lockIcon.SetActive(false);
        _priceText.gameObject.SetActive(false);
        _isUnlocked = true;

        PlayerData.Instance.AddSoftCurrency(-_weaponPrice);
        PlayerData.Instance.SetPlayerWeaponData(WeaponData);
        PlayerData.Instance.AddUnlockedWeaponIndex(_weaponIndex);
        _weaponSelectedButtonHandler.UpdateColor(_buttonImage);
    }

    public bool IsUnlocked() { return _isUnlocked; }
    public void SetUnlocked() { _isUnlocked = true; }
}

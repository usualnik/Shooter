using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponButton : MonoBehaviour
{    
    [SerializeField] private WeaponSO WeaponData;
    [SerializeField] private Image _weaponPreview;
    [SerializeField] private WeaponSelectedButtonHandler _weaponSelectedButtonHandler;

    private Image _buttonImage;
    private Button _button;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        _weaponPreview.sprite = WeaponData?.ItemPreview;

        _button.onClick.AddListener(SendWeaponData);
        _button.onClick.AddListener(SelectButton);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(SendWeaponData);
        _button.onClick.RemoveListener(SelectButton);
    }

    private void SelectButton()
    {
        _weaponSelectedButtonHandler.UpdateColor(_buttonImage);
    }

    private void SendWeaponData()
    { 
        PlayerData.Instance.SetPlayerWeaponData(WeaponData);   
    }
}

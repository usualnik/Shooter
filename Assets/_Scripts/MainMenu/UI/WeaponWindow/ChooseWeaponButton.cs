using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponButton : MonoBehaviour
{    
    [SerializeField] private WeaponSO WeaponData;

    private Image _buttonImage;
    private Button _button;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        _buttonImage.sprite = WeaponData?.ItemPreview;
        _button.onClick.AddListener(SendWeaponData);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(SendWeaponData);
    }

    private void SendWeaponData()
    { 
        PlayerData.Instance.SetPlayerWeaponData(WeaponData);   
    }
}

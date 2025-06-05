
using TMPro;
using UnityEngine;

public class UI_ShootButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private PlayerShooting _playerShooting;
    
    private void Start()
    {
        _playerShooting.OnShoot += PlayerShootingOnOnShoot;
        UpdateAmmoText(_playerShooting.GetPlayerAmmo());
    }

    private void OnDestroy()
    {
        _playerShooting.OnShoot -= PlayerShootingOnOnShoot;
    }

    private void PlayerShootingOnOnShoot(object sender, PlayerShooting.OnShootEventArgs e)
    {
        UpdateAmmoText(e.PlayerAmmoLeft);
    }
    
    private void UpdateAmmoText(int ammoLeft)
    {
        _ammoText.text = ammoLeft.ToString();
    }

}

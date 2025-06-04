using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject _inventoryWindow;
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

   public void ShowInventory()
   {
      _inventoryWindow.SetActive(!_inventoryWindow.activeInHierarchy);
   }

   private void UpdateAmmoText(int ammoLeft)
   {
      _ammoText.text = ammoLeft.ToString();
   }
   
}

   

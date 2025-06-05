using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject _currentWeaponPrefab;

    private PlayerMovement _playerMovement;
    public event EventHandler<OnShootEventArgs> OnShoot;
    
    public class OnShootEventArgs : EventArgs
    {
        public int PlayerAmmoLeft;
    }

    private void Awake()
    {
        _playerMovement = gameObject.GetComponent<PlayerMovement>();
    }
  
    public void Shoot()
    {
        if (_currentWeaponPrefab)
        {
            var onShootEventArgs = new OnShootEventArgs
            {
                PlayerAmmoLeft = GetPlayerAmmo()
            };
            
            OnShoot?.Invoke(this,onShootEventArgs);

            Vector2 shootDir = _playerMovement.GetLookDirection();
            
            // need to cache this later
            _currentWeaponPrefab.TryGetComponent(out BaseWeapon weapon);
            weapon.Shoot(shootDir);
        }
    }

    public int GetPlayerAmmo()
    {
        // need to cache this later
        _currentWeaponPrefab.TryGetComponent(out BaseWeapon weapon);
        return weapon.AmmoLeft;
    }
}

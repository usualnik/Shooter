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
            
            _currentWeaponPrefab.GetComponent<BaseWeapon>().Shoot(shootDir);
        }
    }

    public int GetPlayerAmmo()
    {
        return _currentWeaponPrefab.GetComponent<BaseWeapon>().AmmoLeft;
    }
}

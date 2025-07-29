using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerShooting : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _shootCooldown = 1.5f;
    [SerializeField] private GameObject _currentWeaponPrefab;


    private Joystick _shootingJoystick;

    private float _lastShootTime;

    private void Awake()
    {
       
        _shootingJoystick = GameObject.FindGameObjectWithTag("ShootingJoystick")?.GetComponent<Joystick>();
    }

    private void Start()
    {
        _shootCooldown *= PlayerData.Instance.GetAttackSpeed();
        _lastShootTime = -_shootCooldown;
    }

    private void Update()
    {
        HandleShootingInput();    
    }

    private void HandleShootingInput()
    {
        if (!GameManager.Instance.IsMobilePlatform && Input.GetButtonDown("Fire1"))
        {
            Shoot(GetMouseDirection());
        }
        else if (GameManager.Instance.IsMobilePlatform &&
                (_shootingJoystick.Horizontal != 0 || _shootingJoystick.Vertical != 0))
        {
            Shoot(new Vector2(_shootingJoystick.Horizontal, _shootingJoystick.Vertical).normalized);
        }
    }

    private void Shoot(Vector2 direction)
    {
        if (Time.time - _lastShootTime < _shootCooldown || !_currentWeaponPrefab)
            return;                  

        if (_currentWeaponPrefab.TryGetComponent(out BaseWeapon weapon))
        {
            weapon.Shoot(direction);
            _lastShootTime = Time.time;            
        }
    }

    private Vector2 GetMouseDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (new Vector2(mousePos.x, mousePos.y) - (Vector2)transform.position).normalized;
    }

    public void SetCurrentGun(GameObject weaponPrefab)
    {
        _currentWeaponPrefab = weaponPrefab;
    }
}
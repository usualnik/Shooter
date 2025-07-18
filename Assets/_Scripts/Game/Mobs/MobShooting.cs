using UnityEngine;

public class MobShooting : MonoBehaviour
{
    [SerializeField] private GameObject _currentWeaponPrefab;

    private float _shootTimer;
    private const float ShootTimerMax = 2f;

    private MobMovement _mobMovement;
    private Mob _mob;

    private void Start()
    {
        _mobMovement = GetComponent<MobMovement>();
        _mob = GetComponent<Mob>();
    }

    private void Update()
    {
        _shootTimer -= Time.deltaTime;
        if (_shootTimer < 0 && _mobMovement.IsInShootRange)
        {
            Shoot();
            _shootTimer = ShootTimerMax;
        }
    }

    private void Shoot()
    {
        if (_currentWeaponPrefab && _mob.Target != null)
        {
            Vector3 rawDirection = _mob.Target.position - transform.position;
            //Vector2 snappedDirection = GetSnappedDirection(rawDirection);

            _currentWeaponPrefab.TryGetComponent(out BaseWeapon weapon);
            if (weapon != null)
            {
                weapon.Shoot(rawDirection);
            }
        }
    }

    private Vector2 GetSnappedDirection(Vector3 rawDirection)
    {        
        Vector2 direction = rawDirection.normalized;
       
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;
      
        float snappedAngle = Mathf.Round(angle / 45f) * 45f;
      
        float snappedAngleRad = snappedAngle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(snappedAngleRad), Mathf.Sin(snappedAngleRad)).normalized;
    }
}
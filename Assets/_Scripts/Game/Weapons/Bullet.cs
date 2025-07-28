using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AmmoSO _bulletTypeSO;

    private Rigidbody2D rb;
    private Vector2 direction;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, _bulletTypeSO.Lifetime);
    }

    public void Initialize(Vector2 shootDirection)
    {
        PlayBulletSound();

        if (shootDirection == Vector2.zero)
        {
            shootDirection = Vector2.right; // by default we shoot to the right
        }
        direction = shootDirection.normalized;
        
       
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        
       rb.linearVelocity = direction * _bulletTypeSO.Speed;
    }

    void FixedUpdate()
    {
       rb.linearVelocity = direction * _bulletTypeSO.Speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.TryGetComponent(out IDamageable damageable) && !other.CompareTag(gameObject.tag))
        {

            bool isCrit = Random.Range(0, 100) < PlayerData.Instance.GetCritChance();

            if (isCrit)
                //Deal bullet damage + additional damage from player stats + crit damage
                damageable.TakeDamage((_bulletTypeSO.Damage + PlayerData.Instance.GetDamage()) * 2);
            else
                //Deal bullet damage + additional damage from player stats
                damageable.TakeDamage(_bulletTypeSO.Damage + PlayerData.Instance.GetDamage());

            Destroy(gameObject);
        }
        else if(other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    private void PlayBulletSound()
    {
        switch (_bulletTypeSO.ItemName)
        {
            case "RevolverAmmo":
                AudioManager.Instance.Play("RevolverShot");
                break;
            case "ShotgunAmmo":
                AudioManager.Instance.Play("ShotgunShot");
                break;
            case "RifleAmmo":
                AudioManager.Instance.Play("RifleShot");
                break;
            case "BlasterAmmo":
                AudioManager.Instance.Play("BlasterShot");
                break;
            case "ElectroGunAmmo":
                AudioManager.Instance.Play("ElectroGunShot");
                break;
            case "MachinegunAmmo":
                AudioManager.Instance.Play("MachinegunShot");
                    break;
            default:
                break;
        }
    }

}
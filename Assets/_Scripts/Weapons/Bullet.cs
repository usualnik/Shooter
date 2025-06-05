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
        if (shootDirection == Vector2.zero)
        {
            shootDirection = Vector2.right; // by default we shoot to the right
        }
        direction = shootDirection.normalized;
        
       
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        
       rb.velocity = direction * _bulletTypeSO.Speed;
    }

    void FixedUpdate()
    {
       rb.velocity = direction * _bulletTypeSO.Speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.TryGetComponent(out IDamageable damageable) && !other.CompareTag("Player"))
        {
           damageable.TakeDamage(_bulletTypeSO.Damage);
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Настройки пули")]
    [SerializeField] private float speed = 10f; 
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 10;
    
    private Rigidbody2D rb;
    private Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var collider = GetComponent<Collider2D>();
        
        Destroy(gameObject, lifetime);
    }

    public void Initialize(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        
       
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        
        rb.velocity = direction * speed;
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.TryGetComponent(out IDamageable damageable) && !other.CompareTag("Player"))
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
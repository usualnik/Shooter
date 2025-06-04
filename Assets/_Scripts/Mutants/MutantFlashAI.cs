using UnityEngine;

public class MutantFlashAI : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 2f;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private int _damage = 20;

    private Player _player;
    private Transform _playerTransform;
    private Rigidbody2D _rb;
    private MobState _currentState;
    private float _mutantAttackCooldownTimer;
    private const float MutantAttackCoolDownMax = 3f;

    private enum MobState { Idle, Chase, Attack }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _player = GameObject.FindObjectOfType<Player>();
        _playerTransform = _player.transform;
        
    }

    private void Update()
    {
        if (!_player)
        {
            return;
        }
        
        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);

        switch (_currentState)
        {
            case MobState.Idle:
                if (distanceToPlayer < _detectionRange)
                    _currentState = MobState.Chase;
                break;
                
            case MobState.Chase:
                ChasePlayer();
                if (distanceToPlayer <= _attackRange)
                    _currentState = MobState.Attack;
                break;
                
            case MobState.Attack:
                AttackPlayer();
                if (distanceToPlayer > _attackRange)
                    _currentState = MobState.Chase;
                break;
        }
        
        
    }

    private void ChasePlayer()
    {
        Vector2 direction = (_playerTransform.position - transform.position).normalized;
        _rb.velocity = direction * _moveSpeed;
    }

    private void AttackPlayer()
    {
        _mutantAttackCooldownTimer -= Time.deltaTime;
        _rb.velocity = Vector2.zero;
        
        if (_mutantAttackCooldownTimer <= 0)
        {
            _player.TakeDamage(_damage);
       
            Debug.Log($"Mutant attacked player! Damage: {_damage}");
            
            _mutantAttackCooldownTimer = MutantAttackCoolDownMax;
        }

        
      
    }


}
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    public bool IsInShootRange { get; private set; }
    public event System.Action<object, Vector2> OnChangeLookDir;

    [Header("Movement Settings")]
    [SerializeField] private float _detectionRange = 2f;
    [SerializeField] private float _moveSpeed = 2f;
    
    
    [SerializeField] private LayerMask _obstacleLayer; 

    private Rigidbody2D _rb;
    private Mob _mob;
    private MobState _currentState;
    private float _strafeEndTime;
    private Vector2 _currentStrafeDirection;
    private Vector2 _lastAvoidDirection;
    private float _obstacleAvoidDistance;

    private float _shootDistance;
    private float _shootRangeBuffer;
    private float _strafeDuration;

    private enum MobState { Idle, Chase, Strafe }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mob = GetComponent<Mob>();

        _lastAvoidDirection = Vector2.right;
        _obstacleAvoidDistance = Random.Range(1, 10);

        _shootDistance = Random.Range(10, 15);
        _shootRangeBuffer = Random.Range(1, 3);
        _strafeDuration = Random.Range(1, 3);
               
    }

    private void Update()
    {
        MobBehaviour();
    }

    private void MobBehaviour()
    {
        if (_mob.Target == null)
            return;

        float distanceToTarget = Vector2.Distance(transform.position, _mob.Target.position);

        switch (_currentState)
        {
            case MobState.Idle:
                if (distanceToTarget < _detectionRange)
                    _currentState = MobState.Chase;
                break;

            case MobState.Chase:
                ChaseTarget(distanceToTarget);
                break;

            case MobState.Strafe:
                Strafe(distanceToTarget);
                break;
        }
    }

    private void ChaseTarget(float distance)
    {
        if (distance < _shootDistance - _shootRangeBuffer)
        {
            IsInShootRange = true;
            _rb.linearVelocity = Vector2.zero;
            _currentState = MobState.Strafe;
            _strafeEndTime = Time.time + _strafeDuration;
            return;
        }

        IsInShootRange = false;
        Vector2 direction = (_mob.Target.position - transform.position).normalized;
       

        Vector2 avoidDirection = GetObstacleAvoidanceDirection(direction);

        OnChangeLookDir?.Invoke(this, avoidDirection);

        _rb.linearVelocity = avoidDirection * _moveSpeed;

       
        
    }

    private void Strafe(float currentDistance)
    {
        if (Time.time >= _strafeEndTime || currentDistance > _shootDistance + _shootRangeBuffer)
        {
            IsInShootRange = false;
            _currentState = MobState.Chase;
            return;
        }

        if (Time.frameCount % 60 == 0)
        {
            _currentStrafeDirection = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;
        }
        
        Vector2 avoidDirection = GetObstacleAvoidanceDirection(_currentStrafeDirection);

        OnChangeLookDir?.Invoke(this, avoidDirection);

        _rb.linearVelocity = avoidDirection * _moveSpeed;


    }


    private Vector2 GetObstacleAvoidanceDirection(Vector2 desiredDirection)
    {
        Vector2 boxCastSize = _mob.IsBoss ? new Vector2(2.3f, 2.3f) : new Vector2(2, 2);

        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            boxCastSize,
            0,
            desiredDirection,
            _obstacleAvoidDistance,
            _obstacleLayer
        );

        if (hit.collider != null)
        {            
            Vector2 avoidDir = Vector2.Perpendicular(hit.normal);

            float dotProduct = Vector2.Dot(avoidDir, _lastAvoidDirection);

            if (dotProduct < 0)
            {
                avoidDir *= -1;
            }

            _lastAvoidDirection = avoidDir; 
            return avoidDir.normalized;
        }
        
        _lastAvoidDirection = desiredDirection;
       
        return desiredDirection;
    }

}
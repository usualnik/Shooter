using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public event System.Action<object, Vector2> OnChangeLookDir;

    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed = 5f;
    

    private Joystick _joystick;
    private Rigidbody2D _rigidbody;
    private Vector2 _inputVector;
    private Vector2 _lastLookDir;
    private bool _isFacingRight;

    void Awake()
    {
        _joystick = GameObject.FindGameObjectWithTag("MovementJoystick")?.GetComponent<Joystick>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        MoveCharacter();
        CheckDirectionChange();
    }

    private void GetInput()
    {
        _inputVector = GameManager.Instance.IsMobilePlatform
            ? new Vector2(_joystick.Horizontal, _joystick.Vertical).normalized
            : new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void MoveCharacter()
    {
        _rigidbody.linearVelocity = _inputVector * _movementSpeed;
    }

    private void CheckDirectionChange()
    {
        
        if (_inputVector.magnitude < 0.1f)
        {
            if (_lastLookDir != Vector2.zero)
            {
                _lastLookDir = Vector2.zero;
                OnChangeLookDir?.Invoke(this, Vector2.zero);
            }
            return;
        }

        Vector2 primaryDirection = Mathf.Abs(_inputVector.x) > Mathf.Abs(_inputVector.y)
            ? new Vector2(_inputVector.x, 0)
            : new Vector2(0, _inputVector.y);

        if (primaryDirection != _lastLookDir)
        {
            _lastLookDir = primaryDirection;
            OnChangeLookDir?.Invoke(this, primaryDirection.normalized);
        }



    }
    public Vector2 GetLookDirection() => _lastLookDir;
}
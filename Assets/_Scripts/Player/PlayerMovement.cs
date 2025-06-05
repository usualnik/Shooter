using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{ 
    public event Action<object, Vector2> OnChangeLookDir;

    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed = 5f;
    
    [Header("Joystick Reference")]
    [SerializeField] private Joystick _joystick; 
    
    
    
    private Rigidbody2D _rigidbody;
    private Vector2 _inputVector;
    private Vector2 _lookDir;
    private Vector2 _smoothInputVelocity;
    private bool _isMobile;

    private SpriteRenderer[] _playerVisuals;
    private bool _isFacingRight;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;
        _playerVisuals = gameObject.GetComponentsInChildren<SpriteRenderer>();
        
        
        // _isMobile = Application.isMobilePlatform;
        _isMobile = true; 
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void GetInput()
    {
        if (_isMobile)
        {
            _inputVector = new Vector2(_joystick.Horizontal, _joystick.Vertical);
            _inputVector = _inputVector.normalized;
        }
        else
        {
            
            _inputVector = new Vector2(
                Input.GetAxisRaw("Horizontal"), 
                Input.GetAxisRaw("Vertical"));
            
            _inputVector = _inputVector.normalized;
        }
    }

    private void MoveCharacter()
    {
        _rigidbody.velocity = _inputVector * _movementSpeed;
        
        RotateTowardsInput();
        
    }

    private void RotateTowardsInput()
    {
        if (_inputVector.x != 0)
            _lookDir = _inputVector.normalized;
        
        if (!(_inputVector.magnitude > 0.1f)) return; // ignore deadzone 
        
        bool newDirectionRight = _inputVector.x > 0;
        if (newDirectionRight != _isFacingRight)
        {
            _isFacingRight = newDirectionRight;
            OnChangeLookDir?.Invoke(this, _inputVector);
        }
    }
    public Vector2 GetLookDirection() => _lookDir;
}
    

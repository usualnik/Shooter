using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator _animator;   

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _playerMovement.OnChangeLookDir += HandleLookDirectionChange;
    }

    private void OnDestroy()
    {
        _playerMovement.OnChangeLookDir -= HandleLookDirectionChange;
    }

    private void HandleLookDirectionChange(object sender, Vector2 direction)
    { 
             


        _animator.SetBool("IsWalkingLeft", false);
        _animator.SetBool("IsWalkingRight", false);
        _animator.SetBool("IsWalkingStraight", false);

        
        if (direction == Vector2.zero)
        {
            _animator.SetBool("IsWalking", false);
            return;
        }
        
        _animator.SetBool("IsWalking", true);

        
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            _animator.SetBool(direction.x > 0 ? "IsWalkingRight" : "IsWalkingLeft", true);
        }
        else
        {
            _animator.SetBool("IsWalkingStraight", true);
        }

               
    }
}
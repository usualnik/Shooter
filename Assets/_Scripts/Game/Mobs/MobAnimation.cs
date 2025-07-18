using UnityEngine;

public class MobAnimation : MonoBehaviour
{
    private MobMovement _mobMovement;
    private Animator _animator;

    private void Awake()
    {
        _mobMovement = GetComponent<MobMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _mobMovement.OnChangeLookDir += HandleLookDirectionChange;
    }

    private void OnDestroy()
    {
        _mobMovement.OnChangeLookDir -= HandleLookDirectionChange;
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

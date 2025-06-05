using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private const string IsLookingRight = "IsLookingRight";
    private PlayerMovement _playerMovement;
    private Animator _animator;
    private bool _lastLookRightState;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _lastLookRightState = true; // Начинаем смотрящим вправо
    }

    private void Start()
    {
        _playerMovement.OnChangeLookDir += HandleLookDirectionChange;
        UpdateAnimation(true); // Инициализация
    }
    private void OnDestroy()
    {
        _playerMovement.OnChangeLookDir -= HandleLookDirectionChange;
    }

    private void HandleLookDirectionChange(object sender, Vector2 direction)
    {
        // Определяем направление взгляда (право/лево)
        bool shouldLookRight = direction.x > 0;
        
        // Обновляем только при изменении состояния
        if (shouldLookRight != _lastLookRightState)
        {
            UpdateAnimation(shouldLookRight);
            _lastLookRightState = shouldLookRight;
        }
    }

    private void UpdateAnimation(bool lookRight)
    {
        _animator.SetBool(IsLookingRight, lookRight);
    }
}
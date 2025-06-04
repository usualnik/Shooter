using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private Player _player;
      private void Start()
    {
        _player = gameObject.GetComponentInParent<Player>();
        _player.OnTakingDamage += Player_OnTakingDamage;
        UpdateHealthBar();
    }

    private void OnDestroy()
    {
        _player.OnTakingDamage -= Player_OnTakingDamage;
    }


    private void Player_OnTakingDamage(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }


    private void UpdateHealthBar()
    {
        _healthBar.fillAmount = _player.GetCurrentHealth() / _player.GetMaxHealthConst();
    }



}


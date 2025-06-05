using UnityEngine;
using System;
using UnityEngine.UI;

public class UI_PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private Player _player;
    private float _updateDelay = 0.1f;
    
      private void Start()
    {
        _player = gameObject.GetComponentInParent<Player>();
        _player.OnTakingDamage += Player_OnTakingDamage;
        
        
        Invoke(nameof(UpdateHealthBar),_updateDelay); // delays ui update (load system can load values first)
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
        _healthBar.fillAmount = _player.GetCurrentHealth() / Player.GetMaxHealthConst();
    }



}


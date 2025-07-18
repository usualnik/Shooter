using UnityEngine;
using System;
using UnityEngine.UI;

public class UI_MutantHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private Mob _mob;

    private void Awake()
    {
        _mob = GetComponentInParent<Mob>();
    }

    private void Start()
    {
        UpdateHealthBar();
        
        _mob.OnTakingDamage += Mob_OnTakingDamage;
        _mob.OnRespawn += Mob_OnRespawn;
    }

 

    private void OnDestroy()
    {
        _mob.OnTakingDamage -= Mob_OnTakingDamage;
        _mob.OnRespawn -= Mob_OnRespawn;
    }

    private void Mob_OnTakingDamage(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }
    private void Mob_OnRespawn(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _healthBar.fillAmount = _mob.GetCurrentHealth() / Mob.GetMaxHealthConst();
    }



}
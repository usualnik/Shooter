using UnityEngine;
using System;
using UnityEngine.UI;

public class MutantHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private BaseMutant _mutant;

    private void Awake()
    {
        _mutant = GetComponentInParent<BaseMutant>();
    }

    private void Start()
    {
        UpdateHealthBar();
        
        _mutant.OnTakingDamage += Mutant_OnTakingDamage;
    }

    private void OnDestroy()
    {
        _mutant.OnTakingDamage -= Mutant_OnTakingDamage;
    }

    private void Mutant_OnTakingDamage(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }


    private void UpdateHealthBar()
    {
        _healthBar.fillAmount = _mutant.GetCurrentHealth() / _mutant.GetMaxHealthConst();
    }



}
using System;
using UnityEngine;

public class Player : MonoBehaviour , IDamageable
{ 
    private const int MaxHealth = 100;
    private int _currentHealth;

    public event EventHandler OnTakingDamage; 
    
    private void Awake()
    {
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnTakingDamage?.Invoke(this,EventArgs.Empty);
       
        if (_currentHealth <= 0)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    
    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
    public float GetMaxHealthConst()
    {
        return MaxHealth;
    }

    public void SetHealth(in int playerHealth)
    {
        _currentHealth = playerHealth;
    }
}

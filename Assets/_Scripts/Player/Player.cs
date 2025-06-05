using System;
using UnityEngine;

public class Player : MonoBehaviour , IDamageable
{ 
    [SerializeField]private int _currentHealth;
    private const int MaxHealth = 100;
    
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

    public int GetCurrentHealth() => _currentHealth;
    public static float GetMaxHealthConst() => MaxHealth;

    public void SetHealth(in int playerHealth)
    {
        _currentHealth = playerHealth;
    }
}

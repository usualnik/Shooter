using System;
using UnityEngine;

public class BaseMutant : MonoBehaviour , IDamageable
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private BaseItemSO _mutantDrop;
    
    
    private const int MaxHealth = 100;
    private const float DeathTimer = 0.1f;
    
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
        //Animation
        
        DropItem();
        
        Destroy(gameObject, DeathTimer);
    }

    private void DropItem()
    {
        Instantiate(_mutantDrop.ItemPrefab, transform.position, Quaternion.identity);
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
    public float GetMaxHealthConst()
    {
        return MaxHealth;
    }
}

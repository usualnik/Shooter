using System;
using UnityEngine;

public class Player : MonoBehaviour , IDamageable
{
    public event EventHandler OnTakingDamage;
    public event EventHandler OnRespawn;
    public event EventHandler OnTakingHealing;

    public bool IsBoss { get; private set; }

    [SerializeField]private float _currentHealth;
    [SerializeField] private Transform _gunTransform;
   
    
    private const int HealAmount = 20;
    private const int PlayerBossMaxHealth = 5000;

    private int MaxHealth;
    private float _healingTimer;
    private const float HealingTimerMax = 3f;

    private PlayerShooting _playerShooting;
    
  

    private void Awake()
    {

        _playerShooting = GetComponent<PlayerShooting>();

        MaxHealth = PlayerData.Instance.GetHealth();

        


        _currentHealth = MaxHealth;
        _healingTimer = HealingTimerMax;

        EquipGun();
    }

    private void Start()
    {
        IsBoss = GameManager.Instance.Mode == GameManager.GameMode.PlayerBoss ? true : false;

        if (IsBoss)
        {
            _currentHealth = PlayerBossMaxHealth;          
        }        
    }


    private void EquipGun()
    {
        GameObject currentWeapon = Instantiate(PlayerData.Instance.GetCurrentGunSO().ItemPrefab, _gunTransform);
        _playerShooting.SetCurrentGun(currentWeapon);
    }

    private void Update()
    {
        if(!IsBoss && _currentHealth < MaxHealth)
        {
            StartHealingTimer();
        }
    }

    private void StartHealingTimer()
    {
        _healingTimer -= Time.deltaTime;
        if (_healingTimer <= 0) 
        {
            _currentHealth += HealAmount;
            OnTakingHealing?.Invoke(this, EventArgs.Empty);
            _healingTimer = HealingTimerMax;
            //StartCoroutine(TakeHealing());
        }
    }
   
    public void TakeDamage(float damage)
    {
        AudioManager.Instance.Play("Bullet");

        _currentHealth -= damage;


        //Reset healing timer, so we cant heal when taking damage
        _healingTimer = HealingTimerMax;

        OnTakingDamage?.Invoke(this,EventArgs.Empty);

        if (IsBoss)
            UI_BoosHPBar.Instance.UpdateHealthBar(this);
       
        if (_currentHealth <= 0)
        {
            GameUIManager.Instance.AddScore(gameObject.tag);
            GameUIManager.Instance.ShowPlayerDeathPanel();
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        if (IsBoss)
            GameManager.Instance.KillBossEndGame(true);

        gameObject.SetActive(false);
        
        //reset health, so we can respawn with full
        _currentHealth = MaxHealth;
        OnRespawn?.Invoke(this,EventArgs.Empty);

        MobSpawner.Instance.ReaspawnUnit(gameObject,gameObject.tag);
    }

    public float GetCurrentHealth() => _currentHealth;
    public float GetMaxHealth() => MaxHealth;
    public static float GetPlayerBossMaxHealthConst() => PlayerBossMaxHealth;

    public void SetHealth(in int playerHealth)
    {
        _currentHealth = playerHealth;
    }
   
}

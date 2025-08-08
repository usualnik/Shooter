using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mob : MonoBehaviour , IDamageable
{
    public event EventHandler OnTakingDamage;
    public event EventHandler OnRespawn;
    public Transform _target;

    public Transform Target {  get; private set; }
    public bool IsBoss { get; private set; }

    [SerializeField] private float _currentHealth;  
    
    
    private const int MaxHealth = 100;
    private const int MobBossMaxHealth = 5000;

    //private const float DeathTimer = 0.1f;

    private MobShooting _mobShooting;

    private float _changeTargetTimer;
    private const float ChangeTargetTimerMax = 2f;

    
    private void Awake()
    {
        _currentHealth = MaxHealth;
        _mobShooting = GetComponent<MobShooting>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameEneded += GameManager_EndGameBehaviour;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnGameEneded -= GameManager_EndGameBehaviour;
    }

    private void GameManager_EndGameBehaviour()
    {
       _mobShooting.enabled = false;
    }


    public void TakeDamage(float damage)
    {
        AudioManager.Instance.Play("Bullet");

        _currentHealth -= damage;
        OnTakingDamage?.Invoke(this,EventArgs.Empty);

        if (IsBoss)
            UI_BoosHPBar.Instance.UpdateHealthBar(this);

        if (_currentHealth <= 0)
        {
            GameUIManager.Instance.AddScore(gameObject.tag);
            DestroySelf();
        }
    }

    private void Update()
    {
        _target = Target;

        _changeTargetTimer -= Time.deltaTime;
        if (_changeTargetTimer <= 0)
        {
            ChangeTarget();
            _changeTargetTimer = ChangeTargetTimerMax;
        }
    }

    private void ChangeTarget()
    {
        Unit[] possibleTargets = GameObject.FindObjectsByType<Unit>(FindObjectsSortMode.None);
        
        List<Unit> validTargets = new List<Unit>();
        foreach (Unit target in possibleTargets)
        {
            if (target != null && target.gameObject.tag != gameObject.tag)
            {
                validTargets.Add(target);
            }
        }
        
        if (validTargets.Count == 0)
        {
            Target = null;
            return;
        }

        float randomChance = 0.2f;
        if (Random.value < randomChance && validTargets.Count > 1)
        {
           
            int randomIndex = Random.Range(0, validTargets.Count);
            Target = validTargets[randomIndex].transform;
        }
        else
        {
           
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (Unit target in validTargets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target.transform;
                }
            }

            Target = closestTarget;
        }
    }

    private void DestroySelf()
    {
        AudioManager.Instance.Play("UnitDeath");


        if (IsBoss)
            GameManager.Instance.KillBossEndGame(true);

        //Destroy(gameObject, DeathTimer);
        gameObject.SetActive(false);

        //reset health, so we can respawn with full
        _currentHealth = MaxHealth;
        OnRespawn?.Invoke(this, EventArgs.Empty);

        MobSpawner.Instance.ReaspawnUnit(gameObject, gameObject.tag);
    }
    public void SetBoss(bool value)
    {
        IsBoss = value;
        _currentHealth = MobBossMaxHealth;
    }  
    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
    public static float GetMaxHealthConst()
    {
        return MaxHealth;
    }
    public static float GetMobBossMaxHealthConst()
    {
        return MobBossMaxHealth;
    }
}

using System;
using UnityEngine;
using YG;

[System.Serializable]
public class Data
{
    public bool IsFirstTimePlayed;

    public WeaponSO WeaponData;
    public SkinDataSO SkinData;

    public string NickName = "NickName";

    public int Rating = 99999;
        
    //Currency
    public int SoftCurrency = 0;
    public int HardCurrency = 0;
  
    //Level
    public int Level = 1;
    public int CurrentExperience = 0;
    public float MaxExperience = 400;

    //Stats
    public int LevelPoints = 0;
    public int Health = 100;
    public float Damage = 25;
    public int CritChance = 1;
    public float AttackSpeed = 0.4f;
        
}

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    public event Action OnLevelUp;
    public event Action OnStatChanged;
    public event Action OnPlayerSkinChanged;
    public event Action OnPlayerHardCurrencyChanged;
    public event Action OnPlayerSoftCurrencyChanged;


    [SerializeField] private Data data;

    private const float ExperienceMultiplier = 1.3f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameObject.transform.SetParent(null, false);
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        // Init rating for the first time 
        if (data.Rating == 99999)
        {
            InitRating();

            //Set player to first place in rating table, if he deserve it
        } else if (data.Rating <= 0)
        {
            data.Rating = 1;
        }
    }

    private void InitRating()
    {
        data.Rating = UnityEngine.Random.Range(22283, 25381);
    }

    private void LevelUp()
    {
        data.Level++;
        data.CurrentExperience = 0;
        data.MaxExperience *= ExperienceMultiplier;
        data.LevelPoints += 5;

        OnLevelUp?.Invoke();
    }
   
    public void SetPlayerWeaponData(WeaponSO weaponData)
    {
        data.WeaponData = weaponData;
    }


    #region Get
    public int GetLevelPoints() => data.LevelPoints;
    public int GetHealth() => data.Health;
    public float GetDamage() => data.Damage;
    public int GetCritChance() => data.CritChance;
    public float GetAttackSpeed() => data.AttackSpeed;
    public int GetPlayerLevel() => data.Level;
    public int GetCurrentLevelProgress() => data.CurrentExperience;
    public float GetLevelProgressMax() => data.MaxExperience;
    public int GetSoftCurrency() => data.SoftCurrency;
    public int GetHardCurrency() => data.HardCurrency;
    public int GetRating() => data.Rating;
    public WeaponSO GetCurrentGunSO() => data.WeaponData;
    public string GetNickName() => data.NickName;

    public SkinDataSO GetSkinDataSO() => data.SkinData;

    public bool GetIsFirstTimePlayed() => data.IsFirstTimePlayed;

    #endregion

    #region Add
    public void AddHealth(int value) 
    {
        if(data.LevelPoints > 0)
        {
            data.LevelPoints--;
            data.Health += value;

            OnStatChanged?.Invoke();
        }
       
    }
    public void AddDamage(float value)
    {
        if (data.LevelPoints > 0)
        {
            data.LevelPoints--;
            data.Damage += value;

            data.Damage = Mathf.Round(data.Damage * 100f) / 100f; // avoid floating value calculation mistake

            OnStatChanged?.Invoke();
        }

    }
    public void AddCritChance(int value) 
    {
        if (data.LevelPoints > 0 && data.CritChance < 100)
        {
            data.LevelPoints--;
            data.CritChance += value;

            OnStatChanged?.Invoke();
        }
    }
    public void AddAttackSpeed(float value)
    {
        if (data.LevelPoints > 0 && data.AttackSpeed > 0.2f)
        {            
            data.LevelPoints--;
            data.AttackSpeed += value;

            data.AttackSpeed = Mathf.Round(data.AttackSpeed * 100f) / 100f; // avoid floating value calculation mistake

            OnStatChanged?.Invoke();
        }
    }

    public void AddExperience(int value) 
    { 
        data.CurrentExperience += value; 
        if(data.CurrentExperience >= data.MaxExperience)
        {
            LevelUp();
        }
    }
    public void AddSoftCurrency(int value)
    {
        data.SoftCurrency += value;

        YG2.saves.SoftCurrency = data.SoftCurrency; // Save value to yandex cloud
        YG2.SaveProgress();

        OnPlayerSoftCurrencyChanged?.Invoke();
    }
    public void AddHardCurrency(int value) 
    { 
        data.HardCurrency += value; 

        YG2.saves.HardCurrency = data.HardCurrency; // Save value to yandex cloud
        YG2.SaveProgress();

        OnPlayerHardCurrencyChanged?.Invoke();
    }
    public void GainRating(int value) => data.Rating += -value;
    #endregion


    #region Set
    public void SetNickname(string nickname) => data.NickName = nickname;
    public void SetSkinData(SkinDataSO skinDataSO) 
    {
        data.SkinData = skinDataSO;

        OnPlayerSkinChanged?.Invoke();
    }

    //Used by Yandex save/load system
    public void SetSoftCurrency(int value) { data.SoftCurrency = value; }
    public void SetHardCurrency(int value) { data.HardCurrency = value; }
    public void SetFirstTimePlayed(bool value)
    {
        data.IsFirstTimePlayed = value;
    }

    #endregion

}

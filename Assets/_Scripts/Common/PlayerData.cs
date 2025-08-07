using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

[System.Serializable]
public class Data
{
    public bool IsFirstTimePlayed;

    public WeaponSO WeaponData;
    public SkinDataSO SkinData;        
    
    public string NickName = "NickName";

    //Skins and weapons
    public List<int> UnlockedSkinsIndexes;
    public List<int> UnlockedWeaponsIndexes;

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
    public event Action OnPlayerWeaponChanged;
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
        

        InitIsFirstTimePlayed();

        InitUnlockedSkins();

        InitUnlockedWeapons();    


    }   

    private void InitIsFirstTimePlayed()
    {
        data.IsFirstTimePlayed = YG2.saves.AlreadyPlayed ? false : true;
    }

    private void InitUnlockedSkins()
    {
        data.UnlockedSkinsIndexes = new List<int>();
        data.UnlockedSkinsIndexes.Add(0); // add default skin
    }

    private void InitUnlockedWeapons()
    {
        data.UnlockedWeaponsIndexes = new List<int>();
        data.UnlockedWeaponsIndexes.Add(0); // add default gun   
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
        SavePlayerDataToCloud();
        OnLevelUp?.Invoke();
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
    public List<int> GetUnlockedSkinsIndexesList() => data.UnlockedSkinsIndexes;
    public List<int> GetUnlockedWeaponsIndexesList() => data.UnlockedWeaponsIndexes;

    public bool GetIsFirstTimePlayed() => data.IsFirstTimePlayed;

    #endregion

    #region Add
    public void AddHealth(int value)
    {
        if (data.LevelPoints > 0)
        {
            data.LevelPoints--;
            data.Health += value;
            SavePlayerDataToCloud();
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
            SavePlayerDataToCloud();
            OnStatChanged?.Invoke();
        }

    }
    public void AddCritChance(int value)
    {
        if (data.LevelPoints > 0 && data.CritChance < 100)
        {
            data.LevelPoints--;
            data.CritChance += value;
            SavePlayerDataToCloud();
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
            SavePlayerDataToCloud();
            OnStatChanged?.Invoke();
        }
    }

    public void AddExperience(int value)
    {
        data.CurrentExperience += value;
        if (data.CurrentExperience >= data.MaxExperience)
        {
            LevelUp();
        }
    }
    public void AddSoftCurrency(int value)
    {
        data.SoftCurrency += value;

        //YG2.saves.SoftCurrency = data.SoftCurrency; // Save value to yandex cloud
        //YG2.SaveProgress();
        SavePlayerDataToCloud();

        OnPlayerSoftCurrencyChanged?.Invoke();
    }
    public void AddHardCurrency(int value)
    {
        data.HardCurrency += value;

        //YG2.saves.HardCurrency = data.HardCurrency; // Save value to yandex cloud
        //YG2.SaveProgress();
        SavePlayerDataToCloud();

        OnPlayerHardCurrencyChanged?.Invoke();
    }
    public void GainRating(int value)
    {
        data.Rating += -value;
        SavePlayerDataToCloud();
    }
    public void AddUnlockedSkinIndex(int skinIndex)
    {
        data.UnlockedSkinsIndexes.Add(skinIndex);
        SavePlayerDataToCloud();
    }
    public void AddUnlockedWeaponIndex(int weaponIndex)
    {
        data.UnlockedWeaponsIndexes.Add(weaponIndex);
        SavePlayerDataToCloud();
    }

    #endregion

    #region Set
    public void SetNickname(string nickname)
    {
        data.NickName = nickname;
        SavePlayerDataToCloud();

    }
    public void SetSkinData(SkinDataSO skinDataSO) 
    {
        data.SkinData = skinDataSO;
        SavePlayerDataToCloud();
        OnPlayerSkinChanged?.Invoke();
    }
    
    public void SetFirstTimePlayed(bool firstTimePlayed)
    {
        data.IsFirstTimePlayed = firstTimePlayed;
        SavePlayerDataToCloud();
    }
    public void SetPlayerWeaponData(WeaponSO weaponData)
    {
        data.WeaponData = weaponData;
        SavePlayerDataToCloud();
        OnPlayerWeaponChanged?.Invoke();
    }


    //-----------------------------YANDES CLOUD SAVE|LOAD--------------------------------------------------

    public void SetPlayerData(Data yandexServerData) 
    {
        if (this.data.IsFirstTimePlayed)
        {            
            return;           
        }             
        else
        {
            this.data = yandexServerData;            
        }
    }

    public void SavePlayerDataToCloud()
    {
        YG2.saves.YandexServerData = this.data;
        YG2.SaveProgress();
    }

    #endregion

}

using UnityEngine;

public class GivePlayerDefaultGunAndSkin : MonoBehaviour
{

    [SerializeField] private WeaponSO _weaponSo;
    [SerializeField] private SkinDataSO _skinSo;

    public static GivePlayerDefaultGunAndSkin Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    
    void Start()
    {
        PlayerData.Instance.SetSkinData(_skinSo);
        PlayerData.Instance.SetPlayerWeaponData(_weaponSo);
    }

   
}

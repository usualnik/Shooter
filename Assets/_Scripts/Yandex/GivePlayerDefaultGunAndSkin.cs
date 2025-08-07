using UnityEngine;

public class GivePlayerDefaultGunAndSkin : MonoBehaviour
{
    [SerializeField] private WeaponSO _weaponSo;
    [SerializeField] private SkinDataSO _skinSo;

    
    void Start()
    {
        PlayerData.Instance.SetSkinData(_skinSo);
        PlayerData.Instance.SetPlayerWeaponData(_weaponSo);
    }

   
}

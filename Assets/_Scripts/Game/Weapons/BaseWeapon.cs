using UnityEngine;


public class BaseWeapon : MonoBehaviour
{    
    [SerializeField] protected Transform _shootTransform;
    
    public AmmoSO AmmoSO;
    public WeaponSO GunSO;   


    private void DeactivateSelf()
    {
        gameObject.SetActive(false);
    }

    public virtual void Shoot(Vector2 shootDir)
    {        
        Debug.Log("Base SHOOT");
    }
}
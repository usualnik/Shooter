using UnityEngine;


public class BaseWeapon : MonoBehaviour, IPickable
{
    [SerializeField] private bool CanPick = true;
    
    public AmmoSO AmmoSO;
    public WeaponSO GunSO;
    
    public int AmmoLeft;

    private void Awake()
    {
        AmmoLeft = AmmoSO.Amount;
    }
    
    public BaseItemSO PickItem()
    {
        if (CanPick)
        {
            Invoke(nameof(DeactivateSelf),0.1f);
            return GunSO;
        }

        return null;
    }

    private void DeactivateSelf()
    {
        gameObject.SetActive(false);
    }

    public virtual void Shoot(Vector2 shootDir)
    {
        Debug.Log("Base SHOOT");
    }
}
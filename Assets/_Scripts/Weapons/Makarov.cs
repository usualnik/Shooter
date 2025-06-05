using UnityEngine;

public class Makarov : BaseWeapon
{
    public override void Shoot(Vector2 shootDir)
    {
        if (AmmoLeft <= 0)
        {
            Debug.Log("You need ammo");
            return;
        }

        AmmoLeft--;
        
        GameObject bulletInstance = Instantiate(
            AmmoSO.ItemPrefab, 
            transform.position, 
            Quaternion.identity
        );
        
        if (bulletInstance.TryGetComponent<Bullet>(out var bullet))
        {
            bullet.Initialize(shootDir);
        }
        else
        {
            Debug.LogError("Bullet obj is missing");
        }
    }
}
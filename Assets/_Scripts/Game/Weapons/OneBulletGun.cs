using UnityEngine;

public class OneBulletGun : BaseWeapon
{

    private void Start()
    {
        gameObject.tag = GetComponentInParent<Unit>().gameObject.tag;
    }

    public override void Shoot(Vector2 shootDir)
    {
        //if (AmmoLeft <= 0)
        //{
        //    Debug.Log("You need ammo");
        //    return;
        //}

        //AmmoLeft--;

        GameObject bulletInstance = Instantiate(
            AmmoSO.ItemPrefab,
            _shootTransform.position,
            Quaternion.identity
        );

        bulletInstance.tag = gameObject.tag;

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

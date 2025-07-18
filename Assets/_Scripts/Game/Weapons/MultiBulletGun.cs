using UnityEngine;

public class MultiBulletGun : BaseWeapon
{
    [SerializeField] private int bulletsToSpawn = 4;
    [SerializeField] private float spreadAngle = 30f;

    private void Start()
    {
        gameObject.tag = GetComponentInParent<Unit>().gameObject.tag;
    }

    public override void Shoot(Vector2 shootDir)
    {
        float startAngle = -spreadAngle / 2f;
       
        float angleStep = spreadAngle / (bulletsToSpawn - 1);

        for (int i = 0; i < bulletsToSpawn; i++)
        {
            GameObject bulletInstance = Instantiate(
                AmmoSO.ItemPrefab,
                _shootTransform.position,
                Quaternion.identity
            );

            bulletInstance.tag = gameObject.tag;

            
            float currentAngle = startAngle + angleStep * i;

           
            Vector2 dir = Quaternion.Euler(0, 0, currentAngle) * shootDir;

            if (bulletInstance.TryGetComponent<Bullet>(out var bullet))
            {
                bullet.Initialize(dir);
                
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bulletInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                Debug.LogError("Bullet obj is missing");
            }
        }
    }
}
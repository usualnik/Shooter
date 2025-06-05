using UnityEngine;

[CreateAssetMenu(fileName = "MobSpawnConfig", menuName = "SpawnConfig/MobSpawnConfig")]
public class MobSpawnConfig : ScriptableObject
{
    public GameObject mobPrefab;
    public int initialMobCount = 3;
    public float spawnRadius = 10f;
}

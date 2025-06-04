using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobSpawnConfig", menuName = "Game/MobSpawnConfig")]
public class MobSpawnConfig : ScriptableObject
{
    public GameObject mobPrefab;
    public int initialMobCount = 3;
    public float spawnRadius = 10f;
}

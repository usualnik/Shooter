using UnityEngine;
using System.Collections.Generic;

public class MutantSpawner : MonoBehaviour
{
    [SerializeField] private MobSpawnConfig _config;
    [SerializeField] private Transform _player;
    
    private List<GameObject> _spawnedMobs = new List<GameObject>();

    private void Start()
    {
        SpawnInitialMobs();
    }

    public void SpawnInitialMobs()
    {
        for (int i = 0; i < _config.initialMobCount; i++)
        {
            Vector2 randomPos = GetRandomSpawnPosition();
            var mob = Instantiate(_config.mobPrefab, randomPos, Quaternion.identity);
            _spawnedMobs.Add(mob);
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        return (Vector2)_player.position + randomDir * _config.spawnRadius;
    }

    public void Cleanup()
    {
        foreach (var mob in _spawnedMobs)
        {
            if (mob != null) Destroy(mob);
        }
        _spawnedMobs.Clear();
    }
}
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MobSpawner : MonoBehaviour
{

    public static MobSpawner Instance {  get; private set; }
   
    [Header("Spawn prefabs")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject[] _mobsPrefabs;
    [SerializeField] private GameObject _playerBossPrefab;
    [SerializeField] private GameObject _mobBossPrefab;


    [Header("Spawn Points")]
    [SerializeField] private GameObject _playerSpawnPoint;
    [SerializeField] private GameObject _enemySpawnPoint; 
    private List<GameObject> _spawnedMobs = new List<GameObject>();

    private const string PlayerTeamTag = "PlayerTeam";
    private const string EnemyTeamTag = "EnemyTeam";

    private const float RespawnTimer = 5f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one instance of mobspawner");
    }

    private void Start()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStarted -= GameManager_OnGameStarted;
    }

    private void GameManager_OnGameStarted(GameManager.GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameManager.GameMode.None:
                break;
            case GameManager.GameMode.ThreeVsThree:
                SpawnThreeVsThreeSetup();
                break;
            case GameManager.GameMode.PlayerBoss: 
                SpawnPlayerBossSetup();
                break;
            case GameManager.GameMode.MobBoss:
                SpawnMobBossSetup();
                break;
            default:
                break;
        }
    }

    private void SpawnMobBossSetup()
    {
        SpawnPlayer();

        GameObject mobBoss = Instantiate(_mobBossPrefab, _enemySpawnPoint.transform.position, Quaternion.identity);
        if (mobBoss.TryGetComponent(out Mob mob))
        {
            mob.SetBoss(true);
        }
        
        for (int i = 0; i < 2; i++)
        {
            Vector3 spawnOffsetY = new Vector3(0, Random.Range(-10, 10), 0);
            GameObject spawned = Instantiate(_mobsPrefabs[i], _playerSpawnPoint.transform.position + spawnOffsetY, Quaternion.identity);
            spawned.tag = PlayerTeamTag;
        }
    }

    private void SpawnPlayerBossSetup()
    {
        Instantiate(_playerBossPrefab, _playerSpawnPoint.transform.position, Quaternion.identity);

        for (int i = 2; i < 5; i++)
        {          
            Vector3 spawnOffsetY = new Vector3(0, Random.Range(-10, 10), 0);
            GameObject spawned = Instantiate(_mobsPrefabs[i], _enemySpawnPoint.transform.position + spawnOffsetY, Quaternion.identity);
            spawned.tag = EnemyTeamTag;
        }

    }
    private void SpawnThreeVsThreeSetup()
    {
        SpawnPlayer();

        int i = 0;      
       
        for (; i < 2; i++)
        {
            //GameObject spawned = Instantiate(_mobsPrefabs[i], _playerSpawnPoint.transform.position 
            //    + new Vector3(Random.Range(-5f,5f), Random.Range(-5f, 5f), 0), Quaternion.identity);

            Vector3 spawnOffsetY = new Vector3(0,Random.Range(-10,10),0);
            GameObject spawned = Instantiate(_mobsPrefabs[i], _playerSpawnPoint.transform.position + spawnOffsetY, Quaternion.identity);
            spawned.tag = PlayerTeamTag;
        }
        for (; i < 5; i++)
        {
            //GameObject spawned = Instantiate(_mobsPrefabs[i], _enemySpawnPoint.transform.position 
            //    + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), Quaternion.identity);

            Vector3 spawnOffsetY = new Vector3(0, Random.Range(-10, 10), 0);
            GameObject spawned = Instantiate(_mobsPrefabs[i], _enemySpawnPoint.transform.position + spawnOffsetY, Quaternion.identity);
            spawned.tag = EnemyTeamTag;
        }
    }

    private void SpawnPlayer()
    {
        //Configure player and spawn
        Instantiate(_playerPrefab, _playerSpawnPoint.transform.position, Quaternion.identity);
    }
    
    public void Cleanup()
    {
        foreach (var mob in _spawnedMobs)
        {
            if (mob != null) Destroy(mob);
        }
        _spawnedMobs.Clear();
    }

    public void ReaspawnUnit(GameObject respawnUnit, string tag)
    {
        switch (tag)
        {
            case PlayerTeamTag:
                StartCoroutine(Respawn(respawnUnit, _playerSpawnPoint.transform));
                break;
            case EnemyTeamTag:
                StartCoroutine(Respawn(respawnUnit, _enemySpawnPoint.transform));
                break;
            default:
                break;
        }
    }
    private IEnumerator Respawn(GameObject respawnUnit, Transform respawnPos)
    {
        yield return new WaitForSeconds(RespawnTimer);
        respawnUnit.SetActive(true);       
        respawnUnit.transform.position = respawnPos.position;
    }





}
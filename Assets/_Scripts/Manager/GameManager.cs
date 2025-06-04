using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MutantSpawner _mobSpawner;
    [SerializeField] private Player _player;
    
    private GameData _currentGameData;

    private void Start()
    {
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void SaveGame()
    {
        _currentGameData = new GameData
        {
            playerHealth = _player.GetCurrentHealth(),
        };
        
        SaveSystem.SaveGame(_currentGameData);
    }

    public void LoadGame()
    {
        _currentGameData = SaveSystem.LoadGame();
        _player.SetHealth(_currentGameData.playerHealth);
    }
}
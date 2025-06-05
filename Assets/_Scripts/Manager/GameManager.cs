using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    private GameData _currentGameData;

    private void Start()
    {
        LoadGame();
        Debug.Log("Loaded Player Health = " + _player.GetCurrentHealth());
    }

    private void OnApplicationQuit()
    {
        SaveGame();
        Debug.Log("Saved Player Health = " + _player.GetCurrentHealth());
    }

    private void SaveGame()
    {
        _currentGameData = new GameData
        {
            playerHealth = _player.GetCurrentHealth(),
        };
        
        SaveSystem.SaveGame(_currentGameData);
    }

    private void LoadGame()
    {
        _currentGameData = SaveSystem.LoadGame();
        _player.SetHealth(_currentGameData.playerHealth);
    }
}
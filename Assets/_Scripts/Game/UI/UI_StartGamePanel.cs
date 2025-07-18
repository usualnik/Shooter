using TMPro;
using UnityEngine;

public class UI_StartGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playersText;

    private const int ThreeVsThreePlayersMax = 6;
    private const int BossPlayersMax = 4;

    private float playersFound = 0;    

    void Start()
    {
        UpdatePlayerText();
    }

    void Update()
    {
        int maxPlayers = (GameManager.Instance.Mode == GameManager.GameMode.ThreeVsThree) ? ThreeVsThreePlayersMax : BossPlayersMax;
       
        if (playersFound < maxPlayers)
        {
            playersFound += Time.deltaTime;
          
            float remaining = maxPlayers - playersFound;
            if (remaining < 2.0f)
            {
                float accelerationFactor = 1.0f + (2.0f - remaining) * 2.0f; 
                playersFound += Time.deltaTime * accelerationFactor;
            }                        
        }
        else
        {
            playersFound = maxPlayers;
        }

        UpdatePlayerText();
    }

    private void UpdatePlayerText()
    {
        int maxPlayers = (GameManager.Instance.Mode == GameManager.GameMode.ThreeVsThree) ? ThreeVsThreePlayersMax : BossPlayersMax;
        _playersText.text = $"{Mathf.FloorToInt(playersFound)}\\{maxPlayers}";
    }
}
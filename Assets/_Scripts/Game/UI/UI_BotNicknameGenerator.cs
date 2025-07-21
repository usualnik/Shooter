using UnityEngine;
using System.Collections.Generic;

public class NicknameGenerator : MonoBehaviour
{
    public static NicknameGenerator Instance;

    // Список возможных никнеймов для ботов
    private List<string> botNicknames = new List<string>()
    {
        "Shadow", "Blaze", "Rogue", "Viper", "Neon",
        "Phantom", "Cyber", "Titan", "Raven", "Ghost",
        "Storm", "Wraith", "Frost", "Hunter", "Vortex"
    };


    private void Awake()
    {
        Instance = this;

        if (Instance != null)
            Destroy(gameObject);
    }

    // Метод для получения случайного ника
    public string GetRandomNickname()
    {
        if (botNicknames.Count == 0)
        {
            Debug.LogWarning("Список ников пуст! Возвращаем стандартное имя.");
            return "Bot";
        }

        int randomIndex = Random.Range(0, botNicknames.Count);
        string nickname = botNicknames[randomIndex];
    
        botNicknames.RemoveAt(randomIndex);

        return nickname;
    }
}
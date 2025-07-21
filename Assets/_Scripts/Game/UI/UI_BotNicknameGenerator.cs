using UnityEngine;
using System.Collections.Generic;

public class NicknameGenerator : MonoBehaviour
{
    public static NicknameGenerator Instance;

    // ������ ��������� ��������� ��� �����
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

    // ����� ��� ��������� ���������� ����
    public string GetRandomNickname()
    {
        if (botNicknames.Count == 0)
        {
            Debug.LogWarning("������ ����� ����! ���������� ����������� ���.");
            return "Bot";
        }

        int randomIndex = Random.Range(0, botNicknames.Count);
        string nickname = botNicknames[randomIndex];
    
        botNicknames.RemoveAt(randomIndex);

        return nickname;
    }
}
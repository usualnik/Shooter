using UnityEngine;
using System.Collections.Generic;

public class NicknameGenerator : MonoBehaviour
{
    public static NicknameGenerator Instance;
        
    private List<string> botNicknames = new List<string>()
    {
        "HeadshotHarry", "NoScopeNeil", "FragMaster", "CampingCarl", "360NoScope",
        "DriftKing", "OneTapTony", "MrStealYoKill", "LagSwitch",
        "WifiWarrior", "PingAbuser", "AimbotAndy", "ToxicTimmy", "CarryMePlz",
        "PotatoAim", "FlashbangFred", "YoloSwag", "TryhardTerry", "CheekiBreeki",
        "BunnyHopGod", "CrouchSpammer", "DesyncDennis", "PeekerAdvantage", "TarkovTurtle", "JohnWick",
        "MLGPro", "ClutchOrKick", "KillConfirmed",
        "������������", "����������", "��������", "�������������", "����������������",
        "����������", "�������������", "���������", "������",
        "���������������", "���������", "������������������", "���������������", "������������",
        "���������", "������������", "�������������", "������������",
        "��������������", "����������������", "�����", "����������������", "����������������",
        "����������", "���������", "��������", "�������", "�����������"
    };

    private void Awake()
    {
        Instance = this;

        if (Instance != null)
            Destroy(gameObject);
    }

   
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
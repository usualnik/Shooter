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
        "ѕул€Ќа»злете", " риворукий", "“упиксон", "„итер¬ќтпуске", "ѕатроны ончились",
        "јгентћафии", "√роза“уалетов", "ƒед»нсайд", "∆ивчик",
        "«аточкаƒл€¬рага", "«лой лоун", " апитанќчевидность", " иллер»зѕодвала", "Ћагћен€Ѕесит",
        "ћимо илла", "ќпасный’ом€к", "ѕаникаЌаЋинии", "–ыцарьƒивана",
        "—найпер—ƒивана", "“анкЌаћинималках", "”пырь", "‘рагер«аƒеньгиƒа", "’ом€кѕод√ранатой",
        "÷ельс€¬ыше", "„ерепажка", "Ўакалист", "я∆оЅоец", "«ќќ √ўј—“№я"
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
            Debug.LogWarning("—писок ников пуст! ¬озвращаем стандартное им€.");
            return "Bot";
        }

        int randomIndex = Random.Range(0, botNicknames.Count);
        string nickname = botNicknames[randomIndex];
    
        botNicknames.RemoveAt(randomIndex);

        return nickname;
    }
}
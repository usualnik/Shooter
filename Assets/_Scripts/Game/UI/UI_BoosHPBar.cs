using UnityEngine;
using UnityEngine.UI;

public class UI_BoosHPBar : MonoBehaviour
{
    public static UI_BoosHPBar Instance {  get; private set; }

    private Image _bossHpImage;   

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one instance of boss hp bar");

            _bossHpImage = GetComponent<Image>();
    }

    public void UpdateHealthBar(Player player)
    {
        _bossHpImage.fillAmount = player.GetCurrentHealth() / Player.GetPlayerBossMaxHealthConst();
    }
    public void UpdateHealthBar(Mob mob)
    {
        _bossHpImage.fillAmount = mob.GetCurrentHealth() / Mob.GetMobBossMaxHealthConst();
    }


}

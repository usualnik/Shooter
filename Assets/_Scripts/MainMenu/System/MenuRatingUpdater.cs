using UnityEngine;

public class MenuRatingUpdater : MonoBehaviour
{
    private float _updateTimer;
    private float _updateTimerMax;




    private void Start()
    {
        _updateTimerMax = Random.Range(40f, 160f);
        _updateTimer = _updateTimerMax;
    }

    private void Update()
    {
        _updateTimer -= Time.deltaTime;
        if (_updateTimer < 0)
        {
            UpdateRating();

        }
    }

    private void UpdateRating()
    {
        PlayerData.Instance.GainRating(Random.Range(-500, 500));

        MainMenuUIManager.Instance.UpdateRatingText();

        _updateTimerMax = Random.Range(40f, 160f);
        _updateTimer = _updateTimerMax;

    }
}

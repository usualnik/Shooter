using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    private const int MainMenuIndex = 1;
    [SerializeField] private Mob _target;
    [SerializeField] private GameObject _loadingScreen;

    private void Start()
    {
        _target.OnRespawn += Target_OnRespawn;
    }
    private void OnDestroy()
    {
        _target.OnRespawn -= Target_OnRespawn;
    }

    private void Target_OnRespawn(object sender, System.EventArgs e)
    {
        _loadingScreen.SetActive(true);

        Invoke("LoadMenu", 0.1f);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene(MainMenuIndex);
    }
}

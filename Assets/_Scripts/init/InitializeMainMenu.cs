using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeMainMenu : MonoBehaviour
{
    private const int MainMenuIndex = 1;

    void Start()
    {
        Init();
    }

    private void Init()
    {
      SceneManager.LoadScene(MainMenuIndex);
    }
}

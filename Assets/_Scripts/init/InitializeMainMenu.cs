using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeMainMenu : MonoBehaviour
{
    private const int MainMenuIndex = 1;
    private const int TutorialIndex = 3;

    void Start()
    {
        // Delay loading, before PlayerData is initialized
        Invoke("Init",0.3f);
    }

    private void Init()
    {
        if (PlayerData.Instance.GetIsFirstTimePlayed())
        {
            SceneManager.LoadScene(TutorialIndex);
        }
        else
        {
            SceneManager.LoadScene(MainMenuIndex);
        }
    }
}

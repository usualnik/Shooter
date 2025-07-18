using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private const int GameSceneIndex = 1;
    public void LoadGameScene()
    {
        SceneManager.LoadScene(GameSceneIndex);
    }
}

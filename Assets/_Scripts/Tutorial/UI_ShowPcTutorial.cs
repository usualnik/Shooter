using UnityEngine;

public class UI_ShowPcTutorial : MonoBehaviour
{

    private void Start()
    {
        Invoke("ShowTut", 1f);
    }

    private void ShowTut()
    {
        if (!GameManager.Instance.IsMobilePlatform)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
 
}

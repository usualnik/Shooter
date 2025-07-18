using UnityEngine;

public class UI_ShowJoystick : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance.IsMobilePlatform)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

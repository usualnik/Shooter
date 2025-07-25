using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    
    void Awake()
    {      
        _audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // if music is muted - mute backgound
    }


}

using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name; 
        public AudioClip clip; 
        [Range(0f, 1f)] public float volume = 1f;
        public bool loop;
        [HideInInspector] public AudioSource source;
    }

    [SerializeField] private List<Sound> sounds = new List<Sound>();
    private bool isMuted = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


        gameObject.transform.SetParent(null);


        DontDestroyOnLoad(gameObject); 

      
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }
  
    public void Play(string soundName)
    {
        Sound s = sounds.Find(sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("«вук " + soundName + " не найден!");
            return;
        }
        s.source.Play();
    }
   
    public void Stop(string soundName)
    {
        Sound s = sounds.Find(sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("«вук " + soundName + " не найден!");
            return;
        }
        s.source.Stop();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;  

        foreach (Sound s in sounds)
        {
            s.source.volume = isMuted ? 0 : s.volume;
        }
    }
   
    public bool IsMuted() => isMuted;
   
}
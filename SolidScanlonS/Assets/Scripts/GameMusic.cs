using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour {
    public AudioClip mainMusic;
    public AudioClip dramaMusic;
    public AudioClip finalBossMusic;
    
    AudioSource source;

    void Start () {
        
        DontDestroyOnLoad(this);
        source = GetComponent<AudioSource>();
        source.clip = mainMusic;
        source.Play();
        
    }
	
	void Update () {
	
	}

    public void PlayMainMusic()
    {
        source.clip = mainMusic;
        source.Play();
    }

    public void StopMusic()
    { 
        source.Stop();
    }
}

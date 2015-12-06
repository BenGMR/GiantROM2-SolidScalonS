using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour {
    public AudioClip bossMusic;
    public AudioClip dramaMusic;
    public AudioClip finalBossMusic;
    public AudioClip titleMusic;
    public AudioClip unlimitedAnime;
    public AudioClip fullUnlimitedAnime;
    public AudioClip creditsMusic;
    public AudioClip dramaticSting;
    
    AudioSource source;

    void Start () {
        
        DontDestroyOnLoad(this);
        source = GetComponent<AudioSource>();
        source.clip = titleMusic;
        source.Play();
        
    }
	
	void Update () {
	
	}

    public void PlayBossMusic()
    {
        source.clip = bossMusic;
        source.Play();
    }

    public void PlayFinalBossMusic()
    {
        source.clip = finalBossMusic;
        source.Play();
    }

    public void PlayIntroMusic()
    {
        source.clip = titleMusic;
        source.Play();
    }

    public void PlayDramaMusic()
    {
        source.clip = dramaMusic;
        source.Play();
    }

    public void PlayDramaticSting()
    {
        source.clip = dramaticSting;
        source.Play();
    }
    public bool IsDramaticSting()
    {
        return source.clip == dramaticSting;
    }
    public void PlayFullUnlimitedAnime()
    {
        source.clip = fullUnlimitedAnime;
        source.Play();
    }


    public void PlayCreditsMusic()
    {
        source.clip = creditsMusic;
        source.Play();
    }

    public void StopMusic()
    { 
        source.Stop();
    }
}

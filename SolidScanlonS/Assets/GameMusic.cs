using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour {
    public AudioClip mainMusic;
    public AudioClip dramaMusic;
    public AudioClip finalBossMusic;

    AudioListener mainListener;

    void Start () {
        DontDestroyOnLoad(this);
        AudioSource.PlayClipAtPoint(mainMusic, Vector3.zero);
    }
	
	void Update () {
	
	}

    public void PlayMainMusic()
    {
        
    }
}

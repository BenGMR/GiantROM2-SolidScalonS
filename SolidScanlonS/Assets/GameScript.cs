using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {
    //todo: Script. Drew Image for talking. Later: Shooting, boss health, boss attacks
    enum Characters
    {
        Jeff,
        JeffCreepy,
        Dan
    }

    string[] script =
    {
        "Hey Drew!",
    };

    private bool justStartedFading = true;
    private bool startedGame = false;

    CanvasGroup fadingTextGroup;
    GameObject fadingTextObject;
    AudioSource source;
    GameObject charPanel;
    Image currentCharacter;


    GameObject gameMusicObject;
    public AudioClip gameStartSound;
    public bool Fade;
    public float FadeSpeed = .04f;
    public Sprite jeffImage;
    public Sprite jeffCreepyImage;
    public Sprite danImage;




    void Start () {
        fadingTextObject = GameObject.Find("FadingText");
        fadingTextGroup = fadingTextObject.GetComponent<CanvasGroup>();

        gameMusicObject = GameObject.Find("GameMusic");
        source = gameMusicObject.GetComponent<AudioSource>();

        charPanel = GameObject.Find("CharacterPanel");
        currentCharacter = charPanel.GetComponent<Image>();

	}

    void CharacterSpeak(Characters character, string dialog)
    {
        switch (character)
        {
            case Characters.Jeff:
                currentCharacter.overrideSprite = jeffImage;
                break;

            case Characters.JeffCreepy:
                currentCharacter.overrideSprite = jeffCreepyImage;
                break;

            case Characters.Dan:
                currentCharacter.overrideSprite = danImage;
                break;
        }
        DialogToSpellOut = dialog;
        textToSpellOutTo.text = "";
        spellingOutDialog = true;

    }
    string DialogToSpellOut;
    bool spellingOutDialog = false;
    public Text textToSpellOutTo;
    float timePassed;
    public float TimePerChar = 1;
    int currentCharIndex = 0;
    void SpellOutDialog()
    {

            //make every letter of dialog appear individually
            timePassed += Time.deltaTime;
            if (timePassed >= TimePerChar)
            {
                timePassed = 0;
                textToSpellOutTo.text += DialogToSpellOut[currentCharIndex];
            currentCharIndex++;
                if (currentCharIndex >= DialogToSpellOut.Length)
                {
                    spellingOutDialog = false;
                currentCharIndex = 0;
                }
            }
        

    }
	
	
	void Update () {

        if(startedGame)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                CharacterSpeak(Characters.JeffCreepy, "TESTTESTTESTTEST");
            }
            if (spellingOutDialog)
            {
                SpellOutDialog();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Fade = true;
            }

            if (Fade)
            {
                FadeTitleText();
            }
        }

	}

    private void FadeTitleText()
    {
        if (justStartedFading)
        {
            fadingTextGroup.alpha = 1;
            justStartedFading = false;
            source.Stop();
            source.volume = 1;
            source.clip = gameStartSound;
            source.PlayOneShot(gameStartSound);
        }
        fadingTextGroup.alpha -= FadeSpeed;
        if (fadingTextGroup.alpha <= 0)
        {
            Fade = false;
            justStartedFading = true;
            startedGame = true;
        }
    }
}

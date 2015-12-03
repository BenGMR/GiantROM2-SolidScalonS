using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
    bool FadeOut = false;
    bool FadeIn = false;
    int currentScriptIndex = -1;
    bool waitingForSpaceBar = false;
    CanvasGroup fadingTitleText;
    GameObject fadingTitleObject;
    AudioSource source;
    GameObject charPanel;
    Image currentCharacter;
    CanvasGroup charPanelCanvasGroup;
    CanvasGroup textBoxCanvasGroup;
    
    List<CanvasGroup> fadingObjects = new List<CanvasGroup>();

    GameObject gameMusicObject;
    public AudioClip gameStartSound;

    public float FadeSpeed = .04f;
    public Sprite jeffImage;
    public Sprite jeffCreepyImage;
    public Sprite danImage;


    void Start () {
        fadingTitleObject = GameObject.Find("FadingText");
        fadingTitleText = fadingTitleObject.GetComponent<CanvasGroup>();

        gameMusicObject = GameObject.Find("GameMusic");
        source = gameMusicObject.GetComponent<AudioSource>();

        charPanel = GameObject.Find("CharacterPanel");
        currentCharacter = charPanel.GetComponent<Image>();

        textBoxCanvasGroup = GameObject.Find("DialogBG").GetComponent<CanvasGroup>();
        charPanelCanvasGroup = charPanel.GetComponent<CanvasGroup>();
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
                waitingForSpaceBar = true;
                }
            }
        

    }
	
	
	void Update () {

        if(startedGame)
        {
            if (currentScriptIndex == -1)
            {
                SetObjectsToFade(charPanelCanvasGroup, textBoxCanvasGroup);
                FadeIn = true;
                
                currentScriptIndex++;
                
            }
            else if(currentScriptIndex == 0)
            {
                if (!spellingOutDialog && !waitingForSpaceBar)
                {
                    CharacterSpeak(Characters.Jeff, script[currentScriptIndex]);
                }
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
                FadeOut = true;
                SetObjectsToFade(fadingTitleText);
            }
        }

        if (waitingForSpaceBar && Input.GetKeyDown(KeyCode.Space))
        {
            waitingForSpaceBar = false;
        }
        if (FadeOut)
        {
            FadeObjectsOut();
        }
        else if (FadeIn)
        {
            FadeObjectsIn();
        }
    }

    private void SetObjectsToFade(params CanvasGroup[] objects)
    {
        fadingObjects.Clear();
        for (int i = 0; i < objects.Length; i++)
        {
            fadingObjects.Add(objects[i]);
        }
    }

    private void FadeObjectsOut()
    {
        if (justStartedFading)
        {
            FadeOut = true;
            for (int i = 0; i < fadingObjects.Count; i++)
            {
                 fadingObjects[i].alpha = 1;
            }
            
            justStartedFading = false;
            if (!startedGame)
            {
                source.Stop();
                source.volume = 1;
                source.clip = gameStartSound;
                source.PlayOneShot(gameStartSound);
            }
        }
        for (int i = 0; i < fadingObjects.Count; i++)
        {
            fadingObjects[i].alpha -= FadeSpeed;
            //if the last object was faded
            if (i == fadingObjects.Count-1 && fadingObjects[i].alpha <= 0)
            {
                FadeOut = false;
                justStartedFading = true;
                if (!startedGame)
                {
                    startedGame = true;
                }
                
            }
        }
        
    }

    private void FadeObjectsIn()
    {
        if (justStartedFading)
        {
            FadeIn = true;
            for (int i = 0; i < fadingObjects.Count; i++)
            {
                fadingObjects[i].alpha = 0;
            }

            justStartedFading = false;

            if (!startedGame)
            {
                source.PlayOneShot(gameStartSound);
            }
        }
        for (int i = 0; i < fadingObjects.Count; i++)
        {
            fadingObjects[i].alpha += FadeSpeed;
            //if the last object was faded
            if (i == fadingObjects.Count - 1 && fadingObjects[i].alpha >= 1)
            {
                FadeIn = false;
                justStartedFading = true;
            }
        }

    }
}

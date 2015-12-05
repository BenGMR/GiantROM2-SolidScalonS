using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameScript : MonoBehaviour
{
    //todo: Script. Drew Image for talking. Later: Shooting, boss health, boss attacks
    enum Characters
    {
        Jeff,
        JeffCreepy,
        Drew,
        Dan
    }

    enum GameMode
    {
        Intro,
        Dialog,
        Battle
    }

    KeyValuePair<Characters, string>[] script =
    {
        new KeyValuePair<Characters, string>(Characters.Jeff, "Hey Drew!"),
        new KeyValuePair<Characters, string>(Characters.Drew, "Hey Jeff!"),
        new KeyValuePair<Characters, string>(Characters.Jeff, "I'm going to murder you!"),
        new KeyValuePair<Characters, string>(Characters.Dan, "Drew! You must kill Jeff!"),
        new KeyValuePair<Characters, string>(Characters.Drew, "WHAT IS EVEN HAPPENING?"),
        new KeyValuePair<Characters, string>(Characters.Dan, "He was never a part of the simulation."),
        new KeyValuePair<Characters, string>(Characters.Dan, "Just do it! Press space to shoot!"),
        new KeyValuePair<Characters, string>(Characters.Drew, "Is that a health bar??"),
        new KeyValuePair<Characters, string>(Characters.Jeff, "DIEEEEEEEEEEE"),//8

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
    CanvasGroup healthBarCanvasGroup;

    List<CanvasGroup> fadingObjects = new List<CanvasGroup>();

    GameObject gameMusicObject;
    public AudioClip gameStartSound;

    public float FadeSpeed = .04f;
    public Sprite jeffImage;
    public Sprite jeffCreepyImage;
    public Sprite drewImage;
    public Sprite danImage;
    public AudioClip textSound;
    public GameObject HealthBar;

    GameMode currentMode = GameMode.Intro;

    void Start()
    {
        fadingTitleObject = GameObject.Find("FadingText");
        fadingTitleText = fadingTitleObject.GetComponent<CanvasGroup>();

        gameMusicObject = GameObject.Find("GameMusic");
        source = gameMusicObject.GetComponent<AudioSource>();

        charPanel = GameObject.Find("CharacterPanel");
        currentCharacter = charPanel.GetComponent<Image>();

        textBoxCanvasGroup = GameObject.Find("DialogBG").GetComponent<CanvasGroup>();
        charPanelCanvasGroup = charPanel.GetComponent<CanvasGroup>();

        healthBarCanvasGroup = HealthBar.GetComponent<CanvasGroup>();
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

            case Characters.Drew:
                currentCharacter.overrideSprite = drewImage;
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
            source.PlayOneShot(textSound);
            currentCharIndex++;
            if (currentCharIndex >= DialogToSpellOut.Length)
            {
                spellingOutDialog = false;
                currentCharIndex = 0;
                waitingForSpaceBar = true;
            }
        }


    }


    void Update()
    {
        if (currentMode == GameMode.Dialog)
        {
            if (currentScriptIndex == -1)
            {
                SetObjectsToFade(charPanelCanvasGroup, textBoxCanvasGroup);
                FadeIn = true;
                currentScriptIndex++;

            }
            else if (currentScriptIndex >= 0)
            {
                if (!spellingOutDialog && !waitingForSpaceBar)
                {
                    CharacterSpeak(script[currentScriptIndex].Key, script[currentScriptIndex].Value);
                }
            }
            if (spellingOutDialog)
            {
                SpellOutDialog();
            }

            if (waitingForSpaceBar && Input.GetKeyDown(KeyCode.Space))
            {
                waitingForSpaceBar = false;
                currentScriptIndex++;
                if(currentScriptIndex == 7)
                {
                    healthBarCanvasGroup.alpha = 1;
                }
                if(currentScriptIndex == 9)
                {
                    textBoxCanvasGroup.alpha = 0;
                    currentMode = GameMode.Battle;
                }
            }
        }
        else if (currentMode == GameMode.Intro)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FadeOut = true;
                SetObjectsToFade(fadingTitleText);
            }
        }
        else if(currentMode == GameMode.Battle)
        {

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
            if (currentMode == GameMode.Intro)
            {
                source.Stop();
                source.PlayOneShot(gameStartSound);
            }
        }
        for (int i = 0; i < fadingObjects.Count; i++)
        {
            fadingObjects[i].alpha -= FadeSpeed;
            //if the last object was faded
            if (i == fadingObjects.Count - 1 && fadingObjects[i].alpha <= 0)
            {
                FadeOut = false;
                justStartedFading = true;
                if (currentMode == GameMode.Intro)
                {
                    currentMode = GameMode.Dialog;
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

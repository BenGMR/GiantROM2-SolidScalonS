using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Jeez this file is ridiculously messy.
/// Things I understand need to be changed but won't because this was a Game Jam game and time was limited
/// Separate the code into different scripts!!!!!!!
/// Read main dialog script from an XML file!!!
/// Organize functions and variables!!
/// Improve the FSM!
/// Figure out which variables i ACTUALLY need and use. (still new to unity)!
/// A lot of my methods were workarounds specifically because of the time constraint!
/// </summary>
public class GameScript : MonoBehaviour
{
    //todo: Script. Drew Image for talking. Later: Shooting, boss health, boss attacks
    enum Characters
    {
        Jeff,
        JeffTracksuit,
        JeffCreepy,
        JeffFestival,
        Drew,
        Dan,
        Colonel,
        Kojima,
        KojimaBigBoss,
        Obama,
        MichellObama
    }

    enum GameMode
    {
        Intro,
        Dialog,
        Battle,
        GameOver,
        Credits
    }

    KeyValuePair<Characters, string>[] script =
    {
        new KeyValuePair<Characters, string>(Characters.Jeff, "Hey Drew!"),
        new KeyValuePair<Characters, string>(Characters.Drew, "Hey Jeff!"),
        new KeyValuePair<Characters, string>(Characters.JeffTracksuit, "I'm going to murder you!"),
        new KeyValuePair<Characters, string>(Characters.Dan, "Drew! You must kill Jeff!"),
        new KeyValuePair<Characters, string>(Characters.Drew, "WHAT IS EVEN HAPPENING?"),
        new KeyValuePair<Characters, string>(Characters.Dan, "He was never a part of the simulation. He must be disposed of."),
        new KeyValuePair<Characters, string>(Characters.Dan, "Just do it! Press space to shoot!"),
        new KeyValuePair<Characters, string>(Characters.Drew, "Is that a health bar??"),
        new KeyValuePair<Characters, string>(Characters.JeffTracksuit, "DIEEEEEEEEEEE. Minions! Assemble!"),//8
        new KeyValuePair<Characters, string>(Characters.JeffTracksuit, "YOU THINK YOU CAN DEFEAT ME?"),
        new KeyValuePair<Characters, string>(Characters.JeffTracksuit, "It's time for my ultimate attack!"),//10 
        new KeyValuePair<Characters, string>(Characters.JeffCreepy, "U N L I M I T E D  A N I M E"),//11 play sound
        new KeyValuePair<Characters, string>(Characters.Dan, "DON'T LET THE ANIME TOUCH YOU!"),
        new KeyValuePair<Characters, string>(Characters.JeffCreepy, "DIEEEEEEE"),//13
        new KeyValuePair<Characters, string>(Characters.JeffFestival, "Well then. This sucks. *dies*"),
        new KeyValuePair<Characters, string>(Characters.Dan, "You did it Drew, you completed the mission."),
        new KeyValuePair<Characters, string>(Characters.Drew, "What mission? Why did I just murder Jeff?"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "You fool! Don't you realize that this has all been a simulation?"),//17
        new KeyValuePair<Characters, string>(Characters.Drew, "..."),
        new KeyValuePair<Characters, string>(Characters.Colonel, "Why do you think this game is titled \"SolidScanlonS\""),
        new KeyValuePair<Characters, string>(Characters.Colonel, "That extra S wasn't a typo"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "It was intentionally placed so that the plot twist would be right under your nose!"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "The extra S stands for Simulation. This is the Solid Scanlon Simulation"),
        new KeyValuePair<Characters, string>(Characters.Drew, "..."),
        new KeyValuePair<Characters, string>(Characters.Colonel, "That's right Drew, or should I say..........Mr. Kojima!"),//24, swap player sprite with kojima
        new KeyValuePair<Characters, string>(Characters.Kojima, "No! It's not true!"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "Oh yes it is! Every time you finish creating a Metal Gear Solid game"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "we slap this VR game onto you and put you in a coma"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "Being in a coma for so long somehow gives you great ideas for Metal Gear Solid"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "Exposition Exposition Exposition Exposition Exposition Exposition Exposition Exposition Exposition"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "And now it's time to wake you up..."),
        new KeyValuePair<Characters, string>(Characters.Colonel, "IT'S TIME TO MAKE ANOTHER METAL GEAR SOLID"),
        new KeyValuePair<Characters, string>(Characters.Kojima, "NEVER! Just let me go already!"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "WELL THEN IT'S TIME TO DIE!"),//33 play bullet hell music
        new KeyValuePair<Characters, string>(Characters.Colonel, "YOU MUST FACE THE DEMONS OF YOUR PAST"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "MR. PRESIDENT"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "MGS2 WAS BETTER ANYWAY"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "EXPOSITION EXPOSITION EXPOSITION EXPOSITION EXPOSITION EXPOSITION EXPOSITION EXPOSITION "),
        new KeyValuePair<Characters, string>(Characters.Colonel, "It seems you have bested...me..........*dies*"),//38 credits music
        new KeyValuePair<Characters, string>(Characters.Kojima, "I've done it..."),
        new KeyValuePair<Characters, string>(Characters.KojimaBigBoss, "I'm finally free!!"),
        new KeyValuePair<Characters, string>(Characters.KojimaBigBoss, "Time to live life happily ever after!"),//sunglasses?//41
        new KeyValuePair<Characters, string>(Characters.Colonel, "So you've finally beaten the game..."),
        new KeyValuePair<Characters, string>(Characters.KojimaBigBoss, "I thought I killed you!"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "Enough with the games!"),
        new KeyValuePair<Characters, string>(Characters.KojimaBigBoss, "What??"),
        new KeyValuePair<Characters, string>(Characters.Colonel, "No, seriously, enough with the games!"),
        new KeyValuePair<Characters, string>(Characters.KojimaBigBoss, "I don't understand"),
        new KeyValuePair<Characters, string>(Characters.MichellObama, "Take off the VR headset Mr. President!"),
        new KeyValuePair<Characters, string>(Characters.Kojima, "Wait...so I'm not....."),
        new KeyValuePair<Characters, string>(Characters.Obama, "NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO"),//50
        new KeyValuePair<Characters, string>(Characters.Obama, "OOOOOOOOOOOOOOOOO"),//51
    };


    float creditsSpeed = .01f;
    private bool justStartedFading = true;
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
    CanvasGroup bossHealthCG;
    CanvasGroup drewHealthCG;

    Vector3 mousePosition;

    public float bulletDamage = .08f;

    GameMusic musicScript;

    public Vector2 bossBulletSpeed = new Vector2(.1f, .1f);

    GameObject Drew;

    List<CanvasGroup> fadingObjects = new List<CanvasGroup>();

    GameObject gameMusicObject;

    public AudioClip gameStartSound;

    public float FadeSpeed = .04f;
    public Sprite jeffImage;
    public Sprite jeffCreepyImage;
    public Sprite drewImage;
    public Sprite danImage;
    public Sprite jeffTracksuit;
    public Sprite jeffFestival;
    public Sprite colonel;
    public Sprite Kojima;
    public Sprite Obama;
    public Sprite MichelleObama;
    public Sprite KojimaBigBoss;
    public GameObject CreditsText;
    public GameObject fadeImage;

    Image fadeImageImage;

    public Sprite MGS1;
    public Sprite MGS2;
    public Sprite MGS3;
    public Sprite MGS4;
    public Sprite MGSV;

    public Sprite[] kojimaTweets;

    public AudioClip textSound;
    public GameObject BossHealthBar;
    public GameObject bossGreenHP;


    public Sprite BulletImage;

    public GameObject jeffBullet;
    public GameObject drewBullet;

    GameObject thanksForPlaying;
    CanvasGroup thanksCG;
    public GameObject Anime;

    List<GameObject> drewBullets = new List<GameObject>();

    GameMode currentMode = GameMode.Intro;

    //shoot logic
    float timePerShot = .7f;
    float timeSinceLastShot = 1;



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

        bossHealthCG = BossHealthBar.GetComponent<CanvasGroup>();

        Drew = GameObject.Find("DrewCoolActionGuy");

        bradBullet = GameObject.Instantiate(jeffBullet);
        bradBullet.SetActive(false);

        musicScript = gameMusicObject.GetComponent<GameMusic>();
        fadeImageImage = fadeImage.GetComponent<Image>();

        thanksForPlaying = GameObject.Find("ThanksForPlaying");
        thanksCG = thanksForPlaying.GetComponent<CanvasGroup>();

    }

    void CharacterSpeak(Characters character, string dialog)
    {
        switch (character)
        {
            case Characters.Jeff:
                currentCharacter.overrideSprite = jeffImage;
                break;

            case Characters.JeffTracksuit:
                currentCharacter.overrideSprite = jeffTracksuit;
                break;

            case Characters.JeffCreepy:
                currentCharacter.overrideSprite = jeffCreepyImage;
                break;

            case Characters.JeffFestival:
                currentCharacter.overrideSprite = jeffFestival;
                break;

            case Characters.Colonel:
                currentCharacter.overrideSprite = colonel;
                break;

            case Characters.Kojima:
                currentCharacter.overrideSprite = Kojima;
                break;
            case Characters.KojimaBigBoss:
                currentCharacter.overrideSprite = KojimaBigBoss;
                break;

            case Characters.Drew:
                currentCharacter.overrideSprite = drewImage;
                break;

            case Characters.Dan:
                currentCharacter.overrideSprite = danImage;
                break;

            case Characters.MichellObama:
                currentCharacter.overrideSprite = MichelleObama;
                break;

            case Characters.Obama:
                currentCharacter.overrideSprite = Obama;
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
    bool secretEnding = false;

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
    GameObject bradBullet;

    void JeffShoot()
    {

        bradBullet.transform.position = charPanel.transform.position;
        //bullet.transform.Translate(new Vector3(0, charPanel.transform.localScale.y, 0));
       
    }

    void DrewShoot()
    {
        GameObject bullet = GameObject.Instantiate(drewBullet);
        bullet.transform.position = Drew.transform.position;
        bullet.transform.Translate(0, 1, 0);

        //bullet.GetComponent<SpriteRenderer>().sprite = BulletImages[Random.Range(0, BulletImages.Length)];
        if(currentScriptIndex >= 24)
        {
            bullet.GetComponent<SpriteRenderer>().sprite = kojimaTweets[Random.Range(0, kojimaTweets.Length)];
        }
  
        drewBullets.Add(bullet);
    }
    void BossLoseHealth()
    {
        if (bossGreenHP.transform.localScale.x >= 0)
        {
            bossGreenHP.transform.localScale = new Vector3(bossGreenHP.transform.localScale.x - bulletDamage, bossGreenHP.transform.localScale.y, bossGreenHP.transform.localScale.z);
        }
    }
    void updateAllBullets()
    {
        for (int i = 0; i < drewBullets.Count; i++)
        {
            if ((drewBullets[i].transform.position.x) + 15 >= charPanel.transform.position.x && drewBullets[i].transform.position.x - 15 <= charPanel.transform.position.x && drewBullets[i].transform.position.y >= charPanel.transform.position.y)
            {
                BossLoseHealth();
                GameObject bullet = drewBullets[i];
                drewBullets.Remove(bullet);
                i--;
                DestroyObject(bullet);
            }
        }

        bradBullet.transform.position = Vector3.MoveTowards(bradBullet.transform.position, Drew.transform.position, bossBulletSpeed.magnitude);
    }

    //this code is starting to look disgusting :( i should split these scripts up
    void Update()
    {
        updateAllBullets();

        if(bossGreenHP.transform.localScale.x < 0)
        {
            bossGreenHP.transform.localScale = new Vector3(0, bossGreenHP.transform.localScale.y, bossGreenHP.transform.localScale.z);
        }

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
            if (currentScriptIndex == 11)
            {
                bossGreenHP.transform.localScale = new Vector3(1, bossGreenHP.transform.localScale.y, bossGreenHP.transform.localScale.z);
            }
            if (spellingOutDialog)
            {
                SpellOutDialog();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    textToSpellOutTo.text = DialogToSpellOut;
                    spellingOutDialog = false;
                    currentCharIndex = 0;
                    waitingForSpaceBar = true;
                }
            }

            else if (waitingForSpaceBar && Input.GetKeyDown(KeyCode.Space))
            {
                waitingForSpaceBar = false;
                currentScriptIndex++;
                if (currentScriptIndex == 7)
                {
                    bossHealthCG.alpha = 1;
                }
                else if (currentScriptIndex == 9)
                {

                    textBoxCanvasGroup.alpha = 0;
                    currentMode = GameMode.Battle;
                    musicScript.PlayBossMusic();
                    bradBullet.SetActive(true);
                    JeffShoot();
                }
                else if (currentScriptIndex == 11)
                {
                    musicScript.PlayFullUnlimitedAnime();
                    TimePerChar = .1f;
                }
                else if (currentScriptIndex == 12)
                {
                    TimePerChar = 0;

                }
                else if (currentScriptIndex == 14)
                {
                    currentMode = GameMode.Battle;
                }
                else if (currentScriptIndex == 15)
                {
                    musicScript.PlayDramaMusic();
                }
                else if (currentScriptIndex == 24)
                {
                    Drew.GetComponent<SpriteRenderer>().sprite = Kojima;
                }
                else if (currentScriptIndex == 33)
                {
                    musicScript.PlayFinalBossMusic();
                }
                else if (currentScriptIndex == 34)
                {
                    currentMode = GameMode.Battle;
                    bradBullet.SetActive(true);
                    bradBullet.GetComponent<SpriteRenderer>().sprite = MGS1;
                    bossHealthCG.alpha = 1;
                    bossGreenHP.transform.localScale = Vector3.one;
                }
                else if (currentScriptIndex == 35)
                {
                    currentMode = GameMode.Battle;
                    bradBullet.GetComponent<SpriteRenderer>().sprite = MGS2;
                    bossGreenHP.transform.localScale = Vector3.one;
                }
                else if (currentScriptIndex == 36)
                {
                    currentMode = GameMode.Battle;
                    bradBullet.GetComponent<SpriteRenderer>().sprite = MGS3;
                    bossGreenHP.transform.localScale = Vector3.one;
                }
                else if (currentScriptIndex == 37)
                {
                    currentMode = GameMode.Battle;
                    bradBullet.GetComponent<SpriteRenderer>().sprite = MGS4;
                    bossGreenHP.transform.localScale = Vector3.one;
                }
                else if (currentScriptIndex == 38)
                {
                    currentMode = GameMode.Battle;
                    bradBullet.GetComponent<SpriteRenderer>().sprite = MGSV;
                    bossGreenHP.transform.localScale = Vector3.one;
                }
                else if (currentScriptIndex == 42)
                {
                    currentMode = GameMode.Credits;
                    FadeOut = true;
                    SetObjectsToFade(bossHealthCG, charPanelCanvasGroup);
                    TimePerChar = 0;
                }
                else if(currentScriptIndex == 50)
                {
                    musicScript.PlayDramaticSting();
                }
                else if (currentScriptIndex == 51)
                {
                    FadeSpeed = .02f;
                    
                    secretEnding = false;
                    FadeOut = true;
                    SetObjectsToFade(bossHealthCG, charPanelCanvasGroup);

                    currentMode = GameMode.GameOver;
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
        else if (currentMode == GameMode.Battle)
        {
            if (currentScriptIndex == 9)
            {
                if (bossGreenHP.transform.localScale.x <= 0)
                {
                    currentMode = GameMode.Dialog;
                    musicScript.StopMusic();
                    textBoxCanvasGroup.alpha = 1;
                    bradBullet.SetActive(false);
                }
            }
            //unlimited anime
            if (currentScriptIndex == 14)
            {
                Anime.transform.Translate(-.01f, 0, 0);
            }

            if (bossGreenHP.transform.localScale.x <= 0)
            {
                currentMode = GameMode.Dialog;
                if (currentScriptIndex == 9)
                {
                    musicScript.StopMusic();
                    textBoxCanvasGroup.alpha = 1;
                    bradBullet.SetActive(false);
                }
                if (currentScriptIndex == 14)
                {
                    Anime.SetActive(false);
                    musicScript.StopMusic();
                }
                if (currentScriptIndex == 38)
                {
                    musicScript.PlayCreditsMusic();
                    bradBullet.SetActive(false);
                    TimePerChar = .1f;
                }


            }

            timeSinceLastShot += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space) && timeSinceLastShot >= timePerShot)
            {
                DrewShoot();
                timeSinceLastShot = 0;
            }
        }
        else if (currentMode == GameMode.Credits)
        {
            if (!secretEnding && !source.isPlaying)
            {
                fadeImageImage.CrossFadeAlpha(1, 3, true);
            }
            
            if (secretEnding && fadeImageImage.color.a > 0)
            {
                fadeImageImage.CrossFadeAlpha(0, 3, true);
            }
            else if (fadeImageImage.color.a >= 1)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    creditsSpeed = .04f;
                }
                else
                {
                    creditsSpeed = .01f;
                }
                CreditsText.transform.Translate(0,creditsSpeed, 0);
                if (CreditsText.transform.position.y >= 23)
                {
                    source.Stop();
                    secretEnding = true;
                    FadeIn = true;
                    currentMode = GameMode.Dialog;
                    SetObjectsToFade(charPanelCanvasGroup, bossHealthCG);
                    
                }
            }
        }
        else if(currentMode == GameMode.GameOver)
        {
            thanksCG.alpha += .01f;

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

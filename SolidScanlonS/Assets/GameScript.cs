using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {
    public bool Fade;
    public float FadeSpeed = .04f;

    CanvasGroup fadingTextGroup;
    GameObject fadingTextObject;
	void Start () {
        fadingTextObject = GameObject.Find("FadingText");
        fadingTextGroup = fadingTextObject.GetComponent<CanvasGroup>();
	}
	
	
	void Update () {
	    if(Fade)
        {
            fadingTextGroup.alpha -= FadeSpeed;
            if(fadingTextGroup.alpha <= 0)
            {
                Fade = false;
            }
        }
	}
}

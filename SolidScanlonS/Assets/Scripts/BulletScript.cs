using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {


    GameObject charPanel;

    public float bulletSpeed = .1f;
   
    // Use this for initialization
    void Start () {


        charPanel = GameObject.Find("CharacterPanel");
	
	}

    void Update () {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(charPanel.transform.position.x + 3, charPanel.transform.position.y, charPanel.transform.position.z), bulletSpeed);
    }
}

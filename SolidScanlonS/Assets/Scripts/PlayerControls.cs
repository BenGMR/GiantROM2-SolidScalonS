using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

    public float MoveSpeed = .02f;



    void Start () {
    }


    void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, MoveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-MoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -MoveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(MoveSpeed, 0, 0);
        }
    }
}

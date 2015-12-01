using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

    public float MoveSpeed = .02f;

    //GameObject leftCollider;
    //CircleCollider2D leftCircle;
    //GameObject rightCollider;
    //GameObject topCollider;
    //GameObject bottomCollider;

    void Start () {
        //leftCollider = GameObject.Find("LeftHitBox");
        //rightCollider = GameObject.Find("RightHitBox");
        //topCollider = GameObject.Find("TopHitBox");
        //bottomCollider = GameObject.Find("BottomHitBox");
    }

	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, MoveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-MoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -MoveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(MoveSpeed, 0, 0);
        }
    }
}

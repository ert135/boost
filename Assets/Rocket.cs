using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    void Update () {
        processInput();
	}

    private void processInput()
    {
        if (Input.GetKey((KeyCode.A)))
        {

        }
        else if (Input.GetKey((KeyCode.D)))
        {

        }

        if (Input.GetKey((KeyCode.Space)))
        {
            print("Space pressed");
            rigidBody.AddRelativeForce(new Vector3(0, 50f, 0));
        }
    }
}

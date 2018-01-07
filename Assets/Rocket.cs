using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource engineSound;
    private bool isPlayingEngineSound;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
        this.isPlayingEngineSound = false;
	}
	
	// Update is called once per frame
    void Update () {
        processInput();
	}

    private void processInput()
    {
        if (Input.GetKey((KeyCode.A)))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey((KeyCode.D)))
        {
            transform.Rotate(-Vector3.forward);
        }

        if (Input.GetKey((KeyCode.Space)))
        {
            rigidBody.AddRelativeForce(new Vector3(0, 40f, 0));
            if (this.isPlayingEngineSound == false)
            {
                this.isPlayingEngineSound = true;
                engineSound.Play();
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            this.isPlayingEngineSound = false;
            engineSound.Stop();
        }
    }
}

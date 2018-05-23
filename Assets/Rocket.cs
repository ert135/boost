using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public float rcsThrust = 50f;
    public float mainThrust = 10f;

    Rigidbody rigidBody;
    AudioSource engineSound;
    private bool isPlayingEngineSound;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
        this.isPlayingEngineSound = false;
        engineSound.Stop();
    }
	
	// Update is called once per frame
    void Update () {
        checkThrust();
        checkRotate();
	}

    private void checkThrust()
    {
        if (Input.GetKey((KeyCode.Space)))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (this.isPlayingEngineSound == false)
            {
                this.isPlayingEngineSound = true;
                engineSound.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.isPlayingEngineSound = false;
            engineSound.Stop();
        }

    }

    private void checkRotate()
    {
        rigidBody.freezeRotation = true;
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey((KeyCode.A)))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey((KeyCode.D)))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }
}

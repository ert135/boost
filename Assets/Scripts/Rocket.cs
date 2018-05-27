using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    public float rcsThrust = 50f;
    public float mainThrust = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip levelLoadSound;
    [SerializeField] AudioClip deathSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    [SerializeField] float levelLoadDelay = 2f;

    GameObject flameLight;
    Light lightRef;

    Rigidbody rigidBody;
    AudioSource engineSound;

    public enum State { Alive, Dying, Transcending }
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        engineSound = GetComponentInChildren<AudioSource>();
        engineSound.Stop();
        flameLight = GameObject.FindGameObjectWithTag("flameLight");
        lightRef = flameLight.GetComponent<Light>();
        lightRef.enabled = false;
    }
	
	// Update is called once per frame
    void Update () {
        if (this.state == State.Alive)
        {
            checkThrust();
            checkRotate();
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }
        switch(collision.gameObject.tag)
        {
            case "friendly":
                //do nothing
                break;
            case "fuel":
                //do nothing
                break;
            case "Finish":
                if (this.isOnLastLevel() == true)
                {
                   //do nothing for now 
                } else
                {
                    this.state = State.Transcending;
                    engineSound.PlayOneShot(levelLoadSound);
                    successParticles.Play();
                    Invoke("loadNextScene", levelLoadDelay);
                }
                //do nothing
                break;
            default:
                this.state = State.Dying;
                engineSound.PlayOneShot(deathSound);
                deathParticles.Play();
                Invoke("loadFirstScene", levelLoadDelay);
                break;
        }
    }

    private void loadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void checkThrust()
    {
        if (Input.GetKey((KeyCode.Space)))
        {
            thrust();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            engineSound.Stop();
            lightRef.enabled = false;
            mainEngineParticles.Stop();
        }
    }

    private void thrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!engineSound.isPlaying)
        {
            engineSound.PlayOneShot(mainEngine);
        }
        lightRef.enabled = true;
        mainEngineParticles.Play();
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

    private bool isOnLastLevel()
    {
        if(SceneManager.sceneCount == SceneManager.GetActiveScene().buildIndex - 1)
        {
            return true;
        } else
        {
            return false;
        }
    }
}

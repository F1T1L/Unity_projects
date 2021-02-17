using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource AudioSource;

    [SerializeField] float rcsTrust = 100f;
    [SerializeField] float rcsBody = 100f;
    [SerializeField] float levelLoadDelay=2f;
    [SerializeField] AudioClip audioEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip finishSound;

    [SerializeField] ParticleSystem finishSFX;
    [SerializeField] ParticleSystem deathSFX;
    [SerializeField] ParticleSystem engineSFX;
   
    enum State { Alive, Dying, Port }
    State state = State.Alive;
    bool collisionDisabled = false;
    
     
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(SceneManager.sceneCountInBuildSettings);      
        rigidBody = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
            
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Trusted();
            Rotate();
        }
        if (Debug.isDebugBuild)
        {
            DebugKeysBinds();
        }
       

    }

    void DebugKeysBinds()
    {
        if (Input.GetKeyDown(KeyCode.L))   
        {                        
            LoadNextLevel();
            Debug.LogWarning("Cheated L : LoadNextLevel()");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
            Debug.LogWarning("collisionDisabled: " +collisionDisabled);                
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionDisabled) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            case "Finish":
                Debug.Log("Finish");                
                state = State.Port;
                finishSFX.Play();
                AudioSource.Stop();
                AudioSource.PlayOneShot(finishSound);
                Invoke("LoadNextLevel", levelLoadDelay);
                break;
            default:
                Debug.Log("Dead");
                state=State.Dying;
                deathSFX.Play();
                AudioSource.Stop();
                AudioSource.PlayOneShot(deathSound);
                //rigidBody.freezeRotation = true;
                Invoke("LoadFirstLevel", levelLoadDelay);              
                break;
        }
    }

    
    void LoadNextLevel()    {
        //rigidBody.freezeRotation = false;
        int currentSceneNumber=  SceneManager.GetActiveScene().buildIndex;
        currentSceneNumber++;
        Debug.Log("currentSceneNumber : " + currentSceneNumber);
        int scenes = SceneManager.sceneCountInBuildSettings;
        //Debug.Log("scenes: " + scenes);        
        if (currentSceneNumber == scenes)  
        {
            currentSceneNumber=0;
        }
        SceneManager.LoadScene(currentSceneNumber);
    }
    void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
    void Rotate()
    {
        rigidBody.freezeRotation = true;
        float rotationPerFrame = rcsTrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationPerFrame);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-(Vector3.forward * rotationPerFrame));
        }
        rigidBody.freezeRotation = false;
    }

    void Trusted()
    {
        rigidBody.ResetCenterOfMass();

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * rcsBody ); // * Time.deltaTime?
            if (!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(audioEngine);
            }
            engineSFX.Play();
        }
        else
        {
            AudioSource.Stop();
            engineSFX.Stop();
        }
    }
}

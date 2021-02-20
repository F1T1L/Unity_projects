using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    //transform.position = startPos + Vector3.up * height * Mathf.Sin(Time.time * speed);
    // Start is called before the first frame update
    void Awake()
    {
        int nMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;        
        print(nMusicPlayers);
        if (nMusicPlayers>1)
        {
            Destroy(gameObject);
        }
        else { 
        DontDestroyOnLoad(gameObject.gameObject);
        }
       
    }
    void Start()
    {
        Invoke("LoadFirstScene", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}

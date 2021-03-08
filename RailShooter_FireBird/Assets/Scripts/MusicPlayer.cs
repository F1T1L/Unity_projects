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
       // print(nMusicPlayers);
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
        if (SceneManager.GetActiveScene().buildIndex== 0) { 
        Invoke("LoadFirstScene", 2f);
        }
    }    
    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}

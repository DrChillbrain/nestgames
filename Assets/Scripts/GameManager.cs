using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager ptr;
    private int time = 150;

    // Start is called before the first frame update
    void Start()
    {
        //Sets object to load between scenes
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        ptr = this;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu") {
            //Set timer values
            GameObject.Find("Timer").SendMessage("setTime", time);
        } else {

        }
    }

    public void endGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    public string nextLevel;

    public GameOverScreen gOScreen;
    public ClearScreen clearScreen;

    // Use this for initialization
    void Start ()
    {
        
        gOScreen = FindObjectOfType<GameOverScreen>();
        clearScreen = FindObjectOfType<ClearScreen>();

        clearScreen.levelToLoad = nextLevel;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    

    public void Caught()
    {
        if (!clearScreen.cleared)
        {
            gOScreen.GameOvered();
            //RestartLevel();
        }
    }

    public void StageCleared()
    {
        clearScreen.StageCleared();
    }
}

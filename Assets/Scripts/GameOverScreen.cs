using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public bool gameOvered;
    public GameObject GOCanvas;

	// Use this for initialization
	void Start ()
    {
        gameOvered = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameOvered)
        {
            GOCanvas.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            GOCanvas.SetActive(false);

            Time.timeScale = 1f;
        }
	}

    public void RetryLevel()
    {
        gameOvered = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOvered()
    {
        gameOvered = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearScreen : MonoBehaviour
{
    public bool cleared;
    public GameObject clearCanvas;
    public string levelToLoad;

    // Use this for initialization
    void Start()
    {
        cleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cleared)
        {
            clearCanvas.SetActive(true);

            Time.timeScale = 0f;
            //Debug.Log("clear!");
        }
        else
        {
            clearCanvas.SetActive(false);

            Time.timeScale = 1f;
        }
    }

    public void NextLevel()
    {
        cleared = false;
        SceneManager.LoadScene(levelToLoad);
    }

    public void StageCleared()
    {
        cleared = true;
    }
}

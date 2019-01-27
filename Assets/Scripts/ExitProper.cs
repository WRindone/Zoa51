using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitProper : MonoBehaviour {

    private bool playerInZone;

    public LevelManager lvlManager;

    // Use this for initialization
    void Start()
    {
        playerInZone = false;

        lvlManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInZone)
        {
            lvlManager.StageCleared();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "player")
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "player")
        {
            playerInZone = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharBehavior : MonoBehaviour
{
    public int x;
    public int y;
    public int energy;
    public int cooldown;
    public bool canMove = true;
    public Dictionary<string, bool> keycards =
        new Dictionary<string, bool>();

 

    // Start is called before the first frame update
    void Start()
    {
        keycards.Add("Green", false);
        keycards.Add("Blue" , false);
        keycards.Add("Red", false);
        energy = 0;
        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

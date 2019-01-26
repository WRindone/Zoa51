using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickableSpace : MonoBehaviour
{
    [SerializeField]
    private MapGenerator genie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseUp()
    {
        genie.tryToMove((int)Mathf.Floor(transform.position.x),
                        (int)Mathf.Floor(transform.position.y));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

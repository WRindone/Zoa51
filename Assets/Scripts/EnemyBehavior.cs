using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int x;
    public int y;
    public int lastmove;

    // Start is called before the first frame update
    void Start()
    {
        x = 5;
        y = 5;
        lastmove = 0;
        transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

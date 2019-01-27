using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int x;
    public int y;
    public int lastmove;

    [SerializeField]
    private MapGenerator g;

    // Start is called before the first frame update
    void Start()
    {
        lastmove = 4;
        transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
    }


    public void MoveIt()
    {
        if (x != g.maxMapX - 1 && g.mappy[x + 1, y] == 0 && lastmove != 2)
        {
            x++;
            lastmove = 0;
            Debug.Log(x + " " + y);
        }
        else if (y != g.maxMapY - 1 && g.mappy[x, y + 1] == 0 && lastmove != 3)
        {
            y++;
            lastmove = 1;
            Debug.Log(x + " " + y);
        }
        else if (x != 0 && g.mappy[x - 1, y] == 0 && lastmove != 0)
        {
            x--;
            lastmove = 2;
            Debug.Log("Moved Left");
        }
        else if (y != 0 && g.mappy[x, y - 1] == 0 && lastmove != 1)
        {
            y--;
            lastmove = 3;
            Debug.Log("Moved Down");
        }
        else
        {
            lastmove = 4;
            Debug.Log("Moved Brep");
        }
        transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

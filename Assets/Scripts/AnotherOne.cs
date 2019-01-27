using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherOne : MonoBehaviour
{
    public int x;
    public int y;

    bool chase = false;
    Vector2[] direct = { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
    public int direction;
    Vector2 lastSeenPlayer;
    bool chase_player = false;
    Queue<Vector2> playerPositions = new Queue<Vector2>();
    int offset = 0;
    bool blegh = false;
    public KeyCardsScr[] keys;

    [SerializeField]
    private MapGenerator g;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
    }

    public void MoveIt()
    {
        if (direction == 0 || direction == 1)
        {
            for (int i = y; i >= 0 && i < g.maxMapY; i = i + (int)direct[direction].y)
            {
                if(g.mappy[x,i] == 1)
                {
                    break;
                }
                if (g.lien.x == x && g.lien.y == i)
                {
                    lastSeenPlayer = new Vector2(g.lien.x, g.lien.y);
                    chase_player = true;
                }
            }
        }
        else if (direction == 2 || direction == 3)
        {
            for (int i = x; i >= 0 && i < g.maxMapX; i = i + (int)direct[direction].x)
            {   
                if(g.mappy[i,y] == 1)
                {
                    break;
                }
                if (g.lien.x == i && g.lien.y == y)
                {
                    lastSeenPlayer = new Vector2(g.lien.x, g.lien.y);
                    chase_player = true;
                }
            }
        }
        if (chase_player == true)
        {
            Chase();
            playerPositions.Enqueue(new Vector2(g.lien.x, g.lien.y));
        }
    }

    public void Chase()
    {
        if(!(x == lastSeenPlayer.x && y == lastSeenPlayer.y) && !blegh)
        {
            Debug.Log(x + " " + y + " " + lastSeenPlayer.x + " " + lastSeenPlayer.y);
            x += (int)direct[direction].x;
            y += (int)direct[direction].y;
            offset += 1;
            Debug.Log(offset);
            Debug.Log(x + " " + y);
        } else
        {
            if (!blegh && playerPositions.Count != 0)
            {
                playerPositions.Dequeue();
            } else if(playerPositions.Count == 0)
            {
                playerPositions.Enqueue(new Vector2(g.lien.x, g.lien.y));
            }
            Debug.Log("SWITCHED TO FOLLOW MODE");
            x = (int)playerPositions.Peek().x;
            y = (int)playerPositions.Peek().y;
            playerPositions.Dequeue();
            blegh = true;
        }
        transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

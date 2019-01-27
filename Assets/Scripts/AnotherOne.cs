using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherOne : MonoBehaviour
{
    //Position of the Enemy
    public int x;
    public int y;

    //The various directions the robot can face,
    //0 = north, 1 = south, 2 = East, 3 = West
    Vector2[] direct = { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };

    //The direction
    public int direction;

    //Position the robot first sees the player
    Vector2 lastSeenPlayer;

    //Whether or not the robot is chasing the player
    bool chase_player = false;

    //A Queue holding the positions of the player
    Queue<Vector2> playerPositions = new Queue<Vector2>();

    //How far away (and thus how many moves behind 
    int offset = 0;

    //Fixes a weird bug where the robot would do two moves on the first turn
    bool workaround = false;

    //Makes reference back to the map controller
    [SerializeField]
    private MapGenerator g;

    // Start is called before the first frame update
    void Start()
    {
        //Draws the Robot @ x,y
        transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
    }

    //Move Function (Handles Movement)
    public void MoveIt()
    {
        //If facing North Or South && Haven't Spotted Player
        if (direction == 0 || direction == 1 && !chase_player)
        {
            //Scans from position to direction until end of map...
            for (int i = y; i >= 0 && i < g.maxMapY; i = i + (int)direct[direction].y)
            {
                //...Or until it hits a wall
                if(g.mappy[x,i] == 1)
                {
                    break;
                }
                //If it finds the alien's position, saves it and begins chasing
                if (g.lien.x == x && g.lien.y == i)
                {
                    lastSeenPlayer = new Vector2(g.lien.x, g.lien.y);
                    chase_player = true;
                    break;
                }
            }
        }
        // Else, if he's facing left or right && Hasn't spotted...
        else if (direction == 2 || direction == 3 && !chase_player)
        {
            //...Scan to end of map...
            for (int i = x; i >= 0 && i < g.maxMapX; i = i + (int)direct[direction].x)
            {   
                //j...Or until it hits a wall
                if(g.mappy[i,y] == 1)
                {
                    break;
                }
                //If it finds the alien's position, saves it and begins chasing
                if (g.lien.x == i && g.lien.y == y)
                {
                    lastSeenPlayer = new Vector2(g.lien.x, g.lien.y);
                    chase_player = true;
                    break;
                }
            }
        }
        //If chasing the player, begins chase sequence and saving his moves
        if (chase_player == true)
        {
            Chase();
            playerPositions.Enqueue(new Vector2(g.lien.x, g.lien.y));
        }
    }

    //Chase sequence
    public void Chase()
    {
        //If he's not reached the last point, and the workaround hasn't been called
        //Move robot one step at a time && increase offset
        if(!(x == lastSeenPlayer.x && y == lastSeenPlayer.y) && !workaround)
        {
            x += (int)direct[direction].x;
            y += (int)direct[direction].y;
            offset += 1;
        //Otherwise, if reached last position
        } else
        {
            //Fixes the move twice error
            if (!workaround && playerPositions.Count != 0)
            {
                playerPositions.Dequeue();
            //Fixes crash if Player lands on robot
            } else if(playerPositions.Count == 0)
            {
                playerPositions.Enqueue(new Vector2(g.lien.x, g.lien.y));
            }
            //Sets robot's position to the last spot it saw the player
            x = (int)playerPositions.Peek().x;
            y = (int)playerPositions.Peek().y;
            //Removes player's position from Queue
            playerPositions.Dequeue();
            //Removes the workaround, will now act as normal
            workaround = true;
        }
        //Animates the robot's position
        transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

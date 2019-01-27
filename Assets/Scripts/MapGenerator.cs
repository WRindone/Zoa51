using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
    //Holds the sprites for the sprite map
    public GameObject[] spritemap;
    //The map as a double int array
    public int[,] mappy;
    //Map size
    public int maxMapX;
    public int maxMapY;
    //Script controlling lien
    public CharBehavior lien;
    //Script controlling burt
    public EnemyBehavior burt;
    //Script controlling roburt
    public AnotherOne roburt;
    //Level# (Calls the lvl from the lvlx.txt)
    public int lvl;

    //Starting location of lien
    public int startx;
    public int starty;

    //Determines whether it's lien's turn or the enemies
    public bool turn = true;

    //Array of KeyCards
    public KeyCardsScr[] keys;
    //Array of Gates
    public KeyGateScript[] gates;

    //Called on StartUp
    void Start()
    {
            //Generates MapData
            GenerateMapData();
            GenerateMapVisual();
    }

    //Generate Map Data
    void GenerateMapData()
    {
        //Sets lien's position, and draws him
        lien.x = startx;
        lien.y = starty;
        lien.transform.SetPositionAndRotation(new Vector3(startx, starty, 0), Quaternion.identity);
        //Accepts level data
        string pathForReader = "Assets/LevelData/lvl" + lvl + ".txt";
        StreamReader reader = new StreamReader(pathForReader);
        //Sets up the map as a double array based on map size
        mappy = new int[maxMapX, maxMapY];
        //Reads from the txt file and converts to a level
        for(int y = maxMapY-1; y >=0; y--)
        {
            string line;
            line = reader.ReadLine();
            for(int x = 0; x < maxMapX; x++)
            {
                string thing = line.Substring(3 * x, 2);
                int cheap = (int) float.Parse(thing);
                mappy[x, y] = cheap;
                for (int i = 0; i < gates.Length; i++)
                {
                    //Changes the tiles of gates (regardless of what was underneath them)
                    //To a wall tile
                    if ((int)gates[i].position.x == x && (int)gates[i].position.y == y)
                    {
                        mappy[x, y] = 1;
                    }
                }
            }
        }
    }

    //Takes the data from mappy and generates the terrain based on the sprite
    //(The majority of the levels (and the rest of this code)
    //work under this assumption:
    //0 = Floor
    //1 = Wall
    //2 = Exit
    void GenerateMapVisual()
    {
        for(int x = 0; x < maxMapX; x++)
        {
            for(int y = 0; y < maxMapY; y++)
            {
                GameObject toCreate = spritemap[mappy[x, y]];
                Instantiate(toCreate, new Vector3(x,y,0), Quaternion.identity);
            }
        }
    }

    //Function that controls lien's movement
    public void tryToMove(int x, int y)
    {
        //If it's lien's turn
        if (lien.canMove)
        {
            //if his cooldown is active, does not allow him to move,
            //But allows enemies to move
            if (lien.cooldown > 0)
            {
                lien.cooldown--;
                StartCoroutine(Bleh());
            }
            //Otherwise
            else
            {
                //If it is a walkable tile...
                if (mappy[x, y] == 0 || mappy[x,y] == 2)
                {
                    //Calculate dist from player
                    int dist = DistanceFromPlayer(x, y);
                    //if the player's position is clicked, counts as a wait
                    //Increases energy if energy < 5
                    if (dist == 0 && lien.energy < 5)
                    {
                        lien.energy++;
                        //Switch turns, burts' move
                        lien.canMove = false;
                        StartCoroutine(Bleh());
                    }
                    //Otherwise, if the spot is one away from lien
                    else if (dist == 1)
                    {
                        //Draw and set lien to new position
                        lien.transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
                        lien.x = x;
                        lien.y = y;
                        //Switch turns, burts' move
                        lien.canMove = false;
                        StartCoroutine(Bleh());
                    }
                    //Dash mechanic
                    else if (dist > 1 && dist - 1 <= lien.energy)
                    {
                        if(testPath(lien.x,lien.y,x,y))
                        {
                            lien.transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
                            lien.cooldown = DistanceFromPlayer(x, y) - 1;
                            lien.energy -= DistanceFromPlayer(x, y) - 1;
                            lien.x = x;
                            lien.y = y;
                            lien.canMove = false;
                            StartCoroutine(Bleh());
                        }
                    }
                    if (mappy[x, y] == 2)
                    {
                        //Exit Procedure
                    }
                }
            }
        }
    }

    bool testPath(int lx, int ly, int x, int y)
    {
        int dx = lien.x - x;
        int dy = lien.y - y;
        if(dx == 0)
        {
            int direction = -(int)Mathf.Sign(dy);
            dy = (int)Mathf.Abs(dy);
            Debug.Log(dy + " " + direction + " " + (ly + dy));
            for (int i = ly; i >= ly - dy && i <= ly + dy; i += direction)
            {
                if(mappy[x,i] == 1)
                {
                    return false;
                }
            }
        } else if(dy == 0)
        {
            int direction = -(int)Mathf.Sign(dx);
            dx = (int)Mathf.Abs(dx);
            for (int i = lx; i >= lx - dx && i <= lx + dx; i += direction)
            {
                if (mappy[i, y] == 1)
                {
                    return false;
                }
            }
        }
        return true;
    }

    IEnumerator Bleh()
    {
        yield return new WaitForSeconds(0.3F);
        if(burt != null)
        {
            burt.MoveIt();
        }
        if(roburt != null)
        {
            roburt.MoveIt();
        }
        for(int i = 0; i < keys.Length; i++)
        {
            keys[i].CheckForPick();
        }
        lien.canMove = true;
    }

    int DistanceFromPlayer(int x, int y)
    {
        return (int)Mathf.Abs(lien.x - x) + (int)Mathf.Abs(lien.y - y);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

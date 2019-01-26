using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class MapGenerator : MonoBehaviour
{

    public GameObject[] spritemap;
    int[,] mappy;
    public int maxMapX;
    public int maxMapY;
    public CharBehavior lien;
    public EnemyBehavior burt;
    public int lvl;

    public int startx;
    public int starty;

    public bool turn = true;

    void Start()
    {
            GenerateMapData();
            GenerateMapVisual();
    }

    void GenerateMapData()
    {
        lien.x = startx;
        lien.y = starty;
        lien.transform.SetPositionAndRotation(new Vector3(startx, starty, 0), Quaternion.identity);
        string pathForReader = "Assets/LevelData/lvl" + lvl + ".txt";
        StreamReader reader = new StreamReader(pathForReader);
        mappy = new int[maxMapX, maxMapY];
        for(int y = maxMapY-1; y >=0; y--)
        {
            string line;
            line = reader.ReadLine();
            for(int x = 0; x < maxMapX; x++)
            {
                string thing = line.Substring(3 * x, 2);
                int cheap = (int) float.Parse(thing);
                mappy[x, y] = cheap;
            }
        }
    }

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

    public void tryToMove(int x, int y)
    {
        if (lien.canMove)
        {
            if (lien.cooldown > 0)
            {
                lien.cooldown--;
            }
            else
            {
                if (mappy[x, y] == 0 || mappy[x, y] == 2)
                {
                    int dist = DistanceFromPlayer(x, y);
                    if (dist == 0 && lien.energy < 5)
                    {
                        lien.energy++;
                        Debug.Log("Added Energy" + lien.energy);
                    }
                    else if (dist == 1)
                    {
                        lien.transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
                        lien.x = x;
                        lien.y = y;
                    }
                    else if (dist > 1 && dist - 1 <= lien.energy)
                    {
                        //if (testPath(lien.x, lien.y, x, y, dist))

                        lien.transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
                        lien.cooldown = DistanceFromPlayer(x, y) - 1;
                        lien.energy -= DistanceFromPlayer(x, y) - 1;
                        lien.x = x;
                        lien.y = y;

                    }
                    if (mappy[x, y] == 2)
                    {
                        //Exit Procedure
                    }
                }
            }
            lien.canMove = false;
            StartCoroutine(bleh());
        }
    }

    IEnumerator bleh()
    {
        yield return new WaitForSeconds(0.3F);
        MoveEnemy();
    }

    int DistanceFromPlayer(int x, int y)
    {
        return (int)Mathf.Abs(lien.x - x) + (int)Mathf.Abs(lien.y - y);
    }

    void MoveEnemy()
    {
        int x = burt.x;
        int y = burt.y;
        if(x != maxMapX-1 && mappy[x+1,y] == 0 && burt.lastmove != 2)
        {
            burt.x++;
            burt.transform.SetPositionAndRotation(new Vector3(x+1, y, 0), Quaternion.identity);
            burt.lastmove = 0;
        } else if(y != maxMapY-1 && mappy[x,y+1] == 0 && burt.lastmove != 3)
        {
            burt.y++;
            burt.transform.SetPositionAndRotation(new Vector3(x, y + 1, 0), Quaternion.identity);
            burt.lastmove = 1;
        } else if(x != 0 && mappy[x-1, y] == 0  && burt.lastmove != 0)
        {
            burt.x--;
            burt.transform.SetPositionAndRotation(new Vector3(x - 1, y, 0), Quaternion.identity);
            burt.lastmove = 2;
        } else if(y != 0 && mappy[x,y-1] == 0 && burt.lastmove != 1)
        {
            burt.y--;
            burt.transform.SetPositionAndRotation(new Vector3(x, y - 1, 0), Quaternion.identity);
            burt.lastmove = 3;
        } else
        {
            burt.lastmove = 4;
        }
        lien.canMove = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

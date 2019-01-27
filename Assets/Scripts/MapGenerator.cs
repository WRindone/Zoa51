using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class MapGenerator : MonoBehaviour
{

    public GameObject[] spritemap;
    public int[,] mappy;
    public int maxMapX;
    public int maxMapY;
    public CharBehavior lien;
    public EnemyBehavior burt;
    public AnotherOne roburt;
    public int lvl;

    public int startx;
    public int starty;

    public bool turn = true;

    public KeyCardsScr[] butts;
    public KeyGateScript[] farts;

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
                for (int i = farts.Length-1; i >= 0; i--)
                {
                    if ((int)farts[i].position.x == x && (int)farts[i].position.y == y)
                    {
                        mappy[x, y] = 1;
                    }
                }
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
                StartCoroutine(Bleh());
            }
            else
            {
                if (mappy[x, y] == 0 || mappy[x,y] == 2)
                {
                    bool canMove = true;
                    for(var i = farts.Length-1; i >= 0; i--)
                    {
                        if (x == (int)farts[i].position.x && y == (int)farts[i].position.y && farts[i].isActiveAndEnabled)
                        {
                            canMove = false;
                        }
                    }
                    if (canMove == true)
                    {
                        int dist = DistanceFromPlayer(x, y);
                        if (dist == 0 && lien.energy < 5)
                        {
                            lien.energy++;
                            Debug.Log("Added Energy" + lien.energy);
                            lien.canMove = false;
                            StartCoroutine(Bleh());
                        }
                        else if (dist == 1)
                        {
                            lien.transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
                            lien.x = x;
                            lien.y = y;
                            lien.canMove = false;
                            StartCoroutine(Bleh());
                        }
                        else if (dist > 1 && dist - 1 <= lien.energy)
                        {
                            //if (testPath(lien.x, lien.y, x, y, dist))

                            lien.transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
                            lien.cooldown = DistanceFromPlayer(x, y) - 1;
                            lien.energy -= DistanceFromPlayer(x, y) - 1;
                            lien.x = x;
                            lien.y = y;
                            lien.canMove = false;
                            StartCoroutine(Bleh());
                        }
                        if (mappy[x, y] == 2)
                        {
                            //Exit Procedure
                        }
                    }
                    
                }
            }
        }
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
        for(int i = 0; i < butts.Length; i++)
        {
            butts[i].CheckForPick();
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

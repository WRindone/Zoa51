using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardsScr : MonoBehaviour
{
    public string color;
    // Start is called before the first frame update

    public Vector2 position;
    [SerializeField]
    private MapGenerator g;

    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(position.x, position.y, 0), Quaternion.identity);
    }

    public void CheckForPick()
    {
        if(position.x == g.lien.x && position.y == g.lien.y)
        {
            g.lien.keycards[color] = true;
            gameObject.SetActive(false);
            for (int i = g.gates.Length-1; i >= 0 ; i--)
            {
                if (g.gates[i].color == color)
                {
                    int x = (int) g.gates[i].position.x;
                    int y = (int)g.gates[i].position.y;
                    g.mappy[x, y] = 0;
                    g.gates[i].gameObject.SetActive(false);
                    Instantiate(g.spritemap[0], new Vector3(x, y, 0), Quaternion.identity);
                }
            }
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

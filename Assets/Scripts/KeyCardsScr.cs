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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

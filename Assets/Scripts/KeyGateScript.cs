using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGateScript : MonoBehaviour
{
    public string color;
    public Vector2 position;
    [SerializeField]
    private MapGenerator g;

    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(position.x, position.y, 0), Quaternion.identity);
    }
}

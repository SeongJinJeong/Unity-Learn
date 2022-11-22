using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        if (!player)
        {
            player = GameObject.Find("Player2");
        }
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        checkPos();
    }


    void checkPos()
    {
        if(player.transform.position != transform.position)
        {
            _changeCamerPos();
        }
    }

    void _changeCamerPos()
    {
        transform.position = new Vector3(player.transform.position.x, 1, -1);
    }
}

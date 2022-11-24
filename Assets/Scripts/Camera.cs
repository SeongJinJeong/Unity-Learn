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

    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (transform.position.x >= 25)
        {
            transform.position = new Vector3(24.9f, transform.position.y, -1);
            return;
        }
        else if (transform.position.x <= -25)
        {
            transform.position = new Vector3(-24.9f, transform.position.y, -1);
            return;
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, 1, -1);
            return;
        }
    }
}

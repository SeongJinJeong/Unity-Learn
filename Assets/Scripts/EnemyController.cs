using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool move = false;

    [SerializeField]
    float SPEED = 1.0f;

    string PLAYER_TAG = "Player";
    string MOVE_ANIM = "isMoving";

    float distance = 0.0f;

    Animator anim;
    SpriteRenderer sr;

    private void Awake()
    {
        anim= GetComponent<Animator>();
        sr= GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomPos;
        int random = UnityEngine.Random.Range(0,2);
        if(random == 1)
        {
            randomPos = new Vector3(25.5f, 2, 0);
        }
        else
        {
            randomPos = new Vector3(-25.5f, 2, 0);
        }

        transform.position = randomPos;

        Vector3 playerPos = GameObject.FindGameObjectWithTag(PLAYER_TAG).transform.position;
        distance = transform.position.x - playerPos.x;

        anim.SetBool(MOVE_ANIM, true);
    }

    // Update is called once per frame
    void Update()
    {

        moveToPlayer();
    }

    void LateUpdate()
    {
        checkReachEnd();
    }

    private void OnDestroy()
    {
        GameManager.getInstance().checkEnemyCount();   
    }

    void moveToPlayer()
    {
        if (distance < 0)
        {
            moveRight();
        } else
        {
            moveLeft();
        }
    }

    void moveRight()
    {
        float xPos = transform.position.x + (Time.deltaTime * SPEED);
        float yPos = transform.position.y;
        transform.position = new Vector3(xPos, yPos);

        sr.flipX = false;
    }

    void moveLeft()
    {
        float xPos = transform.position.x - (Time.deltaTime * SPEED);
        float yPos = transform.position.y;
        transform.position = new Vector3(xPos, yPos);

        sr.flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            onCollisionPlayer();
        }
    }

    void onCollisionPlayer()
    {
        SceneManager sMgr = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        sMgr.onGameEnd();
    }

    void checkReachEnd()
    {
        if(transform.position.x >= 26 || transform.position.x <= -26)
        {
            Destroy(gameObject);
        }
    }
}

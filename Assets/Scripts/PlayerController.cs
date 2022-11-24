using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float Speed = 10.0f;
    [SerializeField]
    float JumpHeight = 1.0f;

    float MAX_X = 25f;
    string MOVE_ANIM = "isMoving";
    string JUMP_ANIM = "isJump";

    bool isJump = false;
    bool isMoving = false;

    SpriteRenderer sr;
    Animator anim;
    Rigidbody2D myBody;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.getInstance().gameStart = true;
        anim.SetBool(MOVE_ANIM, false);
        transform.position = new Vector3(0f, 2f,0);
    }

    // Update is called once per frame
    void Update()
    {
        
            checkInput();
    }

    void checkInput()
    {
        var y = Input.GetAxisRaw("Vertical");
        var x = Input.GetAxisRaw("Horizontal");
        movePlayer(y,x);
        flipPlayer(x);
        jumpPlayer(y);
    }

    void movePlayer(float y, float x)
    {
        if( x != 0 )
        {
            if (Math.Abs(transform.position.x) >= MAX_X) {
                float xPos = transform.position.x;
                if (xPos >= 25)
                {
                    xPos = 24.9f;
                } else if(xPos <= -25)
                {
                    xPos = -24.9f;
                }
                transform.position = new Vector3(xPos, transform.position.y);
                return;
            }

            isMoving = true;
            float moveX = x * this.Speed * Time.deltaTime;
            transform.position += new Vector3(moveX, 0, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            if(this.isJump == false)
                anim.SetBool(MOVE_ANIM, true);
        } else
        {
            anim.SetBool(MOVE_ANIM, false);
        }
    }

    void flipPlayer(float x)
    {
        if(x < 0)
        {
            sr.flipX = true;
        } else if(x > 0)
        {
            sr.flipX = false;
        }
    }

    void jumpPlayer(float y)
    {
        if(y > 0 && isJump == false)
        {
            isJump = true;
            myBody.AddForce(Vector2.up * JumpHeight,ForceMode2D.Impulse);
            anim.SetBool(JUMP_ANIM, isJump);
            anim.SetBool(MOVE_ANIM, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.isJump = false;
            anim.SetBool(JUMP_ANIM, isJump);
        }
    }

    public void resetPlayer()
    {

    }
}

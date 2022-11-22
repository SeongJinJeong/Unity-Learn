using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float Speed = 1.0f;
    [SerializeField]
    float JumpHeight = 1.0f;

    float MAX_X = 25f;
    string MOVE_ANIM = "isMoving";
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
        anim.SetBool(MOVE_ANIM, false);
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
    }

    void checkInput()
    {
        var v = Input.GetAxisRaw("Vertical");
        var h = Input.GetAxisRaw("Horizontal");
        movePlayer(v,h);
        flipPlayer(h);
        jumpPlayer(v);
    }

    void movePlayer(float v, float h)
    {
        if( v != 0 || h != 0 )
        {
            if (Math.Abs(transform.position.x) >= MAX_X) {
                onReachEndPosition(h);
                return;
            }

            isMoving = true;
            float moveX = h * this.Speed;
            transform.position += new Vector3(moveX, 0, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.isJump = false;
        }
    }

    void onReachEndPosition(float x)
    {
        isMoving = false;
        float xPos = x < 0 ? (x * MAX_X) + 0.1f : (x * MAX_X) - 0.1f;
        transform.position = new Vector3(xPos ,transform.position.y);
    }
}

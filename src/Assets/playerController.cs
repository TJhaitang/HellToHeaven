using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Rigidbody2D rigid2;
    private GameObject s2;
    private CapsuleCollider2D feet1;
    private CapsuleCollider2D feet2;
    private Animator whiteAnime;
    private Animator blackAnime;
    public float jumpSpeed = 5f;
    public float runSpeed = 5f;
    private bool isHold1 = false;
    private bool isHold2 = false;
    // Start is called before the first frame update
    void Start()
    {
        s2 = GameObject.Find("blackSlime");
        myRigidBody = GetComponent<Rigidbody2D>();
        rigid2 = s2.GetComponent<Rigidbody2D>();
        feet1 = GetComponent<CapsuleCollider2D>();
        feet2 = s2.GetComponent<CapsuleCollider2D>();
        whiteAnime = GetComponent<Animator>();
        blackAnime = s2.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        holding();
        move();
        jump();
        anime();
        compass();
    }

    void anime()
    {
        blackAnime.SetBool("jump", rigid2.velocity.y > 0.0005);
        blackAnime.SetBool("fall", rigid2.velocity.y < -0.0005);
        blackAnime.SetBool("moveleft", rigid2.velocity.x < -0.0005);
        blackAnime.SetBool("moveright", rigid2.velocity.x > 0.0005);
        bool isLand2 = (feet2.IsTouchingLayers(LayerMask.GetMask("block")) || feet2.IsTouchingLayers(LayerMask.GetMask("ground1")) || feet2.IsTouchingLayers(LayerMask.GetMask("ground2")));
        blackAnime.SetBool("land", isLand2);
        blackAnime.SetBool("JK", isHold2);
        //白色史莱姆
        whiteAnime.SetBool("jump", myRigidBody.velocity.y > 0.0005);
        whiteAnime.SetBool("fall", myRigidBody.velocity.y < -0.0005);
        whiteAnime.SetBool("moveleft", myRigidBody.velocity.x < -0.0005);
        whiteAnime.SetBool("moveright", myRigidBody.velocity.x > 0.0005);
        bool isLand1 = (feet1.IsTouchingLayers(LayerMask.GetMask("block")) || feet1.IsTouchingLayers(LayerMask.GetMask("ground1")) || feet1.IsTouchingLayers(LayerMask.GetMask("ground2")));
        whiteAnime.SetBool("land", isLand1);
        whiteAnime.SetBool("JK", isHold1);
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isHold1 && (feet1.IsTouchingLayers(LayerMask.GetMask("block")) || feet1.IsTouchingLayers(LayerMask.GetMask("ground1")) || feet1.IsTouchingLayers(LayerMask.GetMask("ground2"))))
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
            }
            if (!isHold2 && (feet2.IsTouchingLayers(LayerMask.GetMask("block")) || feet2.IsTouchingLayers(LayerMask.GetMask("ground1")) || feet2.IsTouchingLayers(LayerMask.GetMask("ground2"))))
            {
                rigid2.velocity = new Vector2(rigid2.velocity.x, jumpSpeed);
            }
        }
    }

    void move()
    {
        float dir = Input.GetAxis("Horizontal");
        if (isHold1)
        {
            if (!feet1.IsTouchingLayers(LayerMask.GetMask("block")) && !feet1.IsTouchingLayers(LayerMask.GetMask("ground1")))
            {
                myRigidBody.velocity = new Vector2(0, 0);
            }
        }
        else
        {
            myRigidBody.velocity = new Vector2(dir * runSpeed, myRigidBody.velocity.y);
        }
        if (isHold2)
        {
            if (!feet2.IsTouchingLayers(LayerMask.GetMask("block")) && !feet2.IsTouchingLayers(LayerMask.GetMask("ground1")))
            {
                rigid2.velocity = new Vector2(0, 0);
            }
        }
        else
        {
            rigid2.velocity = new Vector2(0 - dir * runSpeed, rigid2.velocity.y);
        }
    }

    void holding()
    {
        isHold1 = Input.GetButton("hold1");
        isHold2 = Input.GetButton("hold2");
    }

    public bool isNextLevel()
    {
        return feet1.IsTouchingLayers(LayerMask.GetMask("ground2")) && feet2.IsTouchingLayers(LayerMask.GetMask("ground2"));
    }
    //↓↓↓不知道为啥突然用不了了
    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (feet1.IsTouchingLayers(LayerMask.GetMask("ground2"))
    //         && feet2.IsTouchingLayers(LayerMask.GetMask("ground2")))
    //     {
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //     }
    // }
    //↓↓↓于是乎在update里面调用这个了
    void compass()
    {
        if (feet1.IsTouchingLayers(LayerMask.GetMask("ground2"))
            && feet2.IsTouchingLayers(LayerMask.GetMask("ground2")))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

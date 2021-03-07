using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luffy2 : MonoBehaviour

{
    public Over Over;
    Rigidbody2D MDLuffy;
    public float maxSpeed;

    //Flip the character script
    bool flipLuffy = true;
    SpriteRenderer luffyFlip;

    //Saltar
    bool gomuGomuJump = true;
    bool floor = false;
    float checkFloor = 0.2f;
    public layerMask floorLayer;
    public transform checkFloor;
    public float heCanFly;

    private int coin;

    void Start()
    {
        MDLuffy = GetComponent<Rigidbody2D>();
        luffyFlip = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gomuGomuJump && floor && Input.GetAxis("Jump") > 0)
        {
            MDLuffy.velocity = new Vector2(MDLuffy.velocity.x, 0f);
            MDLuffy.AddForce(new Vector2(0, heCanFly), ForceMode2D.Impulse);
            floor = false;
        }

        floor = Physics2D.OverlapCircle(checkFloor.position, checkFloor, floorLayer);


        float move = Input.GetAxis("Horizontal");
        if (gomuGomuJump)
        {
            if (move > 0 && !flipLuffy)
            {
                voltear();
            }
            else if (move < 0 && flipLuffy)
            {
                voltear();
            }
            MDLuffy.velocity = new Vector2(move * maxSpeed, MDLuffy.velocity.y);
        }
        else
        {
            MDLuffy.velocity = new Vector2(0, MDLuffy.velocity.y);
        }
    }



    void voltear()
    {
        flipLuffy = !flipLuffy;
        luffyFlip.flipX = !luffyFlip.flipX;
    }



    public void jump()
    {
        gomuGomuJump = !gomuGomuJump;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ovni")
        {
            Over.lose();
        }
    }
    public void OnTriggerEnter2D(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            Coin = Coin + 1;
        }

    }
}
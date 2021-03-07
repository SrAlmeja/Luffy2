using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luffy : MonoBehaviour

{
    public Over Over;
    Rigidbody2D MDLuffy;
    public float maxspeed;

    //Voltear
    bool flipL = true;
    SpriteRenderer Luffyf;

    //Saltar
    bool gomugomujump = true;
    bool floor = false;
    float checkfloor = 0.2f;
    public LayerMask floorlayer;
    public Transform checkfloor;
    public float hecanfly;

    private int Coin;

    void Start()
    {
        MonkyDLuffy = GetComponent<Rigidbody2D>();
        Luffyf = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gomugomujump && floor && Input.GetAxis("Jump") > 0)
        {
            MDLuffy.velocity = new Vector2(MDLuffy.velocity.x, 0f);
            MDLuffy.AddForce(new Vector2(0, hecanfly), ForceMode2D.Impulse);
            floor = false;
        }

        floor = Physics2D.OverlapCircle(checkfloor.position, checkfloor, floorlayer);


        float move = Input.GetAxis("Horizontal");
        if (gomugomujump)
        {
            if (move > 0 && !flipL)
            {
                voltear();
            }
            else if (move < 0 && flipL)
            {
                voltear();
            }
            MDLuffy.velocity = new Vector2(move * maxspeed, MDLuffy.velocity.y);
        }
        else
        {
            MDLuffy.velocity = new Vector2(0, MDLuffy.velocity.y);
        }
    }



    void voltear()
    {
        flipL = !flipL;
        Luffyf.flipX = !Luffyf.flipX;
    }



    public void jump()
    {
        gomugomujump = !gomugomujump;
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
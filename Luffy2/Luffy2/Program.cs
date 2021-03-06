using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luffy : MonoBehaviour

{
    public Over Over;
    Rigidbody2D MDLuffy;
    public float maxspeed;

    //Voltear
    bool voltearL = true;
    SpriteRenderer Luffyv;

    //Saltar
    bool gomugomusalto = true;
    bool Suelo = false;
    float revisarsuelo = 0.2f;
    public LayerMask capasuelo;
    public Transform checarsuelo;
    public float esdelasquevuelan;

    private int Moneda;

    void Start()
    {
        MDLuffy = GetComponent<Rigidbody2D>();
        Luffyv = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gomugomusalto && Suelo && Input.GetAxis("Jump") > 0)
        {
            MDLuffy.velocity = new Vector2(MDLuffy.velocity.x, 0f);
            MDLuffy.AddForce(new Vector2(0, esdelasquevuelan), ForceMode2D.Impulse);
            Suelo = false;
        }

        Suelo = Physics2D.OverlapCircle(checarsuelo.position, revisarsuelo, capasuelo);


        float mover = Input.GetAxis("Horizontal");
        if (gomugomusalto)
        {
            if (mover > 0 && !voltearL)
            {
                voltear();
            }
            else if (mover < 0 && voltearL)
            {
                voltear();
            }
            MDLuffy.velocity = new Vector2(mover * maxspeed, MDLuffy.velocity.y);
        }
        else
        {
            MDLuffy.velocity = new Vector2(0, MDLuffy.velocity.y);
        }
    }



    void voltear()
    {
        voltearL = !voltearL;
        Luffyv.flipX = !Luffyv.flipX;
    }



    public void salta()
    {
        gomugomusalto = !gomugomusalto;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ovni")
        {
            Over.perder();
        }
    }
    public void OnTriggerEnter2D(Collider other)
    {
        if (other.gameObject.CompareTag("Moneda"))
        {
            other.gameObject.SetActive(false);
            Moneda = Moneda + 1;
        }

    }
}
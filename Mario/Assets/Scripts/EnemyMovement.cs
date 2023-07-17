using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    public  float enemySpeed = 1f;
    public Rigidbody2D rb;
    private Vector2 velocity;
    private float gravity = -9.81f;
    public bool move;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enabled = false;

    }

    private void Start()
    {
        velocity = Vector2.left;
    }
    private void OnBecameVisible() // enemy karakterimizn g�r�n�r olmas� i�in yapt�k
    {
        enabled = true;
    }

    private void OnBecameInvisible()//  enemy karakterimizn g�r�nmez olmas� i�in yapt�k
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rb.WakeUp(); // enemiyin hareketlerini uyand�r�r.
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;   // enemiyin hareketlerini k�s�ltar.
        rb.Sleep();
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }
    private void EnemyMove()
    {

        rb.velocity = velocity * enemySpeed;
       


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            if (rb.velocity.x == 0 && velocity == Vector2.left)
            {
                velocity = Vector2.right;
            }
            else
            {
                velocity = Vector2.left;
            }
        }
    }


}

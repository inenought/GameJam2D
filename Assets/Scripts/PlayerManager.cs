﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    float vida = 100;
    // 
    public float moveSpeed = 5f;
    public float jumpForce = 300f;
    public bool isGrounded;
    SpriteRenderer sprite;
    public SpriteRenderer bracosprite;
    Animator anim;

    Rigidbody2D rb;
    [SerializeField]
    float radius = 0.3f;
    [SerializeField] Transform checker;
    [SerializeField] LayerMask layer;
    //

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //move
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0);
        transform.Translate(movement);

        //flip
        if (Input.GetAxis("Horizontal") > 0)
        {
            sprite.flipX = false;
            bracosprite.flipX = false;
            anim.SetFloat("movement", 1);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
        sprite.flipX = true;
        bracosprite.flipX = true;
        anim.SetFloat("movement", 1); 
        }
        else anim.SetFloat("movement", 0);

        //jump
        isGrounded = Physics2D.OverlapCircle(checker.transform.position, radius, layer);
        if (isGrounded && Input.GetAxis("Vertical") > 0)
        {
            Jump();
            anim.SetTrigger("jump");
        }
    }

    void TakeDamageLife(float value)
    {
        vida -= value;
    }
    
    void IncreaseLife(float value)
    {
        vida += value;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Jump() 
    {
        rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checker.transform.position, radius);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //Player Movement: This Script controlls all player movement
    //And only player movement.

    public float speed = 5.0f;

    public Rigidbody2D rb; 

    public Animator animator;

    public Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }
}

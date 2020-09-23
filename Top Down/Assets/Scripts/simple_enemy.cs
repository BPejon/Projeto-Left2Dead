using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
   
public class simple_enemy : MonoBehaviour
{
    [Space]
    [Header ("status : ")]
    public int health = 2;
    public float move_speed = 4.0f;

    [Space]
    [Header ("Basic IA : ")]

    public float sightRange = 5.0f;
    public float attackRange = 3.0f;
    public bool playerInSightRange, playerInAttackRange;
    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 3.0f;
    // Attacking
    public float timeBetweenAtacks = 1.0f;
    bool alreadyAttacked;



    [Space]
    [Header ("Sprites : ")]
    public SpriteRenderer spriteRenderer;
    public Sprite dead_sprite;
    public Sprite standard;

    [Space]
    [Header ("Player : ")]
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    

    

   

    void OnCollisionEnter2D(Collision2D  other) {
        if (other.gameObject.tag.Equals("bullet"))
        {
            health = health - 1;
            
        }
        Debug.Log("squirtle hit");
        
        if (health == 0)
        {
            Debug.Log("squirtle Died");
            spriteRenderer.sprite = dead_sprite; 

        }
    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
   
public class simple_enemy : MonoBehaviour
{
    [Space]
    [Header ("status : ")]
    public GameObject enemy;
    public int health = 4;
    private int fullHealth;
    public float move_speed = 4.0f;

    public GameObject healthBarG;
    public float xBarOffSet;
    public float yBarOffSet;
    private Vector3 iniHealthScale;

    public GameObject healthBarR;


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
    

    private void Start() {
        iniHealthScale = new Vector3(healthBarG.transform.localScale.x, 
                                   healthBarG.transform.localScale.y,
                                   healthBarG.transform.localScale.z);
        fullHealth = health;
        xBarOffSet = healthBarG.transform.position.x - enemy.transform.position.x;
        yBarOffSet = healthBarG.transform.position.y - enemy.transform.position.y;
    }

    private void Update() {
        healthBarG.transform.position = new Vector2(enemy.transform.position.x + xBarOffSet,
                                                    enemy.transform.position.y + yBarOffSet); 
        healthBarR.transform.position = healthBarG.transform.position;
        
    }

   

    void OnCollisionEnter2D(Collision2D  other) {
        if (other.gameObject.tag.Equals("bullet"))
        {
            health = health - 1;
            Debug.Log((float) health/ (float)fullHealth);
            healthBarG.transform.localScale = new Vector3( iniHealthScale.x * ((float) health/ (float)fullHealth),
                                                            iniHealthScale.y,
                                                            iniHealthScale.z);

        }
        Debug.Log("squirtle hit");
        
        if (health <= 0)
        {
            Debug.Log("squirtle Died");
            spriteRenderer.sprite = dead_sprite; 

        }
    }





}

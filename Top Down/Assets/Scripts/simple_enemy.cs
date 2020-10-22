using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
   
public class simple_enemy : MonoBehaviour
{
    [Space]
    [Header ("status : ")]
    public GameObject enemy;    // gameobject do inimigo
    public int health = 4;      // a quantidade de vida do inimigo
    private int fullHealth;     // a quantidade de vida máxima inicial do inimigo
    public float move_speed = 4.0f; // a velocidade de movimento do inimigo

    public GameObject healthBarG;   // objeto barra de vida verde
    private Vector3 iniHealthScale; // scale inicial da barra de vida

    public GameObject healthBarR; // barra de vida vemelha
    private float timedied = -1f; // tempo em que o inimigo morreu
    private float timedie = 2f; // tempo até o inimigo depois de morto desaparecer
    private bool isDead = false;
    public Animator animator;
    Vector3 movement;
    Vector3 prevLoc;

    [Space]
    [Header("dead")]
    public ParticleSystem bloodParticle;







    

    private void Start() {
        // define o scale da barra de vida inicial
        iniHealthScale = new Vector3(healthBarG.transform.localScale.x, 
                                   healthBarG.transform.localScale.y,
                                   healthBarG.transform.localScale.z);
        // define quanto é vida cheia
        fullHealth = health;
        // define a posição da barra de vida do inimigo
        
        
    }

    private void Update() {
        // faz a barra de vida ficar em cima do inimigo


        
        
        
    }

    private void FixedUpdate() {
      
        if (Time.time - timedied >= timedie && isDead)
        {
            Destroy(gameObject);
        }


        movement = (  transform.position - prevLoc)/ Time.deltaTime;

        
        // Debug.Log(movement.sqrMagnitude);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetBool("isDead", isDead);


        prevLoc = transform.position;
    }

   

    // define colisor
    void OnCollisionEnter2D(Collision2D  other) {

        // de colidir com bala então diminui a vida
        if (other.gameObject.tag.Equals("playerBullet"))
        {
            health = health - 1;
            // se a vida for maior igual a zero, re-escala a vida verde
            if (health >= 0)
            {
                healthBarG.transform.localScale = new Vector3( iniHealthScale.x * ((float) health/ (float)fullHealth),
                                                            iniHealthScale.y,
                                                            iniHealthScale.z);
            }
        }
        
        
        // se a vida for menor que zero, fala que o inimigo morreu e troca de sprite.
        if (health <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                timedied = Time.time;
            }
            
            Debug.Log("squirtle Died");
        }
    }





}

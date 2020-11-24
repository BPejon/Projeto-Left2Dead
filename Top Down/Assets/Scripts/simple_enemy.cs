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
    public bool isDead = false;
    public Animator animator;
    Vector3 movement;
    Vector3 prevLoc;

    [Space]
    [Header("dead")]
    public ParticleSystem bloodParticle;

    [Space]
    [Header("KnockBack")]

    public float kbspeed;
    public float kbtime;

    public float kbdur;

    public bool knockd;
    
    public bool kill;

    public bool hit = false;

    public KBReport fevent;



    

    private void Start() {
        // define o scale da barra de vida inicial
        iniHealthScale = new Vector3(healthBarG.transform.localScale.x, 
                                   healthBarG.transform.localScale.y,
                                   healthBarG.transform.localScale.z);
        // define quanto é vida cheia
        fullHealth = health;
        // define a posição da barra de vida do inimigo
        
        //Partes do knockback
        fevent = new KBReport();
        fevent.dir = new Vector2(0,0);
        fevent.kbdur = 0f;
        fevent.kbspeed = 0f;
    }

    private void Update() {
        // faz a barra de vida ficar em cima do inimigo

        //DealKnockback(0);
        
        
        
    }

    private void FixedUpdate() {
      
        KnockBackProgrssion();
        
        

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

    //Teste usando trigger ao inves de collisao.
    void OnTriggerEnter2D(Collider2D other){
       // Debug.Log("Zombie Side collision");
        // de colidir com bala então diminui a vida
        if (other.gameObject.tag.Equals("playerBullet"))
        {
            health = health - other.gameObject.GetComponent<BClass>().damage;
            //Parte experimental Knockback
            KBReport kb = other.gameObject.GetComponent<BClass>().GetKBReport();
           // Debug.Log("Hit - Kb from:" + kb.dir);
            fevent = new KBReport();
            fevent = kb;
            //gameObject.transform.position = gameObject.transform.position + kb.dir*2;
            //DealKnockback(1);
            hit = true;

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

   /*
    // define colisor - Por fisica
    void OnCollisionEnter2D(Collision2D  other) {

        // de colidir com bala então diminui a vida
        if (other.gameObject.tag.Equals("playerBullet"))
        {
            health = health - other.gameObject.GetComponent<BClass>().damage;
            //Parte experimental Knockback
            KBReport kb = other.gameObject.GetComponent<BClass>().GetKBReport();
            Debug.Log("Hit - Kb from:" + kb.dir);
            fevent = new KBReport();
            fevent = kb;
            //gameObject.transform.position = gameObject.transform.position + kb.dir*2;
            //DealKnockback(1);
            hit = true;

            // se a vida for maior igual a zero, re-escala a vida verde
            if (health >= 0)
            {
                healthBarG.transform.localScale = new Vector3( iniHealthScale.x * ((float) health/ (float)fullHealth),
                                                            iniHealthScale.y,
                                                            iniHealthScale.z);
            }
            //Destroy(other.gameObject);
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
    }*/
    
    //Knockback and friends; 
    public void DealKnockback(int f){
        //start knockback
        if(f == 1 && !knockd &&!isDead){
            knockd = true;
            kbtime = Time.time;
        }
        else if((Time.time - kbtime) >= fevent.kbdur){
            if(knockd){
                kill = true;
            }
            knockd = false;
            hit = false;
        }
    }

    public void KnockBackProgrssion(){
        Vector2 pastspeed = gameObject.GetComponent<Rigidbody2D>().velocity;
        //knockback portion
        if(knockd){ 
            Vector2 aux;
            aux.x = fevent.dir.x;
            aux.y = fevent.dir.y;
            //Crazy change incoming:
            gameObject.GetComponent<Rigidbody2D>().velocity = fevent.kbspeed * aux;
            //gameObject.GetComponent<Rigidbody2D>().velocity += fevent.kbspeed * aux;
            
        }
        else if(kill){
            Vector2 aux;
            aux.x = 0;
            aux.y = 0;
            
            //player.GetComponent<Rigidbody2D>().velocity -= kbspeed * pastaux;
            gameObject.GetComponent<Rigidbody2D>().velocity = aux;
            kill = false;
        }
    }

    public void SingleKBFunc(int f){

    }

}

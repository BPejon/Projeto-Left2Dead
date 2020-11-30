using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGotHit : MonoBehaviour
{
    [Space]
    [Header("status")]
    public int maxHealth = 20;
    public int health;


    [Space]
    [Header ("knockback")]
    public NewPlayerMovement moveScript;
    public playermelee meleeScript;
    public float ForceBackBulletHit;
    public float ForceBackEnemyHit;
    public float TimeBack;
    public bool isBackEnemy;
    public bool isBackBullet;
    public bool isBack;

    [Space]
    [Header ("Damage on hit")]
    public int enemyHitDamage;
    public int enemyBulletHitDamage;




    [Space]
    [Header ("time Imune")]
    public float TimeImune;
    public bool isImune = false;

    float timeStartHit;



    public HealthBar healthBar;

    Vector2 direction;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        timeStartHit = -20.0f;
        moveScript =  gameObject.GetComponent<NewPlayerMovement>();
        meleeScript = gameObject.GetComponent<playermelee>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        isBackBullet = false;

        int default_ = PlayerPrefs.GetInt("use_default");
        if(default_ != 1){
            health = PlayerPrefs.GetInt("hp");
        }
        else{
            health = maxHealth;
        
        }
        //vida maxima na Barra de Vida
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBackBullet)
        {
            rb.velocity = (direction * ForceBackBulletHit);
            if ( Time.time - timeStartHit > TimeBack)
            {
                isBackBullet = false;
                isBack = false;
            }       
        }

        if (isBackEnemy)
        {
            rb.velocity = (direction * ForceBackEnemyHit);
            if (Time.time - timeStartHit > TimeBack)
            {
                isBackEnemy = false;
                isBack = false;
            }
        }

        if (isImune)
        {
            if (Time.time - timeStartHit > TimeImune)
            {
                isImune = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D  other) {
        
        if (!isImune && 
            !meleeScript.isAttackingMelee &&
            !moveScript.isOnDash &&
            (other.gameObject.CompareTag("Enemy") ||
            other.gameObject.CompareTag("enemyBullet")))
        {
            
            if (!isBackEnemy && other.gameObject.CompareTag("Enemy") ){
                isBackEnemy = true;
                health -= enemyHitDamage;
                healthBar.SetHealth(health);
            }
            
            
            Transform enemyTransform = other.gameObject.GetComponent<Transform>();
            
            direction = ((Vector2)(transform.position) - (Vector2)enemyTransform.position).normalized;
            
            
            isImune = true;
            isBack = true;

            timeStartHit = Time.time;
        }
        
    }
    public void gotHitByBuleet(Transform bbPosition){
        if (!isBackBullet && !isImune && !moveScript.isOnDash){
            isBackBullet = true;
            isBack = true;
            health -= enemyBulletHitDamage;

            //muda a barra de vida
            healthBar.SetHealth(health);

            direction = ((Vector2)(transform.position) - (Vector2)bbPosition.position).normalized;
            isImune = true;
            timeStartHit = Time.time;
        }

    }    


}

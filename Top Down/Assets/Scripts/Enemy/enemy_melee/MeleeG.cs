using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeG : MonoBehaviour
{
    public float ForceBackEnemy;
    public float TimeBetHit;
    public bool isHit = false;

    float TimeStartHit;
    Vector2 pushDirection;

    // Start is called before the first frame update
    void Start()
    {
        TimeStartHit = -20.0f;
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void FixedUpdate() {


        if (isHit && Time.time - TimeStartHit > TimeBetHit)
        {
            isHit = false;
        }
        if (isHit){
            Debug.Log("entrei aqui");
            GetComponent<Rigidbody2D>().velocity = (pushDirection * ForceBackEnemy * Time.fixedDeltaTime);
        }
        
    }
    void checkIfHitByPlayer(Collision2D  other){
        if (other.gameObject.CompareTag("Player")){
            // se atacarmos um inimigo precisamos joga-lo para trás
            // vamos criar um vetor na direcao do inimigo
            
            playermelee playerMeleeScript = other.gameObject.GetComponent<playermelee>();
            if (playerMeleeScript.isAttackingMelee && !isHit){
                Rigidbody2D rbEnemy = gameObject.GetComponent<Rigidbody2D>();
                Transform playerTransform = other.gameObject.GetComponent<Transform>();
                EnemyShotAI enemyScriptAI = gameObject.GetComponent<EnemyShotAI>();
                simple_enemy enemyScript = gameObject.GetComponent<simple_enemy>();
            
                pushDirection = (((Vector2)transform.position - (Vector2)playerTransform.position)).normalized;
                enemyScript.health -= 2; 
                
                TimeStartHit = Time.time;
                isHit = true;
            }
        }
    }
    


    void OnCollisionEnter2D(Collision2D other) {
        checkIfHitByPlayer(other);
    }
}

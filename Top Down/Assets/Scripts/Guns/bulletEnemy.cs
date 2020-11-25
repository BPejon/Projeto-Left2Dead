using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float ForceBack;
    playermelee playerMScript;
    PlayerGotHit playerGHScript;

    void Awake() {
        playerMScript = GameObject.Find("player").GetComponent<playermelee>();
        playerGHScript = GameObject.Find("player").GetComponent<PlayerGotHit>();

    }
    

    void checkIfplayerAttacking(Collider2D other){
        if  (playerMScript.isAttackingMelee)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            // adiciona uma força que define a movimentaçao da bala
            rbb.AddForce(transform.up * (-ForceBack), ForceMode2D.Impulse);
            // depois de 2 segundos o projétil é destruido
            Destroy(bullet, 8.0f);
        }else{
            playerGHScript.gotHitByBuleet(transform);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other){
     
        if (!other.gameObject.CompareTag("enemyBullet") && 
            !other.gameObject.CompareTag("DroppedItem") && 
            !other.gameObject.CompareTag("Enemy") )
        {
            if (other.gameObject.CompareTag("Player"))
                checkIfplayerAttacking(other);
            

            Destroy(gameObject);
        }
    }
}

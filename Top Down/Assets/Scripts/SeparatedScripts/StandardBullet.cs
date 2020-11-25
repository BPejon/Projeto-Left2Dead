using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : BClass
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnTriggerEnter(Collider other){
        Debug.Log("Collision");
        if(!other.gameObject.CompareTag("playerBullet")){
            Destroy(gameObject);
        }
    }*/
    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log("Collision");
        if (!other.gameObject.CompareTag("playerBullet") &&
            !other.gameObject.CompareTag("DroppedItem") && 
            !other.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }


    /*void OnCollisionEnter2D(Collision2D  other) {
        if (!other.gameObject.CompareTag("playerBullet")){
            Destroy(gameObject);
        }
    }*/
}

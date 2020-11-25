using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log("Collision");
        if (!other.gameObject.CompareTag("playerBullet") && 
            !other.gameObject.CompareTag("DroppedItem") && 
            !other.gameObject.CompareTag("Player") )
        {
            Destroy(gameObject);
        }
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gore : MonoBehaviour
{
    [Space]
    [Header("parts")]
    public GameObject[] parts = new GameObject[5];
    public GameObject bloodPool;
    public float forcePart;
    public float GravityS;
    
    


    Transform player;
    GameObject part;
    simple_enemy simpleScript;
    int inicialHealth;
    bool drop2=false;

    float startTime;
    float TotalTime =5.0f;
    Rigidbody2D rbb;

    // Start is called before the first frame update
    void Start()
    {
        startTime = -20.0f;
        player = GameObject.Find("player").transform;
        simpleScript = gameObject.GetComponent<simple_enemy>();
        inicialHealth = simpleScript.health; 
    }

    // Update is called once per frame
    void Update()
    {
        if(drop2 && Time.time - startTime < 1f){
            rbb.transform.Rotate(0f, 0f, 5f, Space.Self);
        }

        if(drop2 && Time.time - startTime > 1f && Time.time - startTime < TotalTime){
            
            if (rbb.gravityScale > 0)
            {
                Debug.Log("enrew");
                rbb.gravityScale=0;
                rbb.velocity = Vector2.zero;
                rbb.angularVelocity = 0f;
            }

        }
        

        if (simpleScript.health <= (int)(inicialHealth/2) && !drop2)
        {
            drop2=true;
            part = Instantiate(parts[Random.Range(0,4)], transform.position, transform.rotation);
            rbb = part.GetComponent<Rigidbody2D>();
            rbb.gravityScale= GravityS;
            rbb.angularVelocity = 5f;

            

            // joga a parte para cima;
            Vector2 m_NewPosition = new Vector2(0.0f, 0.7f);
            Vector2 direction = ((Vector2)transform.position - (Vector2)player.position + m_NewPosition).normalized;

            
            rbb.AddForce(direction * forcePart, ForceMode2D.Impulse);
            startTime = Time.time;
            Destroy(part, TotalTime);
        }
        
        
    }
    

}

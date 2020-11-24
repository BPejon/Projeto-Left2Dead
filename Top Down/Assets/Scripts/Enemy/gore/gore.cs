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
    public float timeMoving;
    


    Transform player;
    GameObject part;
    GameObject blood;
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
    void FixedUpdate()
    {
        

        if(drop2 && Time.time - startTime > timeMoving && Time.time - startTime < TotalTime){
            
            if (rbb.gravityScale > 0)
            {
                rbb.gravityScale=0;
                rbb.velocity = Vector2.zero;
                rbb.angularVelocity = 0f;
                blood = Instantiate(bloodPool, part.transform.position - new Vector3(0f,0.3f,0f)
                                    , new Quaternion(0f, 0f , 0f, 0f));
                Destroy(blood, TotalTime - timeMoving );
            }
        }
        if(drop2 && Time.time - startTime < timeMoving){
            rbb.transform.Rotate(0f, 0f, 20f, Space.Self);
        }
        if(Time.time - startTime > timeMoving && Time.time - startTime < TotalTime){
            blood.transform.localScale += new Vector3(0.005f,0.005f,0.005f);
        }


        if (simpleScript.health <= (int)(inicialHealth/2) && !drop2)
        {
            drop2=true;
            part = Instantiate(parts[Random.Range(0,4)], transform.position, transform.rotation);
            rbb = part.GetComponent<Rigidbody2D>();
            rbb.gravityScale= GravityS;
            rbb.angularVelocity = 5f;

            

            // joga a parte para cima;
            Vector2 m_NewPosition = new Vector2(0.0f, 3f);
            Vector2 direction = ((Vector2)transform.position - (Vector2)player.position ).normalized;
            direction = (m_NewPosition + direction).normalized;
            
            rbb.AddForce(direction * forcePart, ForceMode2D.Impulse);
            startTime = Time.time;
            Destroy(part, TotalTime);
        }
        
        
    }
    

}

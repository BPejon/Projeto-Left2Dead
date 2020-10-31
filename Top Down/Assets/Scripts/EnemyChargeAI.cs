using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChargeAI : MonoBehaviour
{
     [Space]
    [Header("player")]
    // alvo
    public Transform target;
    private Transform player;
   

    
    [Space]
    [Header("Patroling")]
    public float timeChangePoint = 1.5f; 
    public float walkPointRange = 10f;
    public float lastTime = 0f;



    [Space]
    [Header("States")]
    public bool onChase = false;
    public float sightRange = 10;
    public float attackRange;
    // verifica o inimigo está persguindo o player
    [Space]
    [Header("Charge")]
    Vector2 directionCharge;
    public bool onAttack = false;
    public bool onCharge = false;
    public float forceCharge;
    private float startTimeCharge = -2;
    private float lasTimeAttack = -2;
    public float time_charging;
    public float time_attacking;
    public float time_between_attacks = 5;

    private float inicial_speed;
    public bool playerInSightRange, playerInAttackRange;



    [Space]
    [Header("atributes")]
    // script do basico do inimigo
    private simple_enemy enemyScript;

    // speed
    public float speed = 5f;
    public int health = 4;
    // proximo ponto de escolha
    public float nextWaypointDistance = 5f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    

    void Awake() {
        player = GameObject.Find("player").transform;
        
        enemyScript = gameObject.GetComponent<simple_enemy>();


        GameObject newEmptyGO = new GameObject();
        
        target = newEmptyGO.transform;
        inicial_speed = speed;
        target.position = new Vector2(transform.position.x , transform.position.y);
    }


    // Start is called before the first frame update
    void Start()
    {
        

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        getRandomPoint();
        

        InvokeRepeating("UpdatePath", 0f, .5f);

    }

    void UpdatePath()
    {
        if(target == null)
            return;

        if (seeker.IsDone()){
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void getRandomPoint()
    {
        /* gera um ponto aleatório dentro do rangenpoint*/
        target.position = new Vector2 (
                    transform.position.x + Random.Range(-walkPointRange, walkPointRange),
                    transform.position.y + Random.Range(-walkPointRange, walkPointRange)
        );
    }

    void OnPathComplete(Path p )
    {
        
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }

    }
    
    bool checkIfInSight(){
        /* verificando se está na área de alcance de visão do inimigo */
        float distance = Vector2.Distance(transform.position, player.position);
        playerInSightRange = distance <= sightRange;
    
        if (playerInSightRange)
        {
            onChase = true;
            target = player;
        }
        return playerInSightRange;
    }

    


    bool checkIfInAttackRange(){
        float distance = Vector2.Distance(transform.position, player.position);
        playerInAttackRange = distance <= attackRange;
     
        if (Time.time - lasTimeAttack > time_between_attacks && playerInAttackRange)
        {
            startTimeCharge = Time.time;
            onAttack = true;
            onChase = false;
            speed = 0.0f;
            target = player;
        }
        return playerInAttackRange;
    }

    


    // Update is called once per frame
    void FixedUpdate()
    {
        

        if(target == null)
        {
            return;
        }

        if(path == null)
            return;
        if ( currentWaypoint >= path.vectorPath.Count){
            reachedEndOfPath = true;
            return;
        } else 
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        if(!onCharge){
            rb.velocity = (direction* speed * Time.fixedDeltaTime);
        }else{
            rb.AddForce(direction * forceCharge );
        }
        // rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (direction.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
        else if (direction.x <=  -0.01f)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
        }
        
        if (enemyScript.health > 0){
            if (!onChase && !onAttack)
            {
                checkIfInSight();
            }
            if (!onAttack){
                checkIfInAttackRange();
            }if(onAttack && Time.time - startTimeCharge < time_charging){
                directionCharge = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            }
            if (onAttack && Time.time - startTimeCharge > time_charging && Time.time - startTimeCharge < time_charging + time_attacking){
                /*podemos atacar adicionar a forca*/
                Debug.Log("get ready");
                onCharge = true;
            }else
            if (onAttack && Time.time - startTimeCharge > time_charging + time_attacking){
           
                /*paramos de adicionar força  e paramos de atacar*/
                lastTime = Time.time;
                Debug.Log("atacar");
                onCharge = false;
                onAttack = false;
                onChase = true;
                speed = inicial_speed;
            }


            /* verificando tempo de troca pontos para se andar (para fazer patroling) */
            if (Time.time - lastTime >= timeChangePoint && !onChase && !onAttack)
            {
                getRandomPoint();
                lastTime = Time.time;
            }
        }else{
            speed = 0.0f;
            target = null;
        }        
    }
}

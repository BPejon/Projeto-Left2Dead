using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    // inimigo
    GameObject enemy;

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
    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;


    [Space]
    [Header("States")]
    public float sightRange = 10;
    public float attackRange;
    // verifica o inimigo está persguindo o player
    public bool onChase = false;
    public bool playerInSightRange, playerInAttackRange;


    [Space]
    [Header("atributes")]
    // speed
    public float speed = 5f;
    // proximo ponto de escolha
    public float nextWaypointDistance = 5f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Awake() {
        player = GameObject.Find("player").transform;

        GameObject newEmptyGO = new GameObject();
        
        target = newEmptyGO.transform;

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
        // transform.LookAt(player);
        float distance = Vector2.Distance(transform.position, player.position);
        playerInSightRange = distance <= sightRange;
    
        if (playerInSightRange)
        {
            onChase = true;
            target = player;
        }
        return playerInSightRange;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if(target == null)
            return;

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

        

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (direction.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
        }
        else if (direction.x <=  -0.01f)
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
        
        if (!onChase)
        {
            checkIfInSight();
        }

        /* verificando tempo de troca pontos para se andar (para fazer patroling) */
        if (Time.time - lastTime >= timeChangePoint && !onChase)
        {
            getRandomPoint();
            lastTime = Time.time;
        }
        

        
    }
}

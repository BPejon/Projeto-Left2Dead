using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyShotAI : MonoBehaviour
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
    public float sightRange = 10;
    public float attackRange;
    // verifica o inimigo está persguindo o player
    public bool onChase = false;
    public bool playerInSightRange, playerInAttackRange;


    [Space]
    [Header ("gun : ")]
    public float timeBetweenShoots;
    public float lastTimeShoot;
    public Transform weapon;       // define a arma
    private float angle;    // define o ângulo da arma
    private bool is_invert = false;    // verifica se a mira está invertida ( do lado esquerdo)


    [Space]
    [Header("Prefabs :")]
    public GameObject bulletPrefab; // define o objeto da bala
    public Transform firePoint;     // define de onde sai a bala
    public float BULLET_BASE_SPEED = 19.0f; // define a velocidade do projetil.


    [Space]
    [Header("atributes")]
    // script do basico do inimigo
    private simple_enemy enemyScript;
    // speed
    public float speed = 5f;
    // proximo ponto de escolha
    float speedIni = 0;
    public float nextWaypointDistance = 5f;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;
    Rigidbody2D rb;

    

    void Awake() {
        player = GameObject.Find("player").transform;
        enemyScript = gameObject.GetComponent<simple_enemy>();

        lastTimeShoot = -20.0f;
        GameObject newEmptyGO = new GameObject();
        
        target = newEmptyGO.transform;
        speedIni = speed;
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

    void slow_when_preper_to_shoot(){
        if (Time.time - lastTimeShoot > timeBetweenShoots/2 && onChase)
        {
            speed = speedIni/2;
        }
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
            return;
        } 
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        
        rb.velocity = (direction* speed * Time.fixedDeltaTime);
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
            if (!onChase)
            {
                checkIfInSight();
            }
            slow_when_preper_to_shoot();
            if (onChase){
                roateWeapon();
                if(Time.time - lastTimeShoot > timeBetweenShoots && Random.Range(0,10) == 2 && onChase)
                {   
                    Shoot();
                    speed = speedIni;
                    lastTimeShoot = Time.time;
                }
            }
            

            /* verificando tempo de troca pontos para se andar (para fazer patroling) */
            if (Time.time - lastTime >= timeChangePoint && !onChase)
            {
                getRandomPoint();
                lastTime = Time.time;
            }
        }else{
            speed = 0.0f;
            target = null;
        }
    }

    // função de tiro do player
    void Shoot(){
        
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
        // adiciona uma força que define a movimentaçao da bala
        rbb.AddForce(firePoint.up * BULLET_BASE_SPEED, ForceMode2D.Impulse);
        // depois de 2 segundos o projétil é destruido
        Destroy(bullet, 8.0f);
        
    }
    void roateWeapon(){
        // essas parte é para definir para onde o personagem está atirando
        Vector2 auxVector = (firePoint.position);
        Vector2 auxVector2 = (player.position);
        Vector2 lookDir =  auxVector2 - auxVector;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        if ( -90.0 <= angle && angle <= 90.0){
            is_invert = false;
        }else{
            is_invert = true;
        }


        if(is_invert){
            //rotaciona a arma caso esteja invertido
            weapon.eulerAngles = new Vector3(
                weapon.eulerAngles.x,
                weapon.eulerAngles.y,
                angle + 180
            );
        }else{
            weapon.eulerAngles = new Vector3(
                weapon.eulerAngles.x,
                weapon.eulerAngles.y,
                angle
            );
        }
    }
}



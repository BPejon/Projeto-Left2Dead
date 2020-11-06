using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyShotGunAI : MonoBehaviour
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
    float lastTime = 0f;




    [Space]
    [Header("States")]
    public float sightRange = 10;
    public float attackRange;
    // verifica o inimigo está persguindo o player
    public bool onChase = false;
    public bool playerInSightRange, playerInAttackRange;


    [Space]
    [Header ("gun : ")]
    Vector2 lookDir;
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
    public int health = 4;
    // proximo ponto de escolha
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



    // Update is called once per frame
    void FixedUpdate()
    {

        if(target == null)
        {
            return;
        }

         // essas parte é para definir para onde o personagem está atirando
        Vector2 auxVector = (firePoint.position);
        Vector2 auxVector2 = (player.position);
        lookDir =  auxVector2 - auxVector;

        roateWeapon();
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
            if (onChase){
                roateWeapon();
                if(Time.time - lastTimeShoot > timeBetweenShoots && Random.Range(0,10) == 2)
                {   
                    Debug.Log("atirei");
                    ShootShotgun();
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


  

    void ShootShotgun(){
        int numberOfbullets = 5;
        float[] anglesBet = new float[5];


        

        
        GameObject[] bullet = new GameObject[numberOfbullets];
        Rigidbody2D[] rbb = new Rigidbody2D[numberOfbullets];
        for (int i = 0; i < numberOfbullets; i++)
        {
            bullet[i] = Instantiate(bulletPrefab, firePoint.position, 
                                    firePoint.rotation ); 
            rbb[i] = bullet[i].GetComponent<Rigidbody2D>();
            anglesBet[i] = Random.Range(-70,70);
            // adiciona uma força que define a movimentaçao da bala
            Vector2 directorShot = Quaternion.AngleAxis(anglesBet[i],Vector2.up) * lookDir;
            Debug.Log(bullet[i]);
            
            rbb[i].AddForce(directorShot.normalized * BULLET_BASE_SPEED, ForceMode2D.Impulse);
            // depois de 2 segundos o projétil é destruido
            Destroy(bullet[i], 1.2f);
        }
        
    }


    void roateWeapon(){
       
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



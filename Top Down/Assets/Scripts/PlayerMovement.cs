using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [Space]
    [Header("player")]
    public float moveSpeed = 5f;    //define a valocidade de movimento do personagem
    public Rigidbody2D rb;          // define o objeto de Rigidbody do personagem principal
    public Animator animator1;      // define o animator do personagem principal
    public GameObject player;       //define o game object do jogador
    public Camera cam;              // define a camera
    private Vector3 player_scale_ini;  // define o scale inicial do jogador


    [Space]
    [Header ("crosshair : ")]
    public GameObject crosshair; //define a mira
    public bool endOfAiming;    // define se atirou "acho q é isso kkkkkkkkkkkk"

    [Space]
    [Header ("gun : ")]
    // public Rigidbody2D rbw;
    public GameObject weapon;       // define a arma
    public SpriteRenderer weapon_sprite_render;  // define o sprite da arma
    public Sprite[] weapon_sprites = new Sprite[6]; 
    public float off_set_x; //the of set from the char 
    public float off_set_y; //the of set from the char 
    private float angle;    // define o ângulo da arma
    private bool is_invert = false;    // verifica se a mira está invertida ( do lado esquerdo)
    /*
        weapon_equip define a arma que o player está equipado
        0 - pistola
        1 - metralhadora
        2 - shotgun
        3 - sniper
    */
    private int weapon_equip = 0;
    


    [Space]
    [Header("vector2 : ")]
    Vector2 movement;   // definem movimeto
    Vector2 mousePos;   // define a posição do mouse

    [Space]
    [Header("Prefabs :")]
    public GameObject bulletPrefab; // define o objeto da bala
    public Transform firePoint;     // define de onde sai a bala
    public float BULLET_BASE_SPEED = 19.0f; // define a velocidade do projetil.


    [Space]
    [Header("Dash : ")]
    public ParticleSystem dashParticle;
    public float dashSpeed; // valocidade do dash
    public float dashTime;  // tempo entre dash
    public float dashDuration; // tempo que um dash dura
    public float startDashTime; // tempo inicial do dash
    public bool isOnDash = false;


    void Start() {
        // guarda o scaling inicial do jogador
        player_scale_ini = new Vector3(player.transform.localScale.x, 
                                   player.transform.localScale.y,
                                   player.transform.localScale.z);
        weapon_sprite_render.sprite = weapon_sprites[weapon_equip];
        startDashTime = -2;
    }   



    // Update is called once per frame
    void Update()
    {
        
        

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement_dash();

        if(Input.GetKeyDown("1")){
            weapon_equip = 0;
        }
        if(Input.GetKeyDown("2")){
            weapon_equip = 1;
        } 
        if(Input.GetKeyDown("3")){
            weapon_equip = 2;
        } 
        if(Input.GetKeyDown("4")){
            weapon_equip = 3;
        }
        weapon_sprite_render.sprite = weapon_sprites[weapon_equip];

              
    
        // verifica se atirou, se atirar endOfAiming fica verdadeiro
        endOfAiming = Input.GetButtonUp("Fire1");
        // recebe onde está o mouse
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    

        animator1.SetFloat("Horizontal", movement.x);
        animator1.SetFloat("Vertical", movement.y);
        animator1.SetFloat("Speed", movement.sqrMagnitude);

        
        // movimenta a arma
        weapon.transform.position = new Vector2(rb.position.x + off_set_x, rb.position.y + off_set_y); 
        cam.transform.position = new Vector3(rb.position.x, rb.position.y, -10);

        // faz a transformação do ângulo da arma caso o personagem olhe para a esquerda
        if(is_invert){
            //rotaciona a arma caso esteja invertido
            weapon.transform.eulerAngles = new Vector3(
                weapon.transform.eulerAngles.x,
                weapon.transform.eulerAngles.y,
                angle + 180
            );
            weapon.transform.position = new Vector2(rb.position.x - off_set_x, rb.position.y + off_set_y); 

        }else{
            weapon.transform.eulerAngles = new Vector3(
                weapon.transform.eulerAngles.x,
                weapon.transform.eulerAngles.y,
                angle
            );
            weapon.transform.position = new Vector2(rb.position.x + off_set_x, rb.position.y + off_set_y); 

        }
        
        switch (weapon_equip)
        {
            case 0:
                Shoot();
                break;
            case 1:
                ShootAssult();
                break;
            case 2:
                ShootShotgun();
                break;
            case 3:
                ShootSniper();
                break;
        }
        
    }

    void FixedUpdate()
    {
        // movement  / fisics
        // movimenta o personagem
        
        movement_all();
        
        // essas parte é para definir para onde o personagem está atirando
        Vector2 auxVector = (firePoint.position);
        Vector2 lookDir = mousePos - auxVector;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        // caso o personagem esteja andando para cima, a sprite da arma é carregada atrás do player
        if (movement.y > 0.0f && movement.sqrMagnitude > 0.0f)
        {
            weapon_sprite_render.sortingLayerName  = "behind_player";
        }else{
            weapon_sprite_render.sortingLayerName  = "front_player";
        }

        
        // a mira segue a posição do mouse
        crosshair.transform.position = mousePos;  
        rotateGun();
   

    }
    void movement_all(){

        if(isOnDash){
            rb.velocity = (movement * dashSpeed * Time.fixedDeltaTime);
        }else{
            rb.velocity = (movement * moveSpeed * Time.fixedDeltaTime);
        }

    }

    void movement_dash(){
        // verificando se foi apertado o shift e se o tempo é permitido
        if( Input.GetKeyDown(KeyCode.LeftShift) && !isOnDash && (Time.time - startDashTime) >= dashTime) {
            startDashTime = Time.time;
            ParticleSystem dashParticleClone = (ParticleSystem)Instantiate(dashParticle, transform.position, Quaternion.identity);
            float startTime = GetComponent<ParticleSystem>().main.startLifetime.constantMax;
            float duration = GetComponent<ParticleSystem>().main.duration;
            float totalDuration = startTime + duration;
            Destroy(dashParticleClone.gameObject, totalDuration);
            isOnDash = true;            
        }
        if(isOnDash && (Time.time - startDashTime) > dashDuration){
            isOnDash = false;
        }

    }

    // função de tiro do player
    void Shoot(){
        
        if(endOfAiming){
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            // adiciona uma força que define a movimentaçao da bala
            rbb.AddForce(firePoint.up * BULLET_BASE_SPEED, ForceMode2D.Impulse);
            // depois de 2 segundos o projétil é destruido
            Destroy(bullet, 2.0f);
        }
    }

    void ShootShotgun(){
        int numberOfbullets = 5;
        float[] anglesBet = new float[5];


        Vector2 auxVector = (firePoint.position);
        Vector2 lookDir = mousePos - auxVector;    

        if(endOfAiming){
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
                rbb[i].AddForce(directorShot.normalized * BULLET_BASE_SPEED, ForceMode2D.Impulse);
                // depois de 2 segundos o projétil é destruido
                Destroy(bullet[i], 1.2f);
            }
        }
    }


    void ShootSniper(){
        // A BALA DA SNIPER AUMENTA o quão mais longe vc mirar


        Vector2 auxVector = (firePoint.position);
        Vector2 lookDir = mousePos - auxVector;    
        if(endOfAiming){
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            // adiciona uma força que define a movimentaçao da bala
            rbb.AddForce((lookDir/5) * (BULLET_BASE_SPEED + 2), ForceMode2D.Impulse);
            // depois de 2 segundos o projétil é destruido
            Destroy(bullet, 3.0f);
        }
    }

    void ShootAssult(){
        Vector2 auxVector = (firePoint.position);
        Vector2 lookDir = mousePos - auxVector;    
        if(endOfAiming){
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            // adiciona uma força que define a movimentaçao da bala
            rbb.AddForce(firePoint.up * BULLET_BASE_SPEED, ForceMode2D.Impulse);
            // depois de 2 segundos o projétil é destruido
            Destroy(bullet, 2.0f);
        }
    }
   
    // inverte a arma caso ela seja apontada para a esquerda
    void rotateGun(){

        // se a mira estiver no lado esquerdo.
        if (((95.0f < angle && angle < 180.0f) || (-180.0f <= angle && angle <= -95.0f))  )
        {
            is_invert = true;
            player.transform.localScale = new Vector3(-player_scale_ini.x, player_scale_ini.y, player_scale_ini.z);
        }
        
        if ( ((0.0f < angle && angle < 85.0f) || (-85.0f <= angle && angle <= 0.0f)) )
        {
            
            is_invert = false;
            player.transform.localScale = new Vector3(player_scale_ini.x, player_scale_ini.y, player_scale_ini.z);
        }
    }


}

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
    public SpriteRenderer  weapon_sprite;   // define o sprite da arma
    public float off_set_x; //the of set from the char 
    public float off_set_y; //the of set from the char 
    private float angle;    // define o ângulo da arma
    private bool is_invert = false;    // verifica se a mira está invertida ( do lado esquerdo)
    


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
    public float dashSpeed; // valocidade do dash
    public float dashTime;  // tempo entre dash
    public float startDashTime; // tempo inicial do dash


    void Start() {
        // guarda o scaling inicial do jogador
        player_scale_ini = new Vector3(player.transform.localScale.x, 
                                   player.transform.localScale.y,
                                   player.transform.localScale.z);


    }



    // Update is called once per frame
    void Update()
    {
        // input update
        // guarda o input do movimento, move personagem        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        
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
        
        
        Shoot();
        
    }

    void FixedUpdate()
    {
        // movement  / fisics
        // movimenta o personagem
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        // essas parte é para definir para onde o personagem está atirando
        Vector2 auxVector = (firePoint.position);
        Vector2 lookDir = mousePos - auxVector;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        // caso o personagem esteja andando para cima, a sprite da arma é carregada atrás do player
        if (movement.y > 0.0f && movement.sqrMagnitude > 0.0f)
        {
            weapon_sprite.sortingLayerName  = "behind_player";
        }else{
            weapon_sprite.sortingLayerName  = "front_player";
        }

        
        // a mira segue a posição do mouse
        crosshair.transform.position = mousePos;  
        rotateGun();
   

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

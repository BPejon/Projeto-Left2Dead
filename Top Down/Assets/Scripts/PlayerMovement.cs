using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [Space]
    [Header("player")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb; 
    public Animator animator1;
    public GameObject player;
    public Camera cam;
    private Vector3 player_scale_ini;  


    [Space]
    [Header ("crosshair : ")]
    public float CROSSHAIR_DISTANCE = 2.0f;
    public GameObject crosshair;
    public bool endOfAiming;

    [Space]
    [Header ("gun : ")]
    // public Rigidbody2D rbw;
    public GameObject weapon;
    public SpriteRenderer  weapon_sprite;

    public float off_set_x; //the of set from the char 
    public float off_set_y; //the of set from the char 
    private float angle;
    private bool is_invert = false;
    


    [Space]
    [Header("vector2 : ")]
    Vector2 movement;
    Vector2 mousePos;

    [Space]
    [Header("Prefabs :")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float BULLET_BASE_SPEED = 19.0f;


    [Space]
    [Header("Dash : ")]
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;


    void Start() {
        player_scale_ini = new Vector3(player.transform.localScale.x, 
                                   player.transform.localScale.y,
                                   player.transform.localScale.z);


    }



    // Update is called once per frame
    void Update()
    {
        // input update
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        

        


        endOfAiming = Input.GetButtonUp("Fire1");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    

        animator1.SetFloat("Horizontal", movement.x);
        animator1.SetFloat("Vertical", movement.y);
        animator1.SetFloat("Speed", movement.sqrMagnitude);

        

        weapon.transform.position = new Vector2(rb.position.x + off_set_x, rb.position.y + off_set_y); 
        cam.transform.position = new Vector3(rb.position.x, rb.position.y, -10);

        if(is_invert){
  
            // rbw.rotation = angle  + 180;
            weapon.transform.eulerAngles = new Vector3(
                weapon.transform.eulerAngles.x,
                weapon.transform.eulerAngles.y,
                angle + 180
            );
            weapon.transform.position = new Vector2(rb.position.x - off_set_x, rb.position.y + off_set_y); 

        }else{
            // rbw.rotation = angle;
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
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);




        Vector2 auxVector = (firePoint.position);
        Vector2 lookDir = mousePos - auxVector;

        

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        



        if (movement.y > 0.0f && movement.sqrMagnitude > 0.0f)
        {
            weapon_sprite.sortingLayerName  = "behind_player";
        }else{
            weapon_sprite.sortingLayerName  = "front_player";
        }

        

        crosshair.transform.position = mousePos;  
        rotateGun();
   

    }


   
    void Shoot(){
        
        if(endOfAiming){
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            rbb.AddForce(firePoint.up * BULLET_BASE_SPEED, ForceMode2D.Impulse);
            Destroy(bullet, 2.0f);
        }

    }

    void rotateGun(){


        if (((90.0f < angle && angle < 180.0f) || (-180.0f <= angle && angle <= -90.0f))  )
        {
       
            is_invert = true;
            player.transform.localScale = new Vector3(-player_scale_ini.x, player_scale_ini.y, player_scale_ini.z);
            
        }
        else{
            
            is_invert = false;
            player.transform.localScale = new Vector3(player_scale_ini.x, player_scale_ini.y, player_scale_ini.z);
        }
    }


}

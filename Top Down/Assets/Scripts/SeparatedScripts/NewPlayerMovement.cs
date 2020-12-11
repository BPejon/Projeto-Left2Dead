using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    //Nesse Script Temos apenas as coisas relacionadas a movimentação do Jogador e Suas animações
    //Coisas como dash poderão ser implementadas aqui


    [Header("Config")]
    public float moveSpeed = 5.0f;
    Rigidbody2D rb; //Rigidbody2D do Personagem (ou o gameObject onde usamos este script)

    Animator animator;

    Vector2 movement; //Guardamos os inputs de movimento;

    [Space]
    [Header("Dash : ")]
    public ParticleSystem dashParticle;
    public ParticleSystem dashExplosionParticle;

    public int DASHF;
    public float dashSpeed; // valocidade do dash
    public float dashTime;  // tempo entre dash
    public float dashDuration; // tempo que um dash dura
    public float startDashTime; // tempo inicial do dash
    public bool isOnDash = false;
    public float durationParticle;
    int counter_aux;

    [Space]
    [Header("PlayerGotHit")]
    PlayerGotHit playerHitScript;

    // Start is called before the first frame update
    void Start()
    {
        //Setamos todos para os componentes deste Rigidbody
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        playerHitScript = gameObject.GetComponent<PlayerGotHit>();

        //selecionamos a posicao inicial;
        int default_ = PlayerPrefs.GetInt("use_position");

        if(default_ == 1){
            Debug.Log("Using Position");
            float x_position = PlayerPrefs.GetFloat("x_position");
            float y_position = PlayerPrefs.GetFloat("y_position");

            Vector3 newpos = new Vector3(x_position,y_position,-9);
            this.gameObject.transform.position = newpos;

            PlayerPrefs.SetInt("use_position", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Pegamos o Input:
        //1 - Movimento na direção X para direita, -1 - Para esquerda, 0 - Nenhum movimento
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement_dash();



        //Colocamos tais valores na máquina de estados do nosso animator
        animator.SetBool("isOnDash",isOnDash);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate(){
        if (playerHitScript.isPlayerDead){
            rb.velocity = (movement * 0);
            return;
        }

        //move o personagem conforme o movimento.
        if(!playerHitScript.isBack){
            if(isOnDash){
                rb.velocity = (movement * dashSpeed * Time.fixedDeltaTime);
            }else{
                rb.velocity = (movement * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void movement_dash(){
        // verificando se foi apertado o shift e se o tempo é permitido
        if( Input.GetKeyDown(KeyCode.LeftShift) && !isOnDash && (Time.time - startDashTime) >= dashTime && movement.sqrMagnitude > 0) {
            startDashTime = Time.time;
            ParticleSystem dashParticleClone = (ParticleSystem)Instantiate(dashExplosionParticle, 
                                                                           new Vector3(transform.position.x,
                                                                                       transform.position.y,
                                                                                        - 2),
                                                                            Quaternion.identity);
            SoundManager.PlaySound(SoundManager.Sound.dash);
            //float startTime = GetComponent<ParticleSystem>().main.startLifetime.constantMax;
            //float duration = GetComponent<ParticleSystem>().main.duration;
            //float totalDuration = startTime + duration;
            Destroy(dashParticleClone.gameObject, 2f /*totalDuration*/);
            
            isOnDash = true;
            counter_aux = 0;            
        }
        if (isOnDash)
        {
            counter_aux ++;
            if (counter_aux % DASHF == 0)
            {
                ParticleSystem dashParticleClone = (ParticleSystem)Instantiate(dashParticle, 
                                                                           new Vector3(transform.position.x,
                                                                                       transform.position.y,
                                                                                        - 2),
                                                                            Quaternion.identity);
                Destroy(dashParticleClone.gameObject, durationParticle);
            }
        }
        if(isOnDash && (Time.time - startDashTime) > dashDuration){
            ParticleSystem dashParticleClone = (ParticleSystem)Instantiate(dashParticle, 
                                                                           new Vector3(transform.position.x,
                                                                                       transform.position.y,
                                                                                        - 2),
                                                                            Quaternion.identity);
            Destroy(dashParticleClone.gameObject, durationParticle);
            isOnDash = false;
        }

    }
    
    

}

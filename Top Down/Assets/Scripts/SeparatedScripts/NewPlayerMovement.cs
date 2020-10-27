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
    public float dashSpeed; // valocidade do dash
    public float dashTime;  // tempo entre dash
    public float dashDuration; // tempo que um dash dura
    public float startDashTime; // tempo inicial do dash
    public bool isOnDash = false;

    // Start is called before the first frame update
    void Start()
    {
        //Setamos todos para os componentes deste Rigidbody
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
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
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate(){
        //move o personagem conforme o movimento.
        if(isOnDash){
            rb.velocity = (movement * dashSpeed * Time.fixedDeltaTime);
        }else{
            rb.velocity = (movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void movement_dash(){
        // verificando se foi apertado o shift e se o tempo é permitido
        if( Input.GetKeyDown(KeyCode.LeftShift) && !isOnDash && (Time.time - startDashTime) >= dashTime && movement.sqrMagnitude > 0) {
            startDashTime = Time.time;
            ParticleSystem dashParticleClone = (ParticleSystem)Instantiate(dashParticle, 
                                                                           new Vector3(transform.position.x,
                                                                                       transform.position.y,
                                                                                        - 2),
                                                                            Quaternion.identity);
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
    

}

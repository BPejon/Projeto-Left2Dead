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

        //Colocamos tais valores na máquina de estados do nosso animator
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate(){
        //move o personagem conforme o movimento.
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    

}

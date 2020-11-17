﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    // Start is called before the first frame update
    

    GameObject self;
    public GameObject bullet;

    public float bulletspeed;

    GameObject tip;

    //Ao criar uma pistola, setamos então sua munição, e armas;
    void Awake(){


        self = this.gameObject;

        //self.GetComponent<SpriteRenderer>().sprite = GetSprite("Pistol");  
        //Tip = Ponta da arma - Usada para determinar a posição de onde sai o tiro.
        tip = self.transform.Find("Tip").gameObject;  
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override int Shoot(Vector3 aimvec){
       //Primeiro checamos a questão da munição
       if(curammo == 0){
           return 0;
       }
       //se temos munição, atiramos; Tiro de pistóla é basico
       else{
           curammo--;
           //Vector3.Normalize(aimvec);

           GameObject nbullet = Instantiate(bullet,tip.transform.position,transform.rotation);
           Rigidbody2D rbb = nbullet.GetComponent<Rigidbody2D>();
           rbb.AddForce(tip.transform.up * bulletspeed, ForceMode2D.Impulse);
           
           //rbb.AddForce(aimvec * bulletspeed, ForceMode2D.Impulse);
           Destroy(nbullet, 2.0f);

           
            //Quando Atiramos, temos também que sofrer um "recoil"
        //    GameObject rb = this.transform.parent.gameObject;
           //Precisamos pegar o Gameobject do pai do pai, isto é, do player;
        //    GameObject rb2 = rb.transform.parent.gameObject;
           
          
           //Fazemos o Knockback por meio da posição da bala, em comparação com a posição do jogador.
            // Vector2 diference = rb2.transform.position - nbullet.transform.position;
            // float smoothener = 0.2f;
            // rb2.transform.position = new Vector2(rb2.transform.position.x + (diference.x* smoothener), rb2.transform.position.y + (diference.y*smoothener));

       }
       
       Debug.Log(curammo);
       Debug.Log(ammo);

       return 1;
    }

    //Se foi possivel recarregar = 1
    //Se não = 0
    public override int Reload(){
        //se não tivermos municão guardada
        if(ammo == 0){
            return 0;
        }
        //se temos, mas é menor q o tamanho do clipe;
        else if(ammo < clipsize){
            curammo = ammo;
            ammo = 0;
            return 1;
        }
        //Se a arma já esta recarregada, e não precisamos fazer isso.
        else if(curammo == clipsize){
            return 1;
        }
        //se não, recarregamos normalmente
        else{
            curammo = clipsize;
            ammo = ammo - clipsize;
            return 1;
        }

    }

    //Sera usada com pacotes de munição.
    //Podemos estabelecer um limite máximo de munição.
    public override void Refill(int Ammount)
    {
        ammo = ammo + Ammount;
    }
}
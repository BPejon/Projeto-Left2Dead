using System.Collections;
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
        clipsize = 6;
        ammo = 0;
        curammo = 6;

        self = this.gameObject;

        //self.GetComponent<SpriteRenderer>().sprite = GetSprite("Pistol");  
        //Tip = Ponta da arma - Usada para determinar a posição de onde sai o tiro.
        tip = self.transform.Find("Tip").gameObject;  
    }
    
    void Start()
    {
        clipsize = 6;
        ammo = 0;
        curammo = 6;
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
           GameObject nbullet = Instantiate(bullet,tip.transform.position,transform.rotation);
           Rigidbody2D rbb = nbullet.GetComponent<Rigidbody2D>();
           rbb.AddForce(aimvec * bulletspeed, ForceMode2D.Impulse);
           Destroy(nbullet, 2.0f);
       }
       
       return 1;
    }

    //Se foi possivel recarregar = 1
    //Se não = 0
    public override int Reload(){
        //se não tivermos municão guardada
        if(ammo <= 0){
            return 0;
        }
        //se temos, mas é menor q o tamanho do clipe;
        else if(ammo < clipsize){
            curammo = ammo;
            ammo = 0;
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

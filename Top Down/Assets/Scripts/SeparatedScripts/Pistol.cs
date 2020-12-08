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
    private SpriteRenderer tipSpriteR;

    [Space]
    [Header("shootEffect")]
    public float ShootTime;
    public float appRate;
    private float ShootStartTime;
    private bool isShoingEffect;
    private float curTipTransp;

    //Ao criar uma pistola, setamos então sua munição, e armas;
    void Awake(){
        curTipTransp = 0f;
        isShoingEffect = false;
        self = this.gameObject;
        //self.GetComponent<SpriteRenderer>().sprite = GetSprite("Pistol");  
        //Tip = Ponta da arma - Usada para determinar a posição de onde sai o tiro.
        tip = self.transform.Find("Tip").gameObject;  
        tipSpriteR =  tip.GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        kb = new KBReport();
        this.kb.kbdur = kbdur;
        this.kb.kbspeed = kbspeed;
        timer = 60.0f/tpm;
        // Debug.Log("Timer = " + timer + "Pistol - KB speed:" + this.kbspeed);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        
        if (isShoingEffect)
        {
            if (Time.time - ShootStartTime < ShootTime/2 && curTipTransp < 1.0f)
            {
                curTipTransp += appRate;
                tipSpriteR.color = new Color(1f,1f,1f,curTipTransp);
            }else if(Time.time - ShootStartTime > ShootTime/2 && curTipTransp > 0.0f){
                curTipTransp -= appRate;
                tipSpriteR.color = new Color(1f,1f,1f,curTipTransp);
            }
            if (curTipTransp < 0.0f)
                isShoingEffect = false;
            
        }

    }

    public override KBReport Shoot(Vector3 aimvec){
       //elapsed = tempo desde o ultimo tiro
       //Se esse valor for menor que timer, nao atiramos
       elapsed = Time.time - past;
    
       elapsedreload = Time.time - pastreload;

       //Primeiro checamos a questão da munição
       if(curammo == 0){
          this.kb.status = 0;
           return kb;
       }
       //se temos munição, atiramos; Tiro de pistóla é basico
       else if(elapsed < timer){
           this.kb.status = 2;
           return kb;
       }
       else if(elapsedreload < reloadtime){
           this.kb.status = 2;
           return kb;
       }
       else{

            curammo--;
            isShoingEffect = true;
            ShootStartTime = Time.time;
            //Vector3.Normalize(aimvec);

            //Som do tiro
            SoundManager.PlaySound(SoundManager.Sound.pistolShot);


           GameObject nbullet = Instantiate(bullet,tip.transform.position,transform.rotation);
           Rigidbody2D rbb = nbullet.GetComponent<Rigidbody2D>();
           
           
           
           rbb.AddForce(tip.transform.up * bulletspeed, ForceMode2D.Impulse);
           //nbullet.GetComponent<StandardBullet>().setFactors(kbspeed, kbdur);
           nbullet.GetComponent<StandardBullet>().setAngle(tip.transform.up);
           //rbb.AddForce(aimvec * bulletspeed, ForceMode2D.Impulse);
           Destroy(nbullet, 2.0f);




            


            
        }

        //Debug.Log(curammo);
        //Debug.Log(ammo);
        past = Time.time;
        this.kb.status = 1;
        return kb;
    }

    //Se foi possivel recarregar = 1
    //Se não = 0
    //Se na espera = 2
    public override int Reload(){
        //se não tivermos municão guardada
        if (ammo <= 0)
        {
            return 0;
        }
        //se temos, mas é menor q o tamanho do clipe;
        else if(ammo < clipsize){
            curammo = ammo;
            ammo = 0;


            //Estamos recarregando;
            //reloading = true;
            //Marcamos o tempo que comecamos a recarregar;
            pastreload = Time.time;
            SoundManager.PlaySound(SoundManager.Sound.pistolReload);
            return 1;
        }
        //Se a arma já esta recarregada, e não precisamos fazer isso.
        else if(curammo == clipsize){
            return 1;
        }
        //se não, recarregamos normalmente
        else{
            SoundManager.PlaySound(SoundManager.Sound.pistolReload);
            curammo = clipsize;
            ammo = ammo - clipsize;
            //Estamos recarregando;
            reloading = true;
            //Marcamos o tempo que comecamos a recarregar;
            pastreload = Time.time;


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

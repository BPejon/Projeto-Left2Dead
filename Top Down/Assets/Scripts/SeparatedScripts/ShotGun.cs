﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
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
        this.kb.kbspeed = kbspeed;
        this.kb.kbdur = kbdur;
        timer = 60.0f/tpm;
       // Debug.Log("Timer = " + timer);
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
        
        int numberOfbullets = 5;
        float[] anglesBet = new float[numberOfbullets];
        int max_angle = 30;

        elapsedreload = Time.time - pastreload;

       //Primeiro checamos a questão da munição
       if(curammo == 0){
          this.kb.status = 0;
           return kb;
       }
       else if(elapsed < timer){
          this.kb.status = 2;
           return kb;
       }
       else if(elapsedreload < reloadtime){
           this.kb.status = 2;
           return kb;
       }
       //se temos munição, atiramos; Tiro de pistóla é basico
       else{
            curammo--;
            isShoingEffect = true;
            ShootStartTime = Time.time;
            //Vector3.Normalize(aimvec);

            //Som da 12
            SoundManager.PlaySound(SoundManager.Sound.shotgunShot);

            GameObject[] nbullet = new GameObject[numberOfbullets];
            Rigidbody2D[] rbb = new Rigidbody2D[numberOfbullets];
            for (int i = 0; i < numberOfbullets; i++)
            {
                nbullet[i] = Instantiate(bullet,
                                        new Vector3(
                                            tip.transform.position.x + ((float)i/10),
                                            tip.transform.position.y + ((float)i/10),
                                            tip.transform.position.z
                                        ),
                                        transform.rotation ); 
                rbb[i] = nbullet[i].GetComponent<Rigidbody2D>();
                anglesBet[i] = Random.Range(-max_angle,max_angle);
                // adiciona uma força que define a movimentaçao da bala
                Vector2 directorShot = Quaternion.Euler(tip.transform.rotation.x,
                                                        tip.transform.rotation.y,
                                                        anglesBet[i] ) * tip.transform.up;
                rbb[i].AddForce(directorShot.normalized * bulletspeed, ForceMode2D.Impulse);
                // depois de 2 segundos o projétil é destruido
                //nbullet[i].GetComponent<StandardBullet>().setFactors(kbdur,kbspeed);
                nbullet[i].GetComponent<StandardBullet>().setAngle(directorShot.normalized);
                Destroy(nbullet[i], 1.2f);
            }
       }
       
   
       past = Time.time;
       this.kb.status = 1;
       return kb;
    }

    //Se foi possivel recarregar = 1
    //Se não = 0
    public override int Reload(){
        //se não tivermos municão guardada
        if (ammo <= 0)
        {
            return 0;
        }
        else if(curammo == clipsize){
            return 1;
        }
        //se temos, mas é menor q o tamanho do clipe;
        else if(ammo < clipsize){
            curammo = ammo;
            ammo = 0;
            //Marcamos o tempo que comecamos a recarregar;
            SoundManager.PlaySound(SoundManager.Sound.shotgunReload);
            pastreload = Time.time;
            return 1;
        }
        //Se a arma já esta recarregada, e não precisamos fazer isso.
        else if (curammo == clipsize)
        {
            return 1;
        }
        //se não, recarregamos normalmente
        else{
            curammo = clipsize;
            ammo = ammo - clipsize;
            


            //Marcamos o tempo que comecamos a recarregar;
            pastreload = Time.time;
            SoundManager.PlaySound(SoundManager.Sound.shotgunReload);


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

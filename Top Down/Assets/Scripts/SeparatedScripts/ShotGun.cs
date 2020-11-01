using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
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
        kb = new KBReport();
        this.kb.kbspeed = kbspeed;
        this.kb.kbdur = kbdur;
        timer = 60.0f/tpm;
        Debug.Log("Timer = " + timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override KBReport Shoot(Vector3 aimvec){
        //elapsed = tempo desde o ultimo tiro
        //Se esse valor for menor que timer, nao atiramos
        elapsed = Time.time - past;
        
        int numberOfbullets = 5;
        float[] anglesBet = new float[numberOfbullets];
        int max_angle = 70;

       //Primeiro checamos a questão da munição
       if(curammo == 0){
          this.kb.status = 0;
           return kb;
       }
       else if(elapsed < timer){
          this.kb.status = 2;
           return kb;
       }
       //se temos munição, atiramos; Tiro de pistóla é basico
       else{
            curammo--;
            //Vector3.Normalize(aimvec);

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
                Vector2 directorShot = Quaternion.AngleAxis(anglesBet[i],Vector2.up) * tip.transform.up;
                rbb[i].AddForce(directorShot.normalized * bulletspeed, ForceMode2D.Impulse);
                // depois de 2 segundos o projétil é destruido
                Destroy(nbullet[i], 1.2f);
            }
            for (int i = 0; i < numberOfbullets; i++)
            {
                Debug.Log(anglesBet[i]);
            }
       }
       
    //    Debug.Log(curammo);
    //    Debug.Log(ammo);
       past = Time.time;
       this.kb.status = 1;
       return kb;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class GeneralWeaponScript : MonoBehaviour
{
    //Neste Script fazemos todos os controles que qualquer arma terá
    //Tais controles incluem: Mira, Relações de Munição, armas equipadas.
    //Este Script estará ligado a um objeto chamdo "holster", filho do player
    //Tal objeto existe para facilitar a troca de armas e coisas assim.

    //Atualizacao do GunBelt - Agora com "Array Ilimitada" de armas
    //Usamos uma array de 2 ints - "belt" que guarda o index da arma.
    //Como só temos 2 armas, por enquanto, nao temos grande diferenca.
    //Permite criar a arma "vazia" - Depois precisamos especificar sua posicao
    [Header("Gun Types & Gun Belt")]
    public GameObject[] gunTypes;
    public int[] Belt = new int[2];

    public UIManager UI;


    //Qual arma estamos empunhando;
    //Agora usado como indice de "Belt"
    //0 - gunBelt[0]
    //1 - gunBelt[1]
    public int held = 0;
    public int _other = 1;

    //Sprite da Arma, tiramos do gameobject da arma
    SpriteRenderer wpimg;

    //basicamente this.gameObject
    GameObject holster;
    [Space]
    [Header("Camera & Aim")]
    //Vetor de posição do mouse, usado na mira.
    public Vector3 camvec;

    public Transform player;
    private Vector3 player_scale_ini;


    //Knoockback testing Related.
    [Space]
    [Header("KnockBack & Testing")]
    public float kbspeed;
    public float kbtime;

    public float kbdur;

    public bool knockd;
    
    public bool kill;

    public KBReport fevent;

    //public bool fired;
    public Vector2 pastaux;

   
    void Start()
    {
        //Ao iniciar pegamos o sprite da arma e o holster.
        holster = this.gameObject;
        fevent = new KBReport();
        //Nova versao
        wpimg = gunTypes[Belt[held]].GetComponent<SpriteRenderer>();
        
        CleanAllSprites();

        //Pegar a imagem da arma empunhada (no caso a 0 no inicio);
        //wpimg = gunBelt[held].GetComponent<SpriteRenderer>();
        player = GameObject.Find("player").transform;
        player_scale_ini = new Vector3(player.localScale.x, 
                                   player.localScale.y,
                                   player.localScale.z);
        
        //Estamos na primeira cena ou nao - carregar estado do player;
        int default_ = PlayerPrefs.GetInt("use_default");
        if(default_ != 1){
            //Debug.Log("Here");
            held = PlayerPrefs.GetInt("held");
            _other = PlayerPrefs.GetInt("_other");

            Belt[0] = PlayerPrefs.GetInt("weaponindex_1");
            Belt[1] = PlayerPrefs.GetInt("weaponindex_2");
            

            gunTypes[Belt[0]].GetComponent<Gun>().ammo = PlayerPrefs.GetInt("ammo_weapon_1");
            gunTypes[Belt[0]].GetComponent<Gun>().curammo = PlayerPrefs.GetInt("curammo_weapon_1");
            
            
            gunTypes[Belt[1]].GetComponent<Gun>().ammo = PlayerPrefs.GetInt("ammo_weapon_2");
            gunTypes[Belt[1]].GetComponent<Gun>().curammo = PlayerPrefs.GetInt("curammo_weapon_2");
        }

        UpdateWeapon();

        
        //kb test
        knockd = false;
        kill = false;
        //Mun~ição na arma
        UI.SetBulletGun(gunTypes[Belt[held]].GetComponent<Gun>().curammo, held);
        UI.SetBulletColdre(gunTypes[Belt[held]].GetComponent<Gun>().ammo, held);


        UI.SetBulletGun(gunTypes[Belt[_other]].GetComponent<Gun>().curammo, _other);
        UI.SetBulletColdre(gunTypes[Belt[_other]].GetComponent<Gun>().ammo, _other);

        UI.setGunImages(Belt[0], Belt[1]);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateWeapon();

        DealKnockback(0);

        wpimg = gunTypes[Belt[held]].GetComponent<SpriteRenderer>();
        //wpimg = gunBelt[held].GetComponent<SpriteRenderer>();
        //definimos o angulo que vamos usar para a mira.
        AimAngle();
        //invertendo o personagem
        //Como temos tiros por segundo, devemos usar o botão pressionado.
        if(Input.GetButton("Fire1")){
            
            fevent = gunTypes[Belt[held]].GetComponent<Gun>().Shoot(camvec);
            //int fevent = gunBelt[held].GetComponent<Gun>().Shoot(camvec);
            

            if(fevent.status == 1){
                //Debug.Log("Shot Fired");
                //Debug.Log("Segurando" +  gunTypes[Belt[held]].GetComponent<Gun>());
                //Debug.Log("Gun Type" + Belt[held]);
                ShakeHandler.PlayShake((ShakeHandler.Shake)Belt[held]);
                //test for knockback:
                DealKnockback(1);
            }
            if(fevent.status == 2){
               // Debug.Log("Gun -resting-");
            }
            if(fevent.status == 0){
              //  Debug.Log("Must Reload");
            }

            UI.SetBulletGun(gunTypes[Belt[held]].GetComponent<Gun>().curammo, held);         

        }

        //Se estamos recarregando
        if(Input.GetKeyDown(KeyCode.R)){
           // Debug.Log("R pressed" + held);
            int fevent2 = gunTypes[Belt[held]].GetComponent<Gun>().Reload();
            //int fevent = gunBelt[held].GetComponent<Gun>().Reload();
            if(fevent2 == 0){
               // Debug.Log("No ammo");
            }
            else{
               // Debug.Log("Reloaded");
            }

            UI.SetBulletGun(gunTypes[Belt[held]].GetComponent<Gun>().curammo, held);
            UI.SetBulletColdre(gunTypes[Belt[held]].GetComponent<Gun>().ammo, held);
        }

        //Setamos qual eh a arma que estamos usando.
        if(Input.mouseScrollDelta.y != 0){
            
            //scroll pra cima
            //Como temos duas armas, ambas fazem a mesma coisa, mas esse codigo serve se tivermos mais armas no futuro
            if(held == 0){
                held = 1;
                _other = 0;
            }
            //scroll pra baixo;
            else{
                held = 0;
                _other = 1;
            }

            //Debug.Log("Scroll Moved");
            //Debug.Log(held);
        }

    }

    

    void FixedUpdate(){
        if(knockd){ 
            Vector2 aux;
            aux.x = -camvec.x;
            aux.y = -camvec.y;
            player.GetComponent<Rigidbody2D>().velocity += fevent.kbspeed * aux;
            pastaux = aux;
        }
        else if(kill){
            Vector2 aux;
            aux.x = 0;
            aux.y = 0;
            //player.GetComponent<Rigidbody2D>().velocity -= kbspeed * pastaux;
            player.GetComponent<Rigidbody2D>().velocity = aux;
            kill = false;
        }
    }

    public void DealKnockback(int f){
        //start knockback
        if(f == 1){
            knockd = true;
            kbtime = Time.time;
        }
        else if((Time.time - kbtime) >= fevent.kbdur){
            if(knockd){
                kill = true;
            }
            knockd = false;
        }
    }

    public void AimAngle(){
        //pegamos o angulo da arma.
        camvec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        camvec.z = 0;
        Vector3 auxcamvec = (camvec - transform.position).normalized;
        camvec = auxcamvec;
        //Angles from -90 to 90 are on normal position
        //Other angles are inverted
        float angle = Mathf.Atan2(auxcamvec.y,auxcamvec.x) * Mathf.Rad2Deg;
        bool invert = false;
        if(!(angle <= 90 && angle >= -90)){
            invert = true;
        }


        if(invert){
            player.localScale = new Vector3(-player_scale_ini.x, player_scale_ini.y, player_scale_ini.z);
        }else{
            player.localScale = new Vector3(player_scale_ini.x, player_scale_ini.y, player_scale_ini.z);
        }

        //Basicamente vemos se damos o flipzinho ou não.
        
        if(invert){
            // wpimg.flipY = invert; 
            holster.transform.eulerAngles = new Vector3(holster.transform.eulerAngles.x,
                                                        holster.transform.eulerAngles.y,
                                                        angle + 180);            
        }else{
            holster.transform.eulerAngles = new Vector3(holster.transform.eulerAngles.x,
                                                        holster.transform.eulerAngles.y,
                                                        angle);
        }
    }

    //Pegar Itens e armas - 
    public void ChangeWeapon(GameObject other){
        //Debug.Log("Other!");
        //se eh um item droppado (inicalmente faremos para armas)
        if(other.gameObject.CompareTag("DroppedItem")){
            
                //eh uma arma, nos queremos ela.
                //2 Casos: 1 - Arma nova, 2 - Arma ja equipada
                //1 - Substituimos a arma empunhada pela nova
                //1 - Jogamos a antiga "fora" (eliminamos seus valores de municao e clipe)
                //2 - Aumentamos o valor de municao da arma especifica. 

                //Caso 1:
                DroppedGun weaponInfo = other.gameObject.GetComponent<DroppedGun>();
                //Se nao for nenhuma das armas que temos atualmente.
                if(weaponInfo.indexvalue != Belt[0] && weaponInfo.indexvalue != Belt[1] ){
                    Belt[held] = weaponInfo.indexvalue;
                    gunTypes[Belt[held]].GetComponent<Gun>().ammo = weaponInfo.curammo;
                    gunTypes[Belt[held]].GetComponent<Gun>().curammo = weaponInfo.curclip;
                    CleanAllSprites();
                    UpdateWeapon();
                }
                else{
                    gunTypes[weaponInfo.indexvalue].GetComponent<Gun>().Refill(weaponInfo.curammo);
                }


            
        }

        //Quando troca de arma
        UI.SetBulletGun(gunTypes[Belt[held]].GetComponent<Gun>().curammo, held);
        UI.SetBulletColdre(gunTypes[Belt[held]].GetComponent<Gun>().ammo, held);

        UI.setGunImages(Belt[0], Belt[1]);
        
    }

    //Atualiza quanto que arma estamos usando, mudando o sprite e tal;
    public void UpdateWeapon(){
        
        if(Belt[held] != Belt[_other])
        { 
            gunTypes[Belt[held]].GetComponent<SpriteRenderer>().enabled = true;
            //gunBelt[held].GetComponent<SpriteRenderer>().enabled = true;
            
            
            if(held == 0){
            _other = 1;
            }
            else{
                _other = 0;
            }
            gunTypes[Belt[_other]].GetComponent<SpriteRenderer>().enabled = false;
            //gunBelt[_other].GetComponent<SpriteRenderer>().enabled = false;
        }
        else{
            gunTypes[Belt[held]].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    //Desabilita todos os spirtes de arma, para que apenas as "selecionadas" possam ser habilitadas.
    void CleanAllSprites(){

        //Debug.Log(gunTypes.Length);
        for(int i = 0; i < gunTypes.Length; i++){
            gunTypes[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

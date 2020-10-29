using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralWeaponScript : MonoBehaviour
{
    //Neste Script fazemos todos os controles que qualquer arma terá
    //Tais controles incluem: Mira, Relações de Munição, armas equipadas.
    //Este Script estará ligado a um objeto chamdo "holster", filho do player
    //Tal objeto exsiste para facilitar a troca de armas e coisas assim.

    //Atualizacao do GunBelt - Agora com "Array Ilimitada" de armas
    //Usamos uma array de 2 ints - "belt" que guarda o index da arma.
    //Como só temos 2 armas, por enquanto, nao temos grande diferenca.
    //Permite criar a arma "vazia" - Depois precisamos especificar sua posicao.
    public GameObject[] gunTypes;
    public int[] Belt = new int[2];


    //Qual arma estamos empunhando;
    //Agora usado como indice de "Belt"
    //0 - gunBelt[0]
    //1 - gunBelt[1]
    private int held = 0;
  
    //Gameobject da arma
    //Na versão final provavelmente teremos uma lista
    //que começara vazia, e vamos instanciando objetos para dentro dela.
    //public GameObject weapon;

    //Sprite da Arma, tiramos do gameobject da arma
    SpriteRenderer wpimg;

    //basicamente this.gameObject
    GameObject holster;

    //Vetor de posição do mouse, usado na mira.
    public Vector3 camvec;

    public Transform player;
    private Vector3 player_scale_ini;

    void Start()
    {
        //Ao iniciar pegamos o sprite da arma e o holster.
        holster = this.gameObject;

        //Nova versao
        wpimg = gunTypes[Belt[held]].GetComponent<SpriteRenderer>();
        
        CleanAllSprites();

        //Pegar a imagem da arma empunhada (no caso a 0 no inicio);
        //wpimg = gunBelt[held].GetComponent<SpriteRenderer>();
        player = GameObject.Find("player").transform;
        player_scale_ini = new Vector3(player.localScale.x, 
                                   player.localScale.y,
                                   player.localScale.z);
        UpdateWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWeapon();
        
        wpimg = gunTypes[Belt[held]].GetComponent<SpriteRenderer>();
        //wpimg = gunBelt[held].GetComponent<SpriteRenderer>();
        //definimos o angulo que vamos usar para a mira.
        AimAngle();
        //invertendo o personagem
        //Como temos tiros por segundo, devemos usar o botão pressionado.
        if(Input.GetButton("Fire1")){
            
            int fevent = gunTypes[Belt[held]].GetComponent<Gun>().Shoot(camvec);
            //int fevent = gunBelt[held].GetComponent<Gun>().Shoot(camvec);

            if(fevent == 1){
                Debug.Log("Shot Fired");
            }
            if(fevent == 2){
               // Debug.Log("Gun -resting-");
            }
            else{
                Debug.Log("Must Reload");
            }


        }

        //Se estamos recarregando
        if(Input.GetKeyDown(KeyCode.R)){
            Debug.Log("R pressed" + held);
            int fevent = gunTypes[Belt[held]].GetComponent<Gun>().Reload();
            //int fevent = gunBelt[held].GetComponent<Gun>().Reload();
            if(fevent == 0){
                Debug.Log("No ammo");
            }
            else{
                Debug.Log("Reloaded");
            }
        }

        //Setamos qual eh a arma que estamos usando.
        if(Input.mouseScrollDelta.y != 0){
            
            //scroll pra cima
            //Como temos duas armas, ambas fazem a mesma coisa, mas esse codigo serve se tivermos mais armas no futuro
            if(held == 0){
                held = 1;
            }
            //scroll pra baixo;
            else{
                held = 0;
            }

            Debug.Log("Scroll Moved");
            Debug.Log(held);
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

    //Atualiza quanto que arma estamos usando, mudando o sprite e tal;
    public void UpdateWeapon(){
        
        gunTypes[Belt[held]].GetComponent<SpriteRenderer>().enabled = true;
        //gunBelt[held].GetComponent<SpriteRenderer>().enabled = true;
        
        int other;
        if(held == 0){
            other = 1;
        }
        else{
            other = 0;
        }
        gunTypes[Belt[other]].GetComponent<SpriteRenderer>().enabled = false;
        //gunBelt[other].GetComponent<SpriteRenderer>().enabled = false;
    }

    //Desabilita todos os spirtes de arma, para que apenas as "selecionadas" possam ser habilitadas.
    void CleanAllSprites(){

        Debug.Log(gunTypes.Length);
        for(int i = 0; i < gunTypes.Length; i++){
            gunTypes[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }


}

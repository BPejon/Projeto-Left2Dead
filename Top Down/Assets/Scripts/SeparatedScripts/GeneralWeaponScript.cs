using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralWeaponScript : MonoBehaviour
{
    //Neste Script fazemos todos os controles que qualquer arma terá
    //Tais controles incluem: Mira, Relações de Munição, armas equipadas.
    //Este Script estará ligado a um objeto chamdo "holster", filho do player
    //Tal objeto exsiste para facilitar a troca de armas e coisas assim.
    
    //Gameobject da arma
    //Na versão final provavelmente teremos uma lista
    //que começara vazia, e vamos instanciando objetos para dentro dela.
    public GameObject weapon;

    //Sprite da Arma, tiramos do gameobject da arma
    SpriteRenderer wpimg;

    //basicamente this.gameObject
    GameObject holster;

    //Vetor de posição do mouse, usado na mira.
    public Vector3 camvec;

    void Start()
    {
        //Ao iniciar pegamos o sprite da arma e o holster.
        holster = this.gameObject;
        wpimg = weapon.GetComponent<SpriteRenderer>();
            }

    // Update is called once per frame
    void Update()
    {
        //definimos o angulo que vamos usar para a mira.
        AimAngle();

        //Se estamos atirando, vamos atirar
        if(Input.GetButtonUp("Fire1")){
            //Atirar será chamado da arma em si
            //Desse modo, desse handler podemos atirar com todas as armas
            //Parte de lidar com munição ainda não criada.
            //Esta função é herdada da classe Gun - Mas como chamar "genericamente" ainda não ficou
            //Claro pra mim, estou vendo como será.
            weapon.GetComponent<Pistol>().Shoot(camvec);
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

        //Basicamente vemos se damos o flipzinho ou não.
        /*if(invert){
           wpimg.flipY = invert; 
           holster.transform.eulerAngles = new Vector3(holster.transform.eulerAngles.x,holster.transform.eulerAngles.y,angle); 
        }
        else{
            wpimg.flipY = invert; 
            holster.transform.eulerAngles = new Vector3(holster.transform.eulerAngles.x,holster.transform.eulerAngles.y,angle); 
        }*/
        wpimg.flipY = invert; 
        holster.transform.eulerAngles = new Vector3(holster.transform.eulerAngles.x,holster.transform.eulerAngles.y,angle);
    }


}

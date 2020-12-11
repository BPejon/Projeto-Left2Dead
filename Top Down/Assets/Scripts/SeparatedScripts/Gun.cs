using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
   //Parent Class de todas as armas
   //Todas as armas serão descendentes dessa clase
    
    //Quantas balas cabem em um pente;
    public int clipsize;
  
    //Quantas balas temos fora do pente
    public int ammo;

    //Quantas balas temos no pente
    public int curammo;

    //Quantos tiros por minuto
    public int tpm;

    //Medimos quanto tempo se passou
    public float elapsed = 0;

    //Tempo desde a ultima vez que atiramos
    public float past = 0;

    //timer define o "tempo minimo" para outro tiro ocorrer.
    public float timer;

    public KBReport kb;

    public float kbspeed;
    public float kbdur;

    //Referencias para o processo de reload
    public bool reloading;

    public float reloadtime;

    public float elapsedreload;

    public float pastreload;


    //somos nós!
    GameObject gun;

    /*//Contructor da Arma base
    //Em seus filhos setamos as qtdades necessárias.
    public Gun(string Gname){
        //Setamos, então, um sprite dado o nome da arma
        gun.GetComponent<SpriteRenderer>().sprite = GetSprite(Gname);
    }*/
    void Initialize(string Gname){
        gun = this.gameObject;
        gun.GetComponent<SpriteRenderer>().sprite = GetSprite(Gname);
    }

    //Awake
    void Awake(){

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Esta função sera sobreescrita para cada arma porém, seu conceito continua o mesmo:
    //Return 1 = atirou normalmente
    //Return 2 = Não pode atirar ainda (cooldown da arma - Fire rate, a implementar)
    //Return 0 = Munição acabou, precisa recarregar
    public abstract KBReport Shoot(Vector3 aimvec);

    //Precisamos, também, recarregar;
    public abstract int Reload();

    //Também temos que aumentar a munição fora do clipe;
    public abstract void Refill(int Ammount);

    //Esconder o seu sprite:
    public void Hide(){
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    //mostrar seu sprite:
    public void Show(){
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    //Pegamos o spirte para a arma, usado no contrutor;
    public Sprite GetSprite(string gname){
        switch(gname){
            default:
                case "Pistol": return WeaponAssets.Instance.pistol;
                case "Shotgun": return WeaponAssets.Instance.shotgun;
        }

    }
}

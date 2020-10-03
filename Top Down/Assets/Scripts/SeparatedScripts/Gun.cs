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
    public abstract int Shoot(Vector3 aimvec);

    //Precisamos, também, recarregar;
    public abstract int Reload();

    //Também temos que aumentar a munição fora do clipe;
    public abstract void Refill(int Ammount);

    //Pegamos o spirte para a arma, usado no contrutor;
    public Sprite GetSprite(string gname){
        switch(gname){
            default:
                case "Pistol": return WeaponAssets.Instance.pistol;
                case "Shotgun": return WeaponAssets.Instance.shotgun;
        }

    }
}

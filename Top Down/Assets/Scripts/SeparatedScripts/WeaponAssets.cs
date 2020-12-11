using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAssets : MonoBehaviour
{
    //Aqui guardamos todos os sprites de Armas, de modo a facilitar sua instanciação
    //Meio inutil isso aqui, por enquanto
    //Deixei aqui por deixar, basicamente.
    public static WeaponAssets Instance {get; private set;}

    private void Awake(){
        Instance = this;
    }

    public Sprite pistol;
    public Sprite shotgun;
}

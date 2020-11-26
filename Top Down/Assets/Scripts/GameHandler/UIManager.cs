using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{


    public Canvas canvas;

    //public PlayerGotHit player;
    public GeneralWeaponScript Belt;

    public Image GunImage1;
    public Image GunImage2;

    public Sprite spriteteste;

    public enum GunImage{
        Pistol,
        Shotgun,
        SniperRifle,
        AssaultRifle,        

        //Quando n tiver segurando nenhuma arma
    }



    //Começa com as armas que estão na mao
    private void Start(){

        //Pega a Referencia do cinto
        Belt = GameObject.Find("player").transform.GetChild(0).GetComponent<GeneralWeaponScript>();
        //Pega as armas dos cintos
        GunImage arma1 = (GunImage)Belt.Belt[0];
        GunImage arma2 = (GunImage)Belt.Belt[1];


        ShowGun1(GetSpriteGun(arma1));
        ShowGun2(GetSpriteGun(arma2));


    }

    //Mostra a imagem de arma na ui ao pegar a arma 1
    public void ShowGun1(Sprite gunImage){
        GunImage1.sprite = gunImage;
    }

    //Mostra a imagem de arma na UI ao pegar a arma 2
    public void ShowGun2(Sprite gunImage)
    {
        GunImage2.sprite = gunImage;
    }



    //Função que pega o Sprite que será utilizado
    private static Sprite GetSpriteGun(GunImage gunImage)
    {
        foreach (GameAssets.ImageGunsUI imageGunsUI in GameAssets.Instance.imageGunsUIArray)
        {
            if(imageGunsUI.gunImage == gunImage)
            {
                return imageGunsUI.gunSprite;
            }            
        }
        Debug.LogError("Sprite" + gunImage + "not found!");
        return null;
    }


}

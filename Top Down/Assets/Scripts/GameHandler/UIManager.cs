using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{


    public Canvas canvas;

    //public PlayerGotHit player;
    public GeneralWeaponScript Belt;

    public Image GunImage1;
    public Image GunImage2;

    public TextMeshProUGUI BulletGun1;
    public TextMeshProUGUI BulletGun2;
    public TextMeshProUGUI BulletColdre1;
    public TextMeshProUGUI BulletColdre2;


    public Sprite spriteteste;

    public enum GunImage
    {
        Pistol,
        Shotgun,
        AssaultRifle,
        SniperRifle,
        //Quando n tiver segurando nenhuma arma
        Mellee,
    }

    //Armazenar quais armas estao em quais locais
    GunImage arma1, arma2;

    //Começa com as armas que estão na mao
    private void Start(){

        //Pega a Referencia do cinto
        Belt = GameObject.Find("player").transform.GetChild(0).GetComponent<GeneralWeaponScript>();
        //Pega as armas dos cintos
        arma1 = (GunImage)Belt.Belt[0];
        arma2 = (GunImage)Belt.Belt[1];


        ShowGun1(GetSpriteGun(arma1));
        ShowGun2(GetSpriteGun(arma2));

    }

    public void setGunImages(int a1, int a2){
        arma1 = (GunImage) a1;
        arma2 = (GunImage) a2;
        
        //Debug.Log(a1+ " " + a2);

        ShowGun1(GetSpriteGun(arma1));
        ShowGun2(GetSpriteGun(arma2));

    }

    //Mostra a imagem de arma na ui ao pegar a arma 1
    public void ShowGun1(Sprite gunImage)
    {
        GunImage1.sprite = gunImage;
    }

    //Mostra a imagem de arma na UI ao pegar a arma 2
    public void ShowGun2(Sprite gunImage)
    {
        GunImage2.sprite = gunImage;
    }



    //Função que pega o Sprite que será utilizado
    public static Sprite GetSpriteGun(GunImage gunImage)
    {
        foreach (GameAssets.ImageGunsUI imageGunsUI in GameAssets.Instance.imageGunsUIArray)
        {
            if (imageGunsUI.gunImage == gunImage)
            {
                return imageGunsUI.gunSprite;
            }
        }
        Debug.LogError("Sprite" + gunImage + "not found!");
        return null;
    }




    //Setar a Bala da Arma 
    public void SetBulletGun(int bullets, int gun){
        if (gun == 0){
            BulletGun1.SetText(bullets.ToString());
        }else{
            BulletGun2.SetText(bullets.ToString());
        }
        
    }



    //Setar Bala no Coldre da Arma 1
    public void SetBulletColdre(int bullets, int gun){
        if (gun == 0){
            BulletColdre1.SetText(bullets.ToString());
        }else{
            BulletColdre2.SetText(bullets.ToString());
        }
    }
}
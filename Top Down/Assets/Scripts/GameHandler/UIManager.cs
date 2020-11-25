using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{



    public enum GunImage{
        Pistol,
        Shotgun,
        SniperRifle,
    }
    /*
    public static void Init()
    {
        ShowGun(GameAssets.Instance.Pistol);
    }
    public void ShowGun(GunImage gunImage){
        GameObject spriteGameObject = new GameObject("Gun Sprite");
        Image GunSprite =  spriteGameObject.AddComponent<Image>();
        GunSprite.sprite(gunImage);

        //Tem q deixar a imagem filho de canvas(UI)


    }
 */

}

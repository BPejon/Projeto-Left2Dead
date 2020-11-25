using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{


    //vetor sprite gun 1
    //vetor sprite gun 2
    //private Vector2[] vectorGun1 = { 817.21, -142 };

    //Sprite gun 1
    //Sprite gun 2

    public Sprite spriteteste;
    public enum GunImage{
        Pistol,
        Shotgun,
        SniperRifle,
        //Quando n tiver segurando nenhuma arma
    }

    public Canvas canvas;

    private void Start()
    {
        ShowGun(spriteteste);
    }
    public void ShowGun(Sprite gunImage){
        GameObject spriteGameObject = new GameObject("Gun Sprite");
        Image GunSprite =  spriteGameObject.AddComponent<Image>();
        GunSprite.sprite = gunImage;
        

        //Deixar a imagem filho de canvas(UI)
        spriteGameObject.transform.SetParent(canvas.transform, false);

        //spriteGameObject.GetComponent<RectTransform>().anchoredPosition = vectorGun1;
       // RectTransform rect = spriteGameObject.GetComponent<RectTransform>();
        //rect.anchoredPosition = vectorGun1;
        //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        spriteGameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        //Mostrar Sprite
        //SpriteRenderer spriteRender = spriteGameObject.AddComponent<SpriteRenderer>();
        // spriteRender.sprite = gunImage;

    }
 

}

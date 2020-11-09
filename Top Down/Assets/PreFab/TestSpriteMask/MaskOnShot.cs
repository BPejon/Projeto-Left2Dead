using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskOnShot : MonoBehaviour
{
    
    public GameObject mask;
    public GameObject self;
    // Start is called before the first frame update
    
    //Corners: Dado um BoXCollider 2D

    /*
        c1-------c2
        *         *
        *         *
        *         *
        c3-------c4
    */ 
    
    
    public Vector2 corner1;
    public Vector2 corner2;
    public Vector2 corner3;
    public Vector2 corner4;

    public Vector2 shapesize;

    //Acuracy - quanto menor o numero, mais iteracoes e mais preciso.
    public float acuracy;

    void Start()
    {
        SetSize();
    }

    // Update is called once per frame
    void Update()
    {    
        UpdateCorners();
    }

    void OnCollisionEnter2D(Collision2D other){
        
        if(other.gameObject.tag.Equals("playerBullet")){

            KBReport kb = other.gameObject.GetComponent<BClass>().GetKBReport();

            //Ponto de impacto.
            Vector2 travelpoint = other.gameObject.transform.position;
            
            Debug.Log("Enter at:" + travelpoint);
            
            Debug.Log("With angle:" + kb.dir);            

            Vector2 travelangle = kb.dir;

            travelpoint = travelpoint + travelangle*acuracy;

            //While: 
            //Enquanto ainda dentro do retangulo, continuamos "andando"
            //Quando ele sair de um dos extremos, ele estara fora.

            while((corner1.x < travelpoint.x && travelpoint.x < corner4.x) && (corner4.y < travelpoint.y && travelpoint.y < corner1.y) ){
                //Problema - a bala nao esta "dentro" da area inicialmente, para remediar isso, use acuracy baixa
                //Podemos também colocar um "containment" aqui. 
                travelpoint = travelpoint + travelangle*acuracy;
            }

            float anglerad = Mathf.Atan2(travelangle.y, travelangle.x);

            float angle = anglerad * Mathf.Rad2Deg;
            

            Debug.Log("Leave at:"+ travelpoint);
            
            Debug.Log("Angle:" + angle);

            GameObject splatter = Instantiate(mask,travelpoint, other.gameObject.transform.rotation);
           
           splatter.transform.rotation = Quaternion.Euler(0,0,angle);
        
        }
    }

    //Setamos o Tamanho das formas do usuário. 
    void SetSize(){
        
        shapesize = this.gameObject.GetComponent<BoxCollider2D>().size;

        shapesize.x = shapesize.x * this.gameObject.transform.localScale.x;
        shapesize.y = shapesize.y * this.gameObject.transform.localScale.y;
    }

    //Define os cantos de nosso vetor para o "Raytracing" pirata.
    void UpdateCorners(){
        
        Vector2 pos = this.gameObject.transform.position;

        //primeiro canto - superior esquerdo
        //-shapesize.x/2 no x. shapesize.y/2 no y
        corner1.x = pos.x - shapesize.x/2;
        corner1.y = pos.y + shapesize.y/2;

        //Segundo canto - superior direito
        // shapesize.x/2 no x. shapesize.y/2 no y
        corner2.x = pos.x + shapesize.x/2;
        corner2.y = pos.y + shapesize.y/2;
        
        //Terceiro canto - inferior esquerdo
        //-shapesize.x/2 no x. -shapesize.y/2 no y
        corner3.x = pos.x - shapesize.x/2;
        corner3.y = pos.y - shapesize.y/2;

        //Quarto quanto - inferior direito
        // shapesize.x/2 no x. -shapesize.y/2 no y
        corner4.x = pos.x + shapesize.x/2;
        corner4.y = pos.y - shapesize.y/2;




    }

}

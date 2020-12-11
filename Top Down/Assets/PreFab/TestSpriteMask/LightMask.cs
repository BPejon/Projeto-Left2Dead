using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMask : MonoBehaviour
{
    //Como funciona: 
    //Geramos poca de sangue no chao
    //Geramos, também espirros de sangue.
    //2 tipos de poca
    //4 tipos de espirro
    //Qual poca e qual espirro selecionados aleatoriamente. 
    //E preciso ter o objeto "GoreSprites"
    public GameObject mask;

    public GameObject goresets;

    public int goreindextest;

    // Start is called before the first frame update
    void Start()
    {
        goresets = GameObject.Find("GoreSprites");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log("Collision - Mask");

        
        if(other.gameObject.tag.Equals("playerBullet")){

            KBReport kb = other.gameObject.GetComponent<BClass>().GetKBReport();


            float anglerad = Mathf.Atan2(kb.dir.y, kb.dir.x);

            float angle = anglerad * Mathf.Rad2Deg;
            
            goreindextest = Random.Range(0,4);


            //Add blood splatter
            GameObject splatter = Instantiate(mask,this.gameObject.transform.position, other.gameObject.transform.rotation);
           
            splatter.GetComponent<SpriteMask>().sprite = goresets.GetComponent<GoreSprites>().splatters[goreindextest];

            splatter.transform.rotation = Quaternion.Euler(0,0,angle);
        
            //Add blood pool:
            GameObject pool = Instantiate(mask,this.gameObject.transform.position, other.gameObject.transform.rotation);

            goreindextest = Random.Range(0,2);

            pool.GetComponent<SpriteMask>().sprite = goresets.GetComponent<GoreSprites>().splatters[goreindextest];

            pool.transform.rotation = Quaternion.Euler(0,0,angle);

        }
    }
    /*
    void OnCollisionEnter2D(Collision2D other){
        
        

        if(other.gameObject.tag.Equals("playerBullet")){

            KBReport kb = other.gameObject.GetComponent<BClass>().GetKBReport();


            float anglerad = Mathf.Atan2(kb.dir.y, kb.dir.x);

            float angle = anglerad * Mathf.Rad2Deg;
            
            goreindextest = Random.Range(0,4);

            Debug.Log("Angle:" + angle + " index:"+ goreindextest);

            //Add blood splatter
            GameObject splatter = Instantiate(mask,this.gameObject.transform.position, other.gameObject.transform.rotation);
           
            splatter.GetComponent<SpriteMask>().sprite = goresets.GetComponent<GoreSprites>().splatters[goreindextest];

            splatter.transform.rotation = Quaternion.Euler(0,0,angle);
        
            //Add blood pool:
            GameObject pool = Instantiate(mask,this.gameObject.transform.position, other.gameObject.transform.rotation);

            goreindextest = Random.Range(0,2);

            pool.GetComponent<SpriteMask>().sprite = goresets.GetComponent<GoreSprites>().splatters[goreindextest];

            pool.transform.rotation = Quaternion.Euler(0,0,angle);

        }
    }*/

}

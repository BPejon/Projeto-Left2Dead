using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedGun : MonoBehaviour
{

    public GunFloor gun;

    public int curammo;
    public int curclip;

    public int indexvalue;

    public string iname;

    void Awake(){
        this.gameObject.GetComponent<SpriteRenderer>().sprite = gun.gunsprite;
        curammo = gun.extraammo;
        curclip = gun.clipammount;
        indexvalue = gun.indexValue;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other){
         
        if(Input.GetKeyDown(KeyCode.G)){
            if(other.gameObject.CompareTag("Player")){
                
                GeneralWeaponScript aux = other.gameObject.transform.Find("WeaponHolder").GetComponent<GeneralWeaponScript>();
                if(aux == null){
                    Debug.Log("Error");
                }
                else{
                    Debug.Log("Good!");
                    aux.ChangeWeapon(this.gameObject);
                }
                
                Destroy(this.gameObject);
            }
        }

        
    }

   

    
}

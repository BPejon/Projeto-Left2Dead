using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    public GameObject weapon;

    public SpriteRenderer wpimg;

    public Vector3 camvec;

    private Transform invert;

   void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        AimAngle();

        if(Input.GetButtonUp("Fire1")){
            weapon.GetComponent<Gun>().Shoot(camvec);
        }

    }

    public void AimAngle(){
        camvec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        camvec.z = 0;
        Vector3 auxcamvec = (camvec - transform.position).normalized;
        camvec = auxcamvec;
        //Angles from -90 to 90 are on normal position
        //Other angles are inverted
        float angle = Mathf.Atan2(auxcamvec.y,auxcamvec.x) * Mathf.Rad2Deg;
        bool invert = false;
        if(!(angle <= 90 && angle >= -90)){
            invert = true;
        }


        if(invert){
           wpimg.flipY = invert; 
           weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x,weapon.transform.eulerAngles.y,angle); 
        }
        else{
            wpimg.flipY = invert; 
            weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x,weapon.transform.eulerAngles.y,angle); 
        }

    }

}
